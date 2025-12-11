using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public LevelData levelData;
    public GameObject[] enemies;
    public int levelNumber = 1;
    private List<Transform> enemyPositions = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FindAllEnemyPositions();
        int enemyCount = GetEnemyCountForLevel(levelNumber);
        StartCoroutine(SpawnAtPositions(enemyCount));
    }

    private void FindAllEnemyPositions()
    {
        GameObject[] positions = GameObject.FindGameObjectsWithTag("Enemy");
        enemyPositions.AddRange(positions.Select(x => x.transform));
    }

    int GetEnemyCountForLevel(int level)
    {
        var diff = GameManager.instance.currentDifficulty;
        var info = levelData.levels[level - 1];
        return diff == GameManager.Difficulty.Easy ? info.easy :
            diff == GameManager.Difficulty.Medium ? info.medium :
            info.hard;
    }


    IEnumerator SpawnAtPositions(int count)
    {
        
        List<Transform> shuffled = new List<Transform>(enemyPositions);
        Shuffle(shuffled);

        for (int i = 0; i < Mathf.Min(count, shuffled.Count); i++)
        {
            Transform pos = shuffled[i];
            Instantiate(enemies[UnityEngine.Random.Range(0, enemies.Length)],
                       pos.position, pos.rotation);
            yield return new WaitForSeconds(1f);
        }
    }

    void Shuffle(List<Transform> list)
    {
        for(int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            Transform temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    
}
