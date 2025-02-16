using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTower : Tower
{
    public Rigidbody2D rb;
    private int coolDownNum = 0;
    public int coolDown;
    public int range;
    public int freezeDuration;  // Specifická hodnota pro FreezeTower
    private GameObject targetEnemy;

    public int dorimeCoolDownBoost = 0;
    public int dorimeRangeBoost = 0;

    [SerializeField] private AudioSource freezeSound; // Zvuk zmrazení
    private AudioManager audioManager; // Správce zvuku

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void FixedUpdate()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        if (coolDownNum <= 0)
        {
            foreach (GameObject enemyInList in enemyList)
            {
                if (enemyInList != null)
                {
                    if (Vector2.Distance(enemyInList.transform.position, rb.transform.position) <= (range + dorimeRangeBoost))
                    {
                        if (!enemyInList.gameObject.GetComponent<Enemy>().isInvisible)
                        {
                            targetEnemy = enemyInList;
                            targetEnemy.GetComponent<EnemyMovement>().freezeDuration = freezeDuration;

                            if (freezeSound != null && audioManager != null)
                            {
                                freezeSound.volume = audioManager.GetSFXVolume();
                                freezeSound.Play();
                            }
                        }
                    }
                }
            }
            coolDownNum = coolDown - dorimeCoolDownBoost;
        }
        else
        {
            coolDownNum--;
        }
    }

    // Kliknutí na věž → otevře menu
    
}
