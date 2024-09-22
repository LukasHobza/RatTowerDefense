using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Profiling;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemy;
    private float lastSpawnTime = 0f;
    private float waveDelay = 6f;
    public int wave = 1;
    private int enemyToSpawn;
    private int enemyInCurWave = 0;
    private int randomNum;

    private int[] wave0 = {0,0,1,0,0,0};
    private int[] wave1 = {0,1,0,1,0};
    private int index = 0;

    private void Update()
    {
        if (waveDelay < 5f)
        {
            waveDelay += Time.deltaTime;
        }
        else
        {
            lastSpawnTime += Time.deltaTime;
            switch (wave)
            {
                case 1:
                    waveSettings(0.9f,wave0.Length, wave0[index]);
                    break;
                case 2:
                    waveSettings(0.5f, wave1.Length, wave1[index]);
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }
        }
    }

    private void SpawnEnemy(int enemyNum)
    {
        GameObject spawn = enemy[enemyNum];
        Instantiate(spawn, this.transform.position, Quaternion.identity);
    }

    private void waveSettings(float enemySpawnDelay, int numberOfEnemyInWave, int typeOfEnemy)
    {
        if (lastSpawnTime >= enemySpawnDelay)
        {

            enemyToSpawn = typeOfEnemy;
            lastSpawnTime = 0f;
            SpawnEnemy(enemyToSpawn);
            index++;
            enemyInCurWave++;
            if (enemyInCurWave >= numberOfEnemyInWave)
            {
                wave++;
                waveDelay = 0f;
                enemyInCurWave = 0;
                index = 0;
            }
        }
    }
}