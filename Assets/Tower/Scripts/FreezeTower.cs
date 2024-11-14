using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FreezeTower : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    public Rigidbody2D rb;
    private int coolDownNum = 0;
    public int coolDown;
    public int range;
    public int freezeDuration;
    private GameObject targetEnemy;

    [SerializeField] private AudioSource freezeSound; // P�idan� prom�nn� pro zvukov� efekt zmrazen�

    void FixedUpdate()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy"); // List v�ech nep��tel
        if (coolDownNum <= 0)
        {
            foreach (GameObject enemyInList in enemyList) // Projde v�echny nep��tele
            {
                if (enemyInList != null)
                {
                    if (Vector2.Distance(enemyInList.transform.position, rb.transform.position) <= range) // Pokud je nep��tel bl�zko v�e
                    {
                        if (!enemyInList.gameObject.GetComponent<Enemy>().isInvisible)
                        {
                            targetEnemy = enemyInList; // Nastav� nov�ho c�lov�ho nep��tele
                            targetEnemy.GetComponent<EnemyMovement>().freezeDuration = freezeDuration;

                            // P�ehraje zvukov� efekt zmrazen�
                            if (freezeSound != null)
                            {
                                freezeSound.Play();
                            }
                        }
                    }
                }
            }
            coolDownNum = coolDown;
        }
        else
        {
            coolDownNum--;
        }
    }
}
