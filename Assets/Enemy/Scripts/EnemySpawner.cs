using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemy;
    private float lastSpawnTime = 0f;
    private float waveDelay = 6f;
    public int wave = 1;
    private int enemyToSpawn;
    private int enemyInCurWave = 0;

    //pole kde jsou enemyci co se budou spawnovat v urcitych vlnach
    //private int[] wave0 = {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1};
    //private int[] wave0 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private int[] wave0 = {0,0,0, 0, 0, 0 , 0, 1, 0 , 0, 1, 1 , 0, 0, 0 , 0};
    private int[] wave1 = {0,1,2,2,2,1,1,0,0};
    private int[] wave2 = {0,0,1,4,0,0,4,3};
    private int[] wave3 = { 0, 0, 8, 8, 8, 8, 0, 0 };
    private int[] wave4 = { 3, 0, 7, 0, 6, 1, 5, 3 };
    private int index = 0;

    private void Update()
    {
        if (waveDelay < 5f)//mezera mezi vlnaka
        {
            waveDelay += Time.deltaTime;
        }
        else
        {
            lastSpawnTime += Time.deltaTime;
            switch (wave)
            {
                case 1:
                    waveSettings(2.4f,wave0.Length, wave0[index]);//spawnuti vlny
                    break;
                case 2:
                    waveSettings(2.1f, wave1.Length, wave1[index]);
                    break;
                case 3:
                    waveSettings(1.8f, wave2.Length, wave2[index]);
                    break;
                case 4:
                    waveSettings(1.8f, wave3.Length, wave3[index]);
                    break;
                case 5:
                    waveSettings(1.8f, wave4.Length, wave4[index]);
                    break;
            }
        }
    }

    private void SpawnEnemy(int enemyNum)
    {
        GameObject spawn = enemy[enemyNum];//nastaveni enemaka ktery se spawne
        Instantiate(spawn, this.transform.position, Quaternion.identity);//spawnuti enemaka
    }

    private void waveSettings(float enemySpawnDelay, int numberOfEnemyInWave, int typeOfEnemy)
    {
        if (lastSpawnTime >= enemySpawnDelay)//zdrzeni mezi enemaky ve vlne
        {
            enemyToSpawn = typeOfEnemy;//ktery enemy se bude spawnovat
            lastSpawnTime = 0f;
            SpawnEnemy(enemyToSpawn);//spawne enemaka
            index++;
            enemyInCurWave++;
            if (enemyInCurWave >= numberOfEnemyInWave)//konec vlny
            {
                wave++;
                waveDelay = 0f;
                enemyInCurWave = 0;
                index = 0;
            }
        }
    }
}