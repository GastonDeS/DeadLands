using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public int TotalKills() => _totalKills;
    private static int _totalKills;

    public int CurrentLevel() => _currentLevel;
    private static int _currentLevel;

    public int CurrentLevelKills() => _currentLevelKills;
    private static int _currentLevelKills;

    public static MainManager instance; 
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() 
    {
      _totalKills   = 0;
      _currentLevel = 1;
    }

    public void NewKill() 
    {
      _totalKills++;
      _currentLevelKills--;
      Debug.Log(_currentLevelKills);
      if (_currentLevelKills == 0) { // New level unlocked
        _currentLevel++;
        EventManager.instance.ActionLevelChange(_currentLevel);
        EventManager.instance.ActionLevelVictory(true);
      }
    }

    public void SetCurrentLevelKills(int amount) {
      _currentLevelKills = amount;
      Debug.Log(_currentLevelKills);
    }
}
