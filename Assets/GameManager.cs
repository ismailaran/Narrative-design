using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> pathBlockers = new List<GameObject>();
    private GameObject player;

    private Vector3 respawnLocation;
    public int timesFellOffMap { get { return fallCounter; } }
    private int fallCounter;

    public bool playerHasPoison = false;

    private StoryManager storyManager;

    // Start is called before the first frame update
    void Start()
    {
        storyManager = this.gameObject.GetComponent<StoryManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        respawnLocation = player.transform.position;

        foreach(GameObject blocker in pathBlockers)
        {
            blocker.SetActive(false);
        }

        storyManager.PlayIntroStoryAudio();
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
}
