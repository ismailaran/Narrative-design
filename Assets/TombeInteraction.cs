using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TombeInteraction : MonoBehaviour
{
    private PlayerController player;
    [SerializeField] private Image panelImage;
    private bool fading = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
    }

    public void DrinkPoison()
    {
        player.canMove = false;
        StartCoroutine(FadeToBlack());
    }

    private IEnumerator FadeToBlack()
    {
        for (float i = 0; i <= 2; i += Time.deltaTime)         {             panelImage.color = new Color(0, 0, 0, (i/2));             yield return null;         }         yield return new WaitForSeconds(10);
        Application.Quit();
    }
}
