using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Collections.Concurrent;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

public class GameManager : MonoBehaviour
{
    private bool _isPaused = false;
    private GameObject _hud;
    private GameObject _pauseFrame;
    private GameObject _victoryFrame;

    private void Awake()
    {
        _hud          = GameObject.Find(UnityObjects.Hud.DisplayName());
        _pauseFrame   = GameObject.Find(UnityObjects.PauseFrame.DisplayName());
        _victoryFrame = GameObject.Find(UnityObjects.VictoryFrame.DisplayName());
    }

    private void Start()
    {
        _pauseFrame.SetActive(false);
        _victoryFrame.SetActive(false);

        EventManager.instance.OnLevelVictory += OnLevelVictory;
    }

    private void OnDestroy()
    {
        EventManager.instance.OnLevelVictory -= OnLevelVictory;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (_isPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Pause()
    {
        _pauseFrame.SetActive(true);
        _hud.SetActive(false);
        _isPaused = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        _pauseFrame.SetActive(false);
        _hud.SetActive(true);
        _isPaused = false;
        Time.timeScale = 1f;
    }

    private void OnLevelVictory(bool _isVictory) 
    {
        if (_isVictory) {
            _victoryFrame.SetActive(true);
            _hud.SetActive(false);
        } else {
            SceneManager.LoadScene(UnityScenes.Defeat.DisplayName());
        }
    }
}
