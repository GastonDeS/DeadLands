using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectController : MonoBehaviour, IListenable
{
    private static SoundEffectController instance = null;

    #region PROPERTIES

    public AudioClip AudioClip => _audioClip;
    [SerializeField] private AudioClip _audioClip;

    public AudioSource AudioSource => _audioSource;

    public float Volume => _volume;
    [Range(0f, 1f)]
    [SerializeField] private float _volume = 1;

    public bool Mute => _mute;
    [SerializeField] private bool _mute = false;

    private AudioSource _audioSource;

    #endregion

    #region UNITY EVENTS

    private void Awake()
    {
        if (instance == null)
        { 
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        if (instance == this) return; 
        Destroy(gameObject);
    }

    void Start()
    {
        InitAudioSource();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            if (_mute) {
                Play();
            } else {
                Stop();
            }
        }
    }

    #endregion

    #region METHODS

    public void InitAudioSource()
    {
        _audioSource        = GetComponent<AudioSource>();
        _audioSource.clip   = _audioClip;
        _audioSource.volume = _volume;
        _audioSource.mute   = _mute;
    }

    public void PlayOneShot() => _audioSource.PlayOneShot(AudioClip);

    public void Play() {
        _audioSource.Play();
        _mute = false;
    }

    public void Stop() {
        _audioSource.Stop();
        _mute = true;
    } 

    public void Pause() => _audioSource.Pause();
   
    #endregion
}