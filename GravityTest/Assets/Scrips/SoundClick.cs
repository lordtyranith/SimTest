using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundClick : Singleton<SoundClick>
{
    [SerializeField] AudioSource SoundClick1;
    [SerializeField] AudioSource SoundClick2;

    public void SoundClicking1()
    {
        SoundClick1.Play();
    }
    public void SoundClicking2()
    {
        SoundClick2.Play();
    }

    public void MuteAll()
    {
        AudioListener.volume = 0f;
    }
    public void UnMuteAll()
    {
        AudioListener.volume = 1f;
    }
}
