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
        //was not working for some reason just switched to immediately transitioning for now
        //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //if (enemies.Length == 0)
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //}

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
