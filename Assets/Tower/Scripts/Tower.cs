using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    public Rigidbody2D rb;
    private int coolDown = 0;

    void FixedUpdate()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject go in enemyList)
        {
            if (go != null)
            {
                if (Vector2.Distance(go.transform.position, rb.transform.position) <= 5)
                {
                    Vector2 direction = (go.GetComponent<EntityMovement>().rb.transform.position - transform.position).normalized;

                    Vector3 targetForwardDirection = direction;
                    Quaternion targetRotation = Quaternion.LookRotation(targetForwardDirection);
                    rb.MoveRotation(targetRotation);

                    if (coolDown <= 0)
                    {
                        GameObject bulletToSpawn = bullet;
                        bulletToSpawn.GetComponent<Bullet>().target = go.transform;
                        Instantiate(bulletToSpawn, this.transform.position, Quaternion.identity);
                        coolDown = 100;
                    }
                    else coolDown--;
                    break;
                }
            }
        }
    }
}
