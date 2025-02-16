using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FreezeTower : MonoBehaviour
{
    public Rigidbody2D rb;
    private int coolDownNum = 0;
    public int coolDown;
    public int range;
    public int freezeDuration;
    private GameObject targetEnemy;

    public int dorimeCoolDownBoost = 0;
    public int dorimeRangeBoost = 0;

    [SerializeField] private AudioSource freezeSound; // P�idan� prom�nn� pro zvukov� efekt zmrazen�
    private AudioManager audioManager; // P�id�me spr�vce zvuku

    void Start()
    {
        // Z�sk�me odkaz na AudioManager (pokud existuje)
        audioManager = FindObjectOfType<AudioManager>(); // Pokud pou��v� singleton
    }

    void FixedUpdate()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy"); // List v�ech nep��tel
        if (coolDownNum <= 0)
        {
            foreach (GameObject enemyInList in enemyList) // Projde v�echny nep��tele
            {
                if (enemyInList != null)
                {
                    if (Vector2.Distance(enemyInList.transform.position, rb.transform.position) <= (range + dorimeRangeBoost)) // Pokud je nep��tel bl�zko v�e
                    {
                        if (!enemyInList.gameObject.GetComponent<Enemy>().isInvisible)
                        {
                            targetEnemy = enemyInList; // Nastav� nov�ho c�lov�ho nep��tele
                            targetEnemy.GetComponent<EnemyMovement>().freezeDuration = freezeDuration;

                            // P�ehraje zvukov� efekt zmrazen� s nastavenou hlasitost�
                            if (freezeSound != null && audioManager != null)
                            {
                                freezeSound.volume = audioManager.GetSFXVolume(); // Nastav� hlasitost dle zvukov�ho spr�vce
                                freezeSound.Play(); // P�ehraje zvuk
                            }
                        }
                    }
                }
            }
            coolDownNum = coolDown - dorimeCoolDownBoost; // Nastaven� cooldownu s bonusem
        }
        else
        {
            coolDownNum--; // Decrement cooldownu
        }
    }
}
