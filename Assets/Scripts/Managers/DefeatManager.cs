using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;
using TMPro;

public class DefeatManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private TextMeshProUGUI _totalKills;

    #region UNITY EVENTS

    public void Start() 
    {
        _level.text      = MainManager.instance.CurrentLevel().ToString();
        _totalKills.text = MainManager.instance.TotalKills().ToString();
    }

    #endregion
    
    #region BUTTONS

    public void ActionRestart() => SceneManager.LoadScene(UnityScenes.SampleScene.DisplayName());

    public void ActionExit() => Application.Quit();

    #endregion
}
