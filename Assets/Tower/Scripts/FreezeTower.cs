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

    void FixedUpdate()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");//list vsech enemy
        if (coolDownNum <= 0)
        {
            foreach (GameObject enemyInList in enemyList)//projede vsechny enemaky
            {
                if (enemyInList != null)
                {
                    if (Vector2.Distance(enemyInList.transform.position, rb.transform.position) <= range)//kdyz je nemy blizko veze
                    {
                        targetEnemy = enemyInList;//priradi noveho target enemaka
                        targetEnemy.GetComponent<EnemyMovement>().freezeDuration = freezeDuration;
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
