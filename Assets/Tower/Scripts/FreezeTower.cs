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

    [SerializeField] private AudioSource freezeSound; // Pøidaná promìnná pro zvukový efekt zmrazení
    private AudioManager audioManager; // Pøidáme správce zvuku

    void Start()
    {
        // Získáme odkaz na AudioManager (pokud existuje)
        audioManager = FindObjectOfType<AudioManager>(); // Pokud používáš singleton
    }

    void FixedUpdate()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy"); // List všech nepøátel
        if (coolDownNum <= 0)
        {
            foreach (GameObject enemyInList in enemyList) // Projde všechny nepøátele
            {
                if (enemyInList != null)
                {
                    if (Vector2.Distance(enemyInList.transform.position, rb.transform.position) <= (range + dorimeRangeBoost)) // Pokud je nepøítel blízko vìže
                    {
                        if (!enemyInList.gameObject.GetComponent<Enemy>().isInvisible)
                        {
                            targetEnemy = enemyInList; // Nastaví nového cílového nepøítele
                            targetEnemy.GetComponent<EnemyMovement>().freezeDuration = freezeDuration;

                            // Pøehraje zvukový efekt zmrazení s nastavenou hlasitostí
                            if (freezeSound != null && audioManager != null)
                            {
                                freezeSound.volume = audioManager.GetSFXVolume(); // Nastaví hlasitost dle zvukového správce
                                freezeSound.Play(); // Pøehraje zvuk
                            }
                        }
                    }
                }
            }
            coolDownNum = coolDown - dorimeCoolDownBoost; // Nastavení cooldownu s bonusem
        }
        else
        {
            coolDownNum--; // Decrement cooldownu
        }
    }
}
