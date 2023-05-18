using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scenes;

public class PauseManager : MonoBehaviour
{
    private float _previousTimeScale;

    // public static PauseManager Instance { get; private set; }

    // private void Awake()
    // {
    //     Instance = this;
    // }

    // public void ActionPause()
    // {
    //     _previousTimeScale = Time.timeScale;
    //     Time.timeScale = 0f; // Set time scale to 0 to pause the game
    //     SceneManager.LoadScene(UnityScenes.Pause.DisplayName());
    // }

    // private void ResumeGame()
    // {
    //     isPaused = false;
    //     Time.timeScale = previousTimeScale; // Restore the previous time scale
    //     // Additional code to hide pause menu, unlock cursor, etc.
    // }

    public void ActionResume() => SceneManager.LoadScene(UnityScenes.SampleScene.DisplayName());

    public void ActionQuit() => SceneManager.LoadScene(UnityScenes.MainMenu.DisplayName());
}
