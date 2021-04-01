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
                player.GetComponent<CharacterController>().enabled = false;
                player.transform.position = new Vector3(0,1.55f,-11.47f);
                player.GetComponent<CharacterController>().enabled = true;
                player.GetComponent<PlayerController>().moveDirection = Vector3.zero;
                StartCoroutine(redoneStory());
                return;
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
        yield return new WaitForSeconds(18);
        audioManager.PlayNarratorClip(glitchPadLines[4]);
        yield return new WaitForSeconds(4);
        audioManager.PlayNarratorClip(glitchPadLines[5]);
        yield return new WaitForSeconds(7);
        audioManager.PlayNarratorClip(glitchPadLines[6]);
        yield return new WaitForSeconds(4);
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = new Vector3(-190, 11f, 135);
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<PlayerController>().moveDirection = Vector3.zero;
        audioManager.PlayNarratorClip(glitchPadLines[7]);
        yield return new WaitForSeconds(7);
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = new Vector3(-91, 1.55f, -4.5f);
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<PlayerController>().moveDirection = Vector3.zero;

        //activate story node 4
        List<int> tempList = new List<int>();
        tempList.Add(4);
        this.gameObject.GetComponent<ConnectionsManager>().EnableTriggers(tempList);

        audioManager.PlayNarratorClip(glitchPadLines[8]);
        yield return new WaitForSeconds(7);
        audioManager.PlayNarratorClip(glitchPadLines[9]);
        yield return new WaitForSeconds(4);
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = new Vector3(-6.5f, 1.55f, 165);
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<PlayerController>().moveDirection = Vector3.zero;
        audioManager.PlayNarratorClip(glitchPadLines[10]);
        yield return new WaitForSeconds(8);
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
