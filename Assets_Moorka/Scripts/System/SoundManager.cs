using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [SerializeField]
    private AudioClip[] SEs;
    [SerializeField]
    private AudioClip[] BGMs;

    [SerializeField]
    private AudioSource audioSource;    

    public void LoadClip(int i)
    {
        audioSource.PlayOneShot(SEs[i]);
    }

    public void LoadBGM(int i)
    {
        audioSource.clip = BGMs[i];
        audioSource.Play();
    }
}
