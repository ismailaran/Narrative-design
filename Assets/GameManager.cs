using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
    private AudioManager audioManager;

    private bool hasStarted = false;
    private bool redoneIntro = false;

    [SerializeField] private GameObject parisRagdoll;

    public List<AudioClip> glitchPadLines = new List<AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        storyManager = this.gameObject.GetComponent<StoryManager>();
        audioManager = this.gameObject.GetComponent<AudioManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        respawnLocation = player.transform.position;
    }

    private void Update()
    {
        if (!hasStarted)
        {
            storyManager.PlayIntroStoryAudio();
            hasStarted = true;
        }
    }

    public void RespawnPlayer()
    {
        if (!redoneIntro)
        {
            if (fallCounter == 1)
            {
                audioManager.PlayNarratorClip(glitchPadLines[0]);
            }
            if (fallCounter == 2)
            {
                audioManager.PlayNarratorClip(glitchPadLines[1]);
            }
            if (fallCounter == 3)
            {
                audioManager.PlayNarratorClip(glitchPadLines[2]);
            }
            if (fallCounter == 4)
            {
                storyManager.RedoIntro();
                redoneIntro = true;
                //beging story again
                StartCoroutine(redoneStory());
            }
        }
        else
        {
            int random = Random.Range(0, 2);
            audioManager.PlayNarratorClip(glitchPadLines[random]);
        }
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = respawnLocation;
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<PlayerController>().moveDirection = Vector3.zero;
        AddFallCount();
    }

    private IEnumerator redoneStory()
    {
        audioManager.PlayNarratorClip(glitchPadLines[3]);
        yield return new WaitForSeconds(5);
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

    public void UseSpeedPotion()
    {
        playerHasSpeed = false;
        guard.target = merchant;
        storyManager.PlayPotionMarketAudio();
    }
}
