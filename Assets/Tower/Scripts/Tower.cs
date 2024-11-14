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
    public int type;

    public int damage;
    public int armorDamage;
    public int slowPower;
    public int slowDuration;
    public int rangeDamage;

    [SerializeField] private AudioSource shootSound; // Pøidaná promìnnou pro zvukový efekt støelby

    void FixedUpdate()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy"); // list všech enemy

        // Pokud vìž nemá target, najde si nového
        if (targetEnemy == null || Vector2.Distance(targetEnemy.transform.position, rb.transform.position) >= range)
        {
            foreach (GameObject enemyInList in enemyList) // Projde všechny nepøátele
            {
                if (enemyInList != null)
                {
                    if (Vector2.Distance(enemyInList.transform.position, rb.transform.position) <= range) // Pokud je nepøítel blízko vìže
                    {
                        if (!enemyInList.gameObject.GetComponent<Enemy>().isInvisible)
                        {
                            targetEnemy = enemyInList; // Nastaví nového targeta
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            // Rotace vìže k nepøíteli
            Vector2 direction = (targetEnemy.GetComponent<EnemyMovement>().rb.transform.position - transform.position);
            Vector3 targetForwardDirection = direction;
            Quaternion targetRotation = Quaternion.LookRotation(targetForwardDirection);
            if (targetRotation.y > 0.5f) targetRotation = Quaternion.Euler(0f, 0f, -180f) * targetRotation;
            rb.MoveRotation(targetRotation);

            // Kontrola cooldownu støelby
            if (coolDownNum <= 0)
            {
                GameObject bulletToSpawn = bullet;
                bulletToSpawn.GetComponent<Bullet>().targetForwardDirection = targetForwardDirection;
                bulletToSpawn.GetComponent<Bullet>().target = targetEnemy.transform;
                Instantiate(bulletToSpawn, this.transform.position, Quaternion.identity); // Spawne støelu

                // Nastavení hodnot støely
                bulletToSpawn.GetComponent<Bullet>().damage = damage;
                bulletToSpawn.GetComponent<Bullet>().armorDamage = armorDamage;
                bulletToSpawn.GetComponent<Bullet>().slowPower = slowPower;
                bulletToSpawn.GetComponent<Bullet>().slowDuration = slowDuration;
                bulletToSpawn.GetComponent<Bullet>().rangeDamage = rangeDamage;

                // Pøehraje zvukový efekt støelby
                if (shootSound != null)
                {
                    shootSound.Play();
                }

                // Resetuje cooldown
                coolDownNum = coolDown;
            }
            else coolDownNum--;
        }
    }
}
