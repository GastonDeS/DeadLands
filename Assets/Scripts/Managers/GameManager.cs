using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scenes;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        EventManager.instance.OnLevelVictory += OnLevelVictory;
    }

    private void OnDestroy()
    {
        EventManager.instance.OnLevelVictory -= OnLevelVictory;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene(UnityScenes.Pause.DisplayName());
    }

    private void OnLevelVictory(bool _isVictory) 
    {
        if (_isVictory) {
            SceneManager.LoadScene(UnityScenes.Market.DisplayName());
        } else {
            SceneManager.LoadScene(UnityScenes.Defeat.DisplayName());
        }
    }
}
