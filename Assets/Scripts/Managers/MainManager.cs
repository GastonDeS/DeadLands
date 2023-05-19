using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    #region PROPERTIES

    public int TotalKills() => _totalKills;
    private static int _totalKills;

    public int CurrentLevel() => _currentLevel;
    private static int _currentLevel;

    public int CurrentLevelKills() => _currentLevelKills;
    private static int _currentLevelKills;

    public static MainManager instance; 

    #endregion
    
    #region UNITY_EVENTS

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

    #endregion

    #region  KILLS AND LEVELS

    public void NewKill() 
    {
      _totalKills++;
      _currentLevelKills--;
      if (_currentLevelKills == 0) { // New level unlocked
        _currentLevel++;
        StartCoroutine(SetVictoryFrame());
      }
    }

    public void SetCurrentLevelKills(int amount) {
      _currentLevelKills = amount;
    }

    #endregion

    #region COROUTINES

    IEnumerator SetVictoryFrame()
    {
      // Delay for 2 seconds before setting victory frame
      yield return new WaitForSeconds(2f);
      EventManager.instance.ActionLevelChange(_currentLevel);
      EventManager.instance.ActionLevelVictory(true);
    }

    #endregion
}
