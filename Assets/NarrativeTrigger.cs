using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeTrigger : MonoBehaviour, Itriggerable
{
    [SerializeField] private AudioClip narrativeLine;
    [SerializeField] private bool hasRomeoVoiceLine = false;
    [SerializeField] private float voiceLineDelay;
    [SerializeField] private AudioClip romeoLine;

    private AudioManager audioManager;
    private GameManager gameManager;

    [SerializeField] private bool hasBlockingEvent = false;
    [SerializeField] private int blockerID;

    [HideInInspector] public bool isActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject manager = GameObject.FindGameObjectWithTag("GameController");
        audioManager = manager.GetComponent<AudioManager>();
        gameManager = manager.GetComponent<GameManager>();
    }

    public void TriggerEvent()
    {
        isActivated = true;

        audioManager.PlayNarratorClip(narrativeLine);

        gameManager.SetNewRespawnLocation(this.transform.position);

        if (hasRomeoVoiceLine)
        {
            StartCoroutine(romeoVoiceLineDelay());
        }
    }

    private IEnumerator romeoVoiceLineDelay()
    {
        yield return new WaitForSeconds(voiceLineDelay);
        audioManager.PlayRomeoClip(romeoLine);
    }
}
