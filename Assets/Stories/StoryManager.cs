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
    public List<AudioClip> outroStoryLines = new List<AudioClip>();

    [SerializeField] private GameObject startingDoor;

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

    public void PlayMarketStoryAudio()
    {
        player.canMove = false;
        StartCoroutine(playMarketStory());
    }

    public void PlayOutroStoryAudio()
    {
        player.canMove = false;
        StartCoroutine(playOutroStory());
    }

    private IEnumerator playOutroStory()
    {
        audioManager.PlayNarratorClip(outroStoryLines[0]);
        yield return new WaitForSeconds(14);
        audioManager.PlayNarratorClip(outroStoryLines[1]);
        yield return new WaitForSeconds(14);
        audioManager.PlayNarratorClip(outroStoryLines[2]);
        yield return new WaitForSeconds(15);
        audioManager.PlayNarratorClip(outroStoryLines[3]);
        yield return new WaitForSeconds(6);
        audioManager.PlayNarratorClip(outroStoryLines[4]);
        yield return new WaitForSeconds(14);
        audioManager.PlayRomeoClip(outroStoryLines[5]);
        yield return new WaitForSeconds(1);
        audioManager.PlayNarratorClip(outroStoryLines[6]);
        yield return new WaitForSeconds(20);
        Application.Quit();
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
    }

    private IEnumerator playIntroStory()
    {
        audioManager.PlayNarratorClip(introStoryLines[0]);
        yield return new WaitForSeconds(12);
        for (float i = 2; i >= 0; i -= Time.deltaTime)         {             panelImage.color = new Color(0, 0, 0, (i / 2));             yield return null;         }
        yield return new WaitForSeconds(2);
        player.canMove = true;
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
