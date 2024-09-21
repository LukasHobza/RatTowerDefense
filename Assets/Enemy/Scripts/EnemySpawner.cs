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
                    waveSettings(0.9f,10, 0);
                    break;
                case 2:
                    waveSettings(0.5f,20, 0, 1);
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

            enemyInCurWave++;
            if (enemyInCurWave >= numberOfEnemyInWave)
            {
                wave++;
                waveDelay = 0f;
                enemyInCurWave = 0;
            }
        }
    }

    private void waveSettings(float enemySpawnDelay, int numberOfEnemyInWave, int typeOfEnemy0, int typeOfEnemy1)
    {
        if (lastSpawnTime >= enemySpawnDelay)
        {
            randomNum = Random.Range(0, 100);
            if (randomNum >= 0 && randomNum < 50) enemyToSpawn = typeOfEnemy0;
            else enemyToSpawn =  typeOfEnemy1;
            lastSpawnTime = 0f;
            SpawnEnemy(enemyToSpawn);

            enemyInCurWave++;
            if (enemyInCurWave >= numberOfEnemyInWave)
            {
                wave++;
                waveDelay = 0f;
                enemyInCurWave = 0;
            }
        }
    }

    private void waveSettings(float enemySpawnDelay, int numberOfEnemyInWave, int typeOfEnemy0, int typeOfEnemy1, int typeOfEnemy2)
    {
        if (lastSpawnTime >= enemySpawnDelay)
        {
            randomNum = Random.Range(0, 100);
            if (randomNum >= 0 && randomNum < 33) enemyToSpawn = typeOfEnemy0;
            else if (randomNum >= 33 && randomNum < 66) enemyToSpawn = typeOfEnemy1;
            else enemyToSpawn = typeOfEnemy2;

            lastSpawnTime = 0f;
            SpawnEnemy(enemyToSpawn);

            enemyInCurWave++;
            if (enemyInCurWave >= numberOfEnemyInWave)
            {
                wave++;
                waveDelay = 0f;
                enemyInCurWave = 0;
            }
        }
    }

    private void waveSettings(float enemySpawnDelay, int numberOfEnemyInWave, int typeOfEnemy0, int typeOfEnemy1, int typeOfEnemy2, int typeOfEnemy3)
    {
        if (lastSpawnTime >= enemySpawnDelay)
        {
            randomNum = Random.Range(0, 100);
            if (randomNum >= 0 && randomNum < 25) enemyToSpawn = typeOfEnemy0;
            else if (randomNum >= 25 && randomNum < 50) enemyToSpawn = typeOfEnemy1;
            else if (randomNum >= 50 && randomNum < 75) enemyToSpawn = typeOfEnemy2;
            else enemyToSpawn = typeOfEnemy3;
            lastSpawnTime = 0f;
            SpawnEnemy(enemyToSpawn);

            enemyInCurWave++;
            if (enemyInCurWave >= numberOfEnemyInWave)
            {
                wave++;
                waveDelay = 0f;
                enemyInCurWave = 0;
            }
        }
    }
}