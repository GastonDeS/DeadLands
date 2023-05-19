using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public int TotalKills() => _totalKills;
    private static int _totalKills;

    public int CurrentLevel() => _currentLevel;
    private static int _currentLevel;

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

    public int NewKill() 
    {
      return ++_totalKills;
    }

    public void LevelVictory() 
    {
      _currentLevel++;
    }
}
