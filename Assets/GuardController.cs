using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardController : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent agent;
    private Animator anim;

    public bool chasing = false;
    public bool recovering = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!recovering && chasing)
        {
            Vector3 direction = target.position - this.transform.position;

            if (direction.magnitude > 2)
            {
                agent.destination = target.position;
                anim.SetFloat("Speed", agent.velocity.magnitude);
            }
            else
            {
                agent.destination = agent.transform.position;
                anim.SetFloat("Speed", 0);
                StartCoroutine(Attack());
            }
        }
    }

    private IEnumerator Attack()
    {
        recovering = true;
        anim.Play("Attack");
        yield return new WaitForSeconds(2);
        recovering = false;
    }
}
