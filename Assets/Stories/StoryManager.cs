using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager: MonoBehaviour
{
    private PlayerController player;
    [SerializeField] private Image panelImage;
    private AudioManager audioManager;

    public List<AudioClip> introStoryLines = new List<AudioClip>();
    public List<AudioClip> marketStoryLines = new List<AudioClip>();
    public List<AudioClip> creepyGuyIntroLines = new List<AudioClip>();
    public List<AudioClip> speedPotionStoryLines = new List<AudioClip>();
    public List<AudioClip> outroStoryLines = new List<AudioClip>();

    public List<AudioClip> marketPotionStoryLines = new List<AudioClip>();
    public List<AudioClip> trueEndingEnd = new List<AudioClip>();

    [SerializeField] private GameObject startingDoor;
    public bool playedCreepyGuyIntro = false;
    public bool playing = false;

    public bool canJump = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        audioManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioManager>();
    }

    public void PlayIntroStoryAudio()
    {
        player.canMove = false;
        StartCoroutine(playIntroStory());
    }

    public void PlayTrueEndingAudio()
    {
        player.canMove = false;
        StartCoroutine(playTrueEndingStory());
    }

    public void PlayPotionMarketAudio()
    {
        StartCoroutine(playMarketPotionStory());
    }

    public void PlayMarketStoryAudio()
    {
        player.canMove = false;
        StartCoroutine(playMarketStory());
    }

    public void PlaySpeedPotionStoryAudio()
    {
        player.canMove = false;
        StartCoroutine(playSpeedPotionStory());
    }

    public void PlayOutroStoryAudio()
    {
        player.canMove = false;
        StartCoroutine(playOutroStory());
    }

    public void PlayCreepyGuyIntro()
    {
        playing = true;
        StartCoroutine(playCreepyGuyStory());
    }

    public void RedoIntro()
    {
        player.canMove = false;
        panelImage.color = new Color(0, 0, 0, 255);
        StartCoroutine(fadePanelFromBlack());
    }

    private IEnumerator playMarketPotionStory()
    {
        audioManager.PlayNarratorClip(marketPotionStoryLines[0]);
        yield return new WaitForSeconds(13);
        audioManager.PlayNarratorClip(marketPotionStoryLines[1]);
        yield return new WaitForSeconds(2);
        audioManager.PlayNarratorClip(marketPotionStoryLines[2]);
        yield return new WaitForSeconds(17);
        audioManager.PlayNarratorClip(marketPotionStoryLines[3]);
        //Activate next story node: Node 15
        List<int> tempList = new List<int>();
        tempList.Add(15);
        gameObject.GetComponent<ConnectionsManager>().EnableTriggers(tempList);
    }

    private IEnumerator playCreepyGuyStory()
    {
        audioManager.PlayNarratorClip(creepyGuyIntroLines[0]);
        yield return new WaitForSeconds(10);
        audioManager.PlayNarratorClip(creepyGuyIntroLines[1]);
        yield return new WaitForSeconds(3);
        playing = false;
        playedCreepyGuyIntro = true;
    }

    private IEnumerator playTrueEndingStory()
    {
        Debug.Log("Playing true ending");
        yield return new WaitForSeconds(10);
        player.characterController.enabled = false;
        player.transform.position = new Vector3(52, -0.33f, 120);
        player.characterController.enabled = true;
        audioManager.PlayNarratorClip(trueEndingEnd[0]);//36
        yield return new WaitForSeconds(3);
        audioManager.PlayNarratorClip(trueEndingEnd[1]);//37
        yield return new WaitForSeconds(2);
        audioManager.PlayNarratorClip(trueEndingEnd[2]);//38
        yield return new WaitForSeconds(2);
        audioManager.PlayNarratorClip(trueEndingEnd[3]);//39
        yield return new WaitForSeconds(5);
        audioManager.PlayNarratorClip(trueEndingEnd[4]);//40
        yield return new WaitForSeconds(2);
        audioManager.PlayNarratorClip(trueEndingEnd[5]);//41
        yield return new WaitForSeconds(4);
        audioManager.PlayNarratorClip(trueEndingEnd[6]);//42
        yield return new WaitForSeconds(6);
        audioManager.PlayNarratorClip(trueEndingEnd[7]);//43
        yield return new WaitForSeconds(2);
        audioManager.PlayNarratorClip(trueEndingEnd[8]);//44
        yield return new WaitForSeconds(2);
        audioManager.PlayNarratorClip(trueEndingEnd[9]);//45
        yield return new WaitForSeconds(8);
        audioManager.PlayNarratorClip(trueEndingEnd[10]);//46
        yield return new WaitForSeconds(3);
        audioManager.PlayNarratorClip(trueEndingEnd[11]);//47
        yield return new WaitForSeconds(2);
        audioManager.PlayNarratorClip(trueEndingEnd[12]);//48
        yield return new WaitForSeconds(4);
        audioManager.PlayNarratorClip(trueEndingEnd[13]);//49
        yield return new WaitForSeconds(23);
        audioManager.PlayNarratorClip(trueEndingEnd[14]);//50
        yield return new WaitForSeconds(6);
        audioManager.PlayNarratorClip(trueEndingEnd[15]);//51
        yield return new WaitForSeconds(4.5f);
        audioManager.PlayNarratorClip(trueEndingEnd[16]);//52
        yield return new WaitForSeconds(5);
        audioManager.PlayNarratorClip(trueEndingEnd[17]);//53
        yield return new WaitForSeconds(6);
        audioManager.PlayNarratorClip(trueEndingEnd[18]);//55
        player.canMove = true;
        canJump = true;
        yield return new WaitForSeconds(2);
        audioManager.PlayNarratorClip(trueEndingEnd[19]);//56
        yield return new WaitForSeconds(1);
        audioManager.PlayNarratorClip(trueEndingEnd[20]);//57
        yield return new WaitForSeconds(3);
        audioManager.PlayNarratorClip(trueEndingEnd[21]);//58
        yield return new WaitForSeconds(2);
        audioManager.PlayNarratorClip(trueEndingEnd[22]);//59
        yield return new WaitForSeconds(6);
        audioManager.PlayNarratorClip(trueEndingEnd[23]);//60
        yield return new WaitForSeconds(5);
        audioManager.PlayNarratorClip(trueEndingEnd[24]);//61
        yield return new WaitForSeconds(9);
        audioManager.PlayNarratorClip(trueEndingEnd[25]);//62
        yield return new WaitForSeconds(4);
        audioManager.PlayNarratorClip(trueEndingEnd[26]);//63
        yield return new WaitForSeconds(4);
        audioManager.PlayNarratorClip(trueEndingEnd[27]);//64
        yield return new WaitForSeconds(2);
        audioManager.PlayNarratorClip(trueEndingEnd[28]);//65
        yield return new WaitForSeconds(103);
        audioManager.PlayNarratorClip(trueEndingEnd[29]);//66
    }

    private IEnumerator playOutroStory()
    {
        audioManager.PlayNarratorClip(outroStoryLines[4]);
        yield return new WaitForSeconds(14);
        audioManager.PlayRomeoClip(outroStoryLines[5]);
        yield return new WaitForSeconds(1);
        audioManager.PlayNarratorClip(outroStoryLines[6]);
        yield return new WaitForSeconds(20);
        StartCoroutine(fadePanelToBlack());
    }

    private IEnumerator playMarketStory()
    {
        audioManager.PlayRomeoClip(marketStoryLines[0]);
        yield return new WaitForSeconds(3);
        audioManager.PlayNarratorClip(marketStoryLines[1]);
        yield return new WaitForSeconds(13);
        audioManager.PlayRomeoClip(marketStoryLines[2]);
        yield return new WaitForSeconds(5);
        audioManager.PlayNarratorClip(marketStoryLines[3]);
        yield return new WaitForSeconds(10);
        player.canMove = true;
        //Activate next story node: Node 9
        List<int> tempList = new List<int>();
        tempList.Add(9);
        gameObject.GetComponent<ConnectionsManager>().EnableTriggers(tempList);
    }

    private IEnumerator playSpeedPotionStory()
    {
        audioManager.PlayNarratorClip(speedPotionStoryLines[0]);
        yield return new WaitForSeconds(4);
        audioManager.PlayNarratorClip(speedPotionStoryLines[1]);
        yield return new WaitForSeconds(17);
        player.canMove = true;
    }

    private IEnumerator fadePanelToBlack()
    {
        for (float i = 0; i <= 2; i += Time.deltaTime)         {             panelImage.color = new Color(0, 0, 0, (i / 2));             yield return null;         }
        yield return new WaitForSeconds(5);
        Application.Quit();
    }

    private IEnumerator fadePanelFromBlack()
    {
        for (float i = 2; i >= 0; i -= Time.deltaTime)         {             panelImage.color = new Color(0, 0, 0, (i / 2));             yield return null;         }
    }

    private IEnumerator playIntroStory()
    {
        audioManager.PlayNarratorClip(introStoryLines[0]);
        yield return new WaitForSeconds(12);
        for (float i = 2; i >= 0; i -= Time.deltaTime)         {             panelImage.color = new Color(0, 0, 0, (i / 2));             yield return null;         }
        player.canMove = true;
        yield return new WaitForSeconds(2);
        audioManager.PlayNarratorClip(introStoryLines[1]);
        yield return new WaitForSeconds(18);
        audioManager.PlayRomeoClip(introStoryLines[2]);
        yield return new WaitForSeconds(2);
        audioManager.PlayNarratorClip(introStoryLines[3]);
        yield return new WaitForSeconds(9);
        audioManager.PlayRomeoClip(introStoryLines[4]);
        yield return new WaitForSeconds(3);
        audioManager.PlayNarratorClip(introStoryLines[5]);
        startingDoor.GetComponent<Rigidbody>().AddForce(startingDoor.transform.forward * 300, ForceMode.Impulse);
        startingDoor.GetComponent<Rigidbody>().AddExplosionForce(100f,  startingDoor.transform.position, 35f);
    }
}
