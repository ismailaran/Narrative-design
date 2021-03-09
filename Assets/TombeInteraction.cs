using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TombeInteraction : MonoBehaviour
{
    private PlayerController player;
    [SerializeField] private Image panelImage;
    private StoryManager storyManager;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        storyManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<StoryManager>();
    }

    public void DrinkPoison()
    {
        player.canMove = false;
        storyManager.PlayOutroStoryAudio();
        StartCoroutine(FadeToBlack());
    }

    private IEnumerator FadeToBlack()
    {
        for (float i = 0; i <= 2; i += Time.deltaTime)         {             panelImage.color = new Color(0, 0, 0, (i/2));             yield return null;         }         yield return new WaitForSeconds(10);
    }
}
