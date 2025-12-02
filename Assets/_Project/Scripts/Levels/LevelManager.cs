using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int enemiesToDefeat = 10;
    private int deafeatedEnemies = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void EnemyDefeated()
    {
        deafeatedEnemies++;
        if(deafeatedEnemies >= enemiesToDefeat)
        {
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
