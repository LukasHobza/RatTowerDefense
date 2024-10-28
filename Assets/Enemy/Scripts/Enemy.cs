using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject armor;
    public int hp;
    public int armorHp;
    public bool isArmored;
    public int coinReward;
    private bool isDead = false;

    private void Awake()
    {
        if(isArmored)//pokud ma enemak armor
        {
            armor = this.transform.GetChild(0).gameObject;//prirazeni objektu armor
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet" && collision.GetComponent<Bullet>().active)//kolize nemaka se strelou
        {
            if(collision.gameObject.GetComponent<Bullet>().rangeDamage > 0) 
            {
                GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");

                foreach (GameObject enemyInList in enemyList)//projede vsechny enemaky
                {
                    if (enemyInList != null)
                    {
                        if (Vector2.Distance(enemyInList.transform.position, rb.transform.position) <= collision.gameObject.GetComponent<Bullet>().rangeDamage)//kdyz je nemy blizko veze
                        {
                            if (enemyInList.GetComponent<Enemy>().isArmored)//pro armorovaneho enemaka
                            {
                                if (enemyInList.GetComponent<Enemy>().armorHp > 0)//odebrani armoru
                                {
                                    enemyInList.GetComponent<Enemy>().armorHp -= collision.gameObject.GetComponent<Bullet>().armorDamage;
                                }
                                else if (enemyInList.GetComponent<Enemy>().armorHp <= 0)
                                {
                                    Destroy(enemyInList.GetComponent<Enemy>().armor);//mazanio armoru
                                    enemyInList.GetComponent<Enemy>().isArmored = false;
                                }
                            }
                            else
                            {
                                enemyInList.GetComponent<Enemy>().hp -= collision.gameObject.GetComponent<Bullet>().damage;//odebrani hp enema
                            }
                            collision.GetComponent<Bullet>().active = false;//aby strela davala damage jen jednou mam pocit ze to funguje xd 
                            GetComponent<EnemyMovement>().slowPower = collision.GetComponent<Bullet>().slowPower;//zpomaleni enemaka
                            GetComponent<EnemyMovement>().slowDuration = collision.GetComponent<Bullet>().slowDuration;//zpomaleni enemaka
                            Destroy(collision.gameObject);//zniceni kulky
                            if (enemyInList.GetComponent<Enemy>().hp <= 0 && !enemyInList.GetComponent<Enemy>().isDead)
                            {
                                enemyInList.GetComponent<Enemy>().isDead = true;
                                CoinManager.cM.coin += enemyInList.GetComponent<Enemy>().coinReward;
                                Destroy(enemyInList.GetComponent<Enemy>().gameObject);//zniceni enemaka
                            }
                        }
                    }
                }
            }
            else
            {
                if (isArmored)//pro armorovaneho enemaka
                {
                    if (armorHp > 0)//odebrani armoru
                    {
                        armorHp -= collision.gameObject.GetComponent<Bullet>().armorDamage;
                    }
                    else if (armorHp <= 0)
                    {
                        Destroy(armor);//mazanio armoru
                        isArmored = false;
                    }
                }
                else
                {
                    hp -= collision.gameObject.GetComponent<Bullet>().damage;//odebrani hp enema
                }
                collision.GetComponent<Bullet>().active = false;//aby strela davala damage jen jednou mam pocit ze to funguje xd 
                GetComponent<EnemyMovement>().slowPower = collision.GetComponent<Bullet>().slowPower;//zpomaleni enemaka
                GetComponent<EnemyMovement>().slowDuration = collision.GetComponent<Bullet>().slowDuration;//zpomaleni enemaka
                Destroy(collision.gameObject);//zniceni kulky
                if (hp <= 0 && !isDead)
                {
                    isDead = true;
                    CoinManager.cM.coin += coinReward;
                    Destroy(gameObject);//zniceni enemaka
                }
            }
        }
    }
}
