using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

public class MenuManager : MonoBehaviour
{
    public void ActionPlay() => SceneManager.LoadScene(UnityScenes.LoadGameAsync.DisplayName());

    public void ActionExit() => Application.Quit();
}
