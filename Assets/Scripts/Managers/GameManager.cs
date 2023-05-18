using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scenes;

public class GameManager : MonoBehaviour
{
    // [SerializeField] private bool _isVictory = false;
    // [SerializeField] private bool _isDefeat = false;

    private void Start()
    {
        EventManager.instance.OnLevelVictory += OnLevelVictory;
    }

    private void OnDestroy()
    {
        EventManager.instance.OnLevelVictory -= OnLevelVictory;
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
