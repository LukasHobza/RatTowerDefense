using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    public Rigidbody2D rb;
    private int coolDownNum = 0;
    public int coolDown;
    public int range;
    private GameObject targetEnemy;

    void FixedUpdate()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");//list vsech enemy

        if(targetEnemy == null || Vector2.Distance(targetEnemy.transform.position, rb.transform.position) >= range)//pokud vez nema target enemaka tak si nejakeho najde
        {
            foreach (GameObject enemyInList in enemyList)//projede vsechny enemaky
            {
                if (enemyInList != null)
                {
                    if (Vector2.Distance(enemyInList.transform.position, rb.transform.position) <= range)//kdyz je nemy blizko veze
                    {
                        targetEnemy = enemyInList;//priradi noveho target enemaka
                        break;
                    }
                }
            }
        }
        else
        {
            Vector2 direction = (targetEnemy.GetComponent<EntityMovement>().rb.transform.position - transform.position);//zjisteni rotace pro vez
            Vector3 targetForwardDirection = direction;
            Quaternion targetRotation = Quaternion.LookRotation(targetForwardDirection);
            if (targetRotation.y > 0.5f) targetRotation = Quaternion.Euler(0f, 0f, -180f) * targetRotation;//oprava divne rotace xd
            rb.MoveRotation(targetRotation);//nastaveni rotace pro vez

            if (coolDownNum <= 0)
            {
                GameObject bulletToSpawn = bullet;
                bulletToSpawn.GetComponent<Bullet>().targetForwardDirection = targetForwardDirection;//nastaveni veci pro budouci strely
                bulletToSpawn.GetComponent<Bullet>().target = targetEnemy.transform;//nastaveni veci pro budouci strely
                Instantiate(bulletToSpawn, this.transform.position, Quaternion.identity);//spawn strely
                coolDownNum = coolDown;//nastaveni casovace
            }
            else coolDownNum--;
        }
        
    }
}
