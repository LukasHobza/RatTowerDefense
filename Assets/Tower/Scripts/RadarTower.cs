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
    private AudioManager audioManager; // P�edpokl�d�me, �e m� tento spr�vce zvuku

    void Start()
    {
        // Z�sk�me odkaz na AudioManager (pokud existuje)
        audioManager = FindObjectOfType<AudioManager>(); // Pokud pou��v� singleton
    }

    void FixedUpdate()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy"); // seznam v�ech nep��tel
        if (coolDownNum <= 0)
        {
            foreach (GameObject enemyInList in enemyList) // projede v�echny nep��tele
            {
                if (enemyInList != null)
                {
                    if (Vector2.Distance(enemyInList.transform.position, rb.transform.position) <= (range + dorimeRangeBoost)) // pokud je nep��tel bl�zko v�e
                    {
                        targetEnemy = enemyInList; // p�i�ad� nov�ho nep��tele
                        if (targetEnemy.gameObject.GetComponent<Enemy>().isInvisible)
                        {
                            targetEnemy.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
                            targetEnemy.gameObject.GetComponent<Enemy>().isInvisible = false;

                            if (ratdarSound != null && audioManager != null)
                            {
                                ratdarSound.volume = audioManager.GetSFXVolume(); // Nastav� hlasitost dle zvukov�ho spr�vce
                                ratdarSound.Play(); // P�ehraje zvuk
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
