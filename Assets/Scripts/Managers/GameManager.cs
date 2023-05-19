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
    private int _lifePrice = 10;
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
        // Set values
        _pauseFrame.SetActive(false);
        _victoryFrame.SetActive(false);

        EventManager.instance.OnLevelVictory += OnLevelVictory;
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

        if (Input.GetKeyDown(KeyCode.V)) {
            OnLevelVictory(true);
        }
    }

    #region PAUSE/RESUME

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

    #endregion

    #region NEXT_LEVEL

    public void BuyLife()
    {
        EventManager.instance.ActionRecoverLife();
        EventManager.instance.ActionSpend(_lifePrice);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        _victoryFrame.SetActive(false);
    }

    #endregion

    private void OnLevelVictory(bool _isVictory) 
    {
        if (_isVictory) {
            Time.timeScale = 0f;
            _victoryFrame.SetActive(true);
        } else {
            SceneManager.LoadScene(UnityScenes.Defeat.DisplayName());
        }
    }
}
