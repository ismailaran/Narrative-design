using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour, Itriggerable
{
    [SerializeField] private List<AudioClip> narrativeLines = new List<AudioClip>();

    private AudioManager audioManager;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject manager = GameObject.FindGameObjectWithTag("GameController");
        audioManager = manager.GetComponent<AudioManager>();
        gameManager = manager.GetComponent<GameManager>();
    }

    public void TriggerEvent()
    {
        int lineID = gameManager.timesFellOffMap;

        if(lineID > (narrativeLines.Count - 1))
        {
            lineID = (narrativeLines.Count - 1);
        }

        //audioManager.PlayNarratorClip(narrativeLines[lineID]);
        gameManager.AddFallCount();

        gameManager.RespawnPlayer();
    }
}
