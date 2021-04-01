using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> pathBlockers = new List<GameObject>();
    private GameObject player;
    [SerializeField]private Transform merchant;

    private Vector3 respawnLocation;
    public int timesFellOffMap { get { return fallCounter; } }
    private int fallCounter;

    public bool playerHasPoison = false;
    public bool playerHasSpeed = false;

    public bool calledGuards = false;
    [SerializeField] GuardController guard;

    private StoryManager storyManager;

    private bool hasStarted = false;

    [SerializeField] private GameObject parisRagdoll;

    // Start is called before the first frame update
    void Start()
    {
        storyManager = this.gameObject.GetComponent<StoryManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        respawnLocation = player.transform.position;

        /*foreach(GameObject blocker in pathBlockers)
        {
            blocker.SetActive(false);
        }*/
    }

    private void Update()
    {
        if (!hasStarted)
        {
            storyManager.PlayIntroStoryAudio();
            hasStarted = true;
        }
    }

    public void BlockPath(int id)
    {
        pathBlockers[id].SetActive(true);
    }

    public void RespawnPlayer()
    {
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = respawnLocation;
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<PlayerController>().moveDirection = Vector3.zero;
    }

    public void AddFallCount()
    {
        fallCounter++;
    }

    public void SetNewRespawnLocation(Vector3 location)
    {
        if(location != null)
        {
            respawnLocation = location;
        }
    }

    public void CallGuards()
    {
        guard.chasing = true;
        guard.target = player.transform;
    }

    public void KillParis(GameObject paris)
    {
        parisRagdoll.transform.position = paris.transform.position;
        parisRagdoll.transform.rotation = paris.transform.rotation;

        Destroy(paris);
        Instantiate(parisRagdoll);
    }

    public IEnumerator UseSpeedPotion()
    {
        playerHasSpeed = false;
        guard.target = merchant;
        PlayerController script = player.GetComponent<PlayerController>();
        float startingSpeed = script.walkingSpeed;
        script.walkingSpeed = 7;
        yield return new WaitForSeconds(3);
        script.walkingSpeed = startingSpeed;
    }
}
