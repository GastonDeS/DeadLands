using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IListenable
{
    GameObject SoundObject { get; }
    Slider VolumeSlider { get; }
    float Volume { get; }

    void Play();
    void Stop();
    void Pause();

    void InitAudioSource();
    void UpdateVolume(float volume);
}
