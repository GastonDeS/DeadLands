using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IListenable
{
    AudioClip AudioClip { get; }
    AudioSource AudioSource { get; }

    float Volume { get; }
    bool Mute { get; }

    void Play();
    void Stop();
    void Pause();

    void InitAudioSource();
    void PlayOneShot();
}