using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyLevelManager : MonoBehaviour
{
    public LevelData levelData;
    public int enemiesToDefeat = 10;
    private int deafeatedEnemies = 0;
    private static int currentLevel = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetEnemiesToDefeatForLevel();
    }

    private void SetEnemiesToDefeatForLevel()
    {
        if(levelData != null && currentLevel <= levelData.levels.Length)
        {
            var info = levelData.levels [currentLevel - 1];
            var diff = GameManager.instance.currentDifficulty;
            enemiesToDefeat = diff == GameManager.Difficulty.Easy ? info.easy
                : diff == GameManager.Difficulty.Easy ? info.medium
                : info.hard;
        }
    }

    

    public void EnemyDefeated()
    {
        deafeatedEnemies++;
        if (deafeatedEnemies >= enemiesToDefeat)
        {
            currentLevel++;
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

