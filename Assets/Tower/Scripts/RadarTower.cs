using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarTower : Tower // ✅ Dědí z Tower
{
    public Sprite sprite;
    private int coolDownNum = 0;
    private GameObject targetEnemy;
    
    [SerializeField] private AudioSource radarSound;
    [HideInInspector] private AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void FixedUpdate()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");

        if (coolDownNum <= 0)
        {
            foreach (GameObject enemyInList in enemyList)
            {
                if (enemyInList != null)
                {
                    if (Vector2.Distance(enemyInList.transform.position, rb.transform.position) <= (range + dorimeRangeBoost))
                    {
                        targetEnemy = enemyInList;
                        if (targetEnemy.gameObject.GetComponent<Enemy>().isInvisible)
                        {
                            targetEnemy.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
                            targetEnemy.gameObject.GetComponent<Enemy>().isInvisible = false;

                            if (radarSound != null && audioManager != null)
                            {
                                radarSound.volume = audioManager.GetSFXVolume();
                                radarSound.Play();
                            }
                        }
                    }
                }
            }
            coolDownNum = attackSpeed;
        }
        else
        {
            coolDownNum--;
        }
    }

    // 📌 Kliknutí na věž → otevře menu
   
}

