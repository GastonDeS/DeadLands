using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scenes;

public class DefeatManager : MonoBehaviour
{
    public void ActionRestart() => SceneManager.LoadScene(UnityScenes.SampleScene.DisplayName());

    public void ActionExit() => Application.Quit();
}
