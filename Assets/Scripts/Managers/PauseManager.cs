using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scenes;

public class PauseManager : MonoBehaviour
{
    public void ActionResume() => SceneManager.LoadScene(UnityScenes.SampleScene.DisplayName());

    public void ActionQuit() => SceneManager.LoadScene(UnityScenes.MainMenu.DisplayName());
}
