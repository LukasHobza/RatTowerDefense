using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarTower : MonoBehaviour
{
    public Sprite sprite;
    public Rigidbody2D rb;
    private int coolDownNum = 0;
    public int coolDown;
    public int range;
    private GameObject targetEnemy;

    [SerializeField] private AudioSource ratdarSound;

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
                        if (targetEnemy.gameObject.GetComponent<Enemy>().isInvisible)
                        {
                            targetEnemy.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
                            targetEnemy.gameObject.GetComponent<Enemy>().isInvisible = false;

                            if (ratdarSound != null)
                            {
                                ratdarSound.Play();
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
