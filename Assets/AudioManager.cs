using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource NarratorAudioSource;
    [SerializeField] private AudioSource RomeoAudioSource;
    [SerializeField] private AudioSource OtherAudioSource;

    [SerializeField] private float AudioVolume = 1;

    // Start is called before the first frame update
    void Start()
    {
        NarratorAudioSource.volume = AudioVolume;
        RomeoAudioSource.volume = AudioVolume;
        OtherAudioSource.volume = AudioVolume;
    }

    public void PlayNarratorClip(AudioClip clip)
    {
        NarratorAudioSource.PlayOneShot(clip);
    }

    public void PlayRomeoClip(AudioClip clip)
    {
        RomeoAudioSource.PlayOneShot(clip);
    }

    public void PlayOtherClip(AudioClip clip)
    {
        OtherAudioSource.PlayOneShot(clip);
    }
}
