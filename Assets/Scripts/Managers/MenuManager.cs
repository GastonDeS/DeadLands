using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scenes;

public class MenuManager : MonoBehaviour
{
    public void ActionPlay() => SceneManager.LoadScene(UnityScenes.SampleScene.DisplayName());

    public void ActionSettings() => SceneManager.LoadScene(UnityScenes.Settings.DisplayName());

    public void ActionExit() => Application.Quit();
}
