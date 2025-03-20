using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DorimeRatManager : MonoBehaviour
{
    public static DorimeRatManager dM;

    private int coolDownNum = 0;
    public int coolDown;
    public int towerMinCooldown;

    private void Awake()
    {
        dM = this;
    }

    public void FixedUpdate()
    {
        GameObject[] towerList = GameObject.FindGameObjectsWithTag("tower");//list vsech vezi krome dorime
        GameObject[] dorimeList = GameObject.FindGameObjectsWithTag("dorime");

        if (coolDownNum <= 0)
        {
            
            foreach (GameObject towerInList in towerList)
            {
                try
                {
                    towerInList.gameObject.GetComponent<FreezeTower>().dorimeCoolDownBoost = 0;
                    towerInList.gameObject.GetComponent<FreezeTower>().dorimeRangeBoost = 0;
                }
                catch
                {
                    
                }

                try
                {
                    towerInList.gameObject.GetComponent<RadarTower>().dorimeRangeBoost = 0;
                }
                catch
                {
                    
                }

                try
                {
                    towerInList.gameObject.GetComponent<Tower>().dorimeCoolDownBoost = 0;
                    towerInList.gameObject.GetComponent<Tower>().dorimeDamageBoost = 0;
                    towerInList.gameObject.GetComponent<Tower>().dorimeRangeBoost = 0;
                }
                catch
                {
                    
                }


                foreach (GameObject dorimeInList in dorimeList)
                {
                    if (Vector2.Distance(dorimeInList.transform.position, towerInList.transform.position) <= dorimeInList.gameObject.GetComponent<DorimeRat>().range)
                    {
                        try
                        {
                            towerInList.gameObject.GetComponent<FreezeTower>().dorimeCoolDownBoost += dorimeInList.gameObject.GetComponent<DorimeRat>().coolDownBoost;
                            towerInList.gameObject.GetComponent<FreezeTower>().dorimeRangeBoost += dorimeInList.gameObject.GetComponent<DorimeRat>().rangeBoost;
                            if (towerInList.gameObject.GetComponent<FreezeTower>().attackSpeed - towerInList.gameObject.GetComponent<FreezeTower>().dorimeCoolDownBoost < towerMinCooldown) towerInList.gameObject.GetComponent<FreezeTower>().dorimeCoolDownBoost = towerInList.gameObject.GetComponent<FreezeTower>().attackSpeed - towerMinCooldown;
                        }
                        catch
                        {
                            
                        }

                        try
                        {
                            towerInList.gameObject.GetComponent<RadarTower>().dorimeRangeBoost += dorimeInList.gameObject.GetComponent<DorimeRat>().rangeBoost;
                        }
                        catch
                        {
                            
                        }

                        try
                        {
                            print("TEST");
                            towerInList.gameObject.GetComponent<Tower>().dorimeCoolDownBoost += dorimeInList.gameObject.GetComponent<DorimeRat>().coolDownBoost;
                            towerInList.gameObject.GetComponent<Tower>().dorimeDamageBoost += dorimeInList.gameObject.GetComponent<DorimeRat>().damageBoost;
                            towerInList.gameObject.GetComponent<Tower>().dorimeRangeBoost += dorimeInList.gameObject.GetComponent<DorimeRat>().rangeBoost;
                            if (towerInList.gameObject.GetComponent<Tower>().attackSpeed - towerInList.gameObject.GetComponent<Tower>().dorimeCoolDownBoost < towerMinCooldown) towerInList.gameObject.GetComponent<Tower>().dorimeCoolDownBoost = towerInList.gameObject.GetComponent<Tower>().attackSpeed - towerMinCooldown;
                        }
                        catch
                        {
                            
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


        /*
        GameObject[] dorimeList = GameObject.FindGameObjectsWithTag("dorime"); // List všech nepøátel
        if (coolDownNum <= 0)
        {
            foreach (GameObject enemyInList in enemyList) // Projde všechny nepøátele
            {
                if (enemyInList != null)
                {
                    if (Vector2.Distance(enemyInList.transform.position, rb.transform.position) <= range) // Pokud je nepøítel blízko vìže
                    {
                        if (!enemyInList.gameObject.GetComponent<Enemy>().isInvisible)
                        {
                            targetEnemy = enemyInList; // Nastaví nového cílového nepøítele
                            targetEnemy.GetComponent<EnemyMovement>().freezeDuration = freezeDuration;
                        }
                    }
                }
            }
            coolDownNum = coolDown;
        }
        else
        {
            coolDownNum--;
        }*/
    }
}
