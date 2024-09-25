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
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");

        if(targetEnemy == null || Vector2.Distance(targetEnemy.transform.position, rb.transform.position) >= range)
        {
            foreach (GameObject enemyInList in enemyList)
            {
                print("a");
                if (enemyInList != null)
                {
                    if (Vector2.Distance(enemyInList.transform.position, rb.transform.position) <= range)
                    {
                        targetEnemy = enemyInList;
                        break;
                    }
                }
            }
        }
        else
        {
            Vector2 direction = (targetEnemy.GetComponent<EntityMovement>().rb.transform.position - transform.position);
            Vector3 targetForwardDirection = direction;
            Quaternion targetRotation = Quaternion.LookRotation(targetForwardDirection);
            if (targetRotation.y > 0.5f) targetRotation = Quaternion.Euler(0f, 0f, -180f) * targetRotation;
            rb.MoveRotation(targetRotation);

            if (coolDownNum <= 0)
            {
                GameObject bulletToSpawn = bullet;
                bulletToSpawn.GetComponent<Bullet>().targetForwardDirection = targetForwardDirection;
                bulletToSpawn.GetComponent<Bullet>().target = targetEnemy.transform;
                Instantiate(bulletToSpawn, this.transform.position, Quaternion.identity);
                coolDownNum = coolDown;
            }
            else coolDownNum--;
        }
        
    }
}
