using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundEffectController : MonoBehaviour, IListenable
{
    #region IListenable_Properties

    public GameObject SoundObject => _soundObject;
    [SerializeField] private GameObject _soundObject;

    public Slider VolumeSlider => _volumeSlider;
    [SerializeField] private Slider _volumeSlider;

    public float Volume => _volume;
    [Range(0f, 1f)]
    [SerializeField] private float _volume;

    #endregion

    private AudioSource _audioSource;

    #region IListenable_Methods
    public void InitAudioSource()
    {
        // Asignar el componente AudioSource
        _soundObject = GameObject.FindGameObjectWithTag("BackgroundSound");
        _audioSource = _soundObject.GetComponent<AudioSource>();
        // Asignamos el audio clip al AudioSource
        _volume = PlayerPrefs.GetFloat("volume");
        _audioSource.volume = _volume;
        _volumeSlider.value = _volume;
    }

    public void Play() => _audioSource.Play();

    public void Stop() => _audioSource.Stop();

    public void Pause() => _audioSource.Pause();

    public void UpdateVolume(float volume) {
        _volume = volume;
        _audioSource.volume = _volume;
        PlayerPrefs.SetFloat("volume", _volume);
    }
   
    #endregion

    #region Unity_Events
    // Start is called before the first frame update
    void Start()
    {
        InitAudioSource();
    }

    void Update()
    {
        _audioSource.volume = _volume;
    }

    #endregion
}

