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

    private void Start() 
    {
        EventManager.instance.OnDefeat += OnDefeat;
    }

    private void OnDefeat(int level, int totalKills)
    {
        _level.text = $"{level}";
        _totalKills.text = $"{totalKills}";
    }

    public void ActionRestart() => SceneManager.LoadScene(UnityScenes.SampleScene.DisplayName());

    public void ActionExit() => Application.Quit();
}
