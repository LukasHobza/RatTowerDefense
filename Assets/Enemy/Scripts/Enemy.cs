using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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
            if(isArmored)//pro armorovaneho enemaka
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
