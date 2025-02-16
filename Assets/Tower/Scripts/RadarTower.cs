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

    public int dorimeRangeBoost = 0;

    [SerializeField] private AudioSource ratdarSound;
    private AudioManager audioManager; // Pøedpokládáme, že máš tento správce zvuku

    void Start()
    {
        // Získáme odkaz na AudioManager (pokud existuje)
        audioManager = FindObjectOfType<AudioManager>(); // Pokud používáš singleton
    }

    void FixedUpdate()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy"); // seznam všech nepøátel
        if (coolDownNum <= 0)
        {
            foreach (GameObject enemyInList in enemyList) // projede všechny nepøátele
            {
                if (enemyInList != null)
                {
                    if (Vector2.Distance(enemyInList.transform.position, rb.transform.position) <= (range + dorimeRangeBoost)) // pokud je nepøítel blízko vìže
                    {
                        targetEnemy = enemyInList; // pøiøadí nového nepøítele
                        if (targetEnemy.gameObject.GetComponent<Enemy>().isInvisible)
                        {
                            targetEnemy.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
                            targetEnemy.gameObject.GetComponent<Enemy>().isInvisible = false;

                            if (ratdarSound != null && audioManager != null)
                            {
                                ratdarSound.volume = audioManager.GetSFXVolume(); // Nastaví hlasitost dle zvukového správce
                                ratdarSound.Play(); // Pøehraje zvuk
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
