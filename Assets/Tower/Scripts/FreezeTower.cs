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

    [SerializeField] private AudioSource freezeSound; // Pøidaná promìnná pro zvukový efekt zmrazení

    void FixedUpdate()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy"); // List všech nepøátel
        if (coolDownNum <= 0)
        {
            foreach (GameObject enemyInList in enemyList) // Projde všechny nepøátele
            {
                if (enemyInList != null)
                {
                    if (Vector2.Distance(enemyInList.transform.position, rb.transform.position) <= range) // Pokud je nepøítel blízko vìže
                    {
                        if (!enemyInList.gameObject.GetComponent<Enemy>().isInvisible)
                        {
                            targetEnemy = enemyInList; // Nastaví nového cílového nepøítele
                            targetEnemy.GetComponent<EnemyMovement>().freezeDuration = freezeDuration;

                            // Pøehraje zvukový efekt zmrazení
                            if (freezeSound != null)
                            {
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
}
