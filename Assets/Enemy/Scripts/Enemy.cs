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

    private void Awake()
    {
        if(isArmored) 
        {
            armor = this.transform.GetChild(0).gameObject;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet" && collision.GetComponent<Bullet>().active)
        {
            if(isArmored)
            {
                if (armorHp > 0)
                {
                    armorHp -= collision.gameObject.GetComponent<Bullet>().armorDamage;
                }
                else if (armorHp <= 0)
                {
                    Destroy(armor);
                    isArmored = false;
                }
            }
            else
            {
                hp -= collision.gameObject.GetComponent<Bullet>().damage;
            }
            collision.GetComponent<Bullet>().active = false;
            GetComponent<EntityMovement>().slowPower = collision.GetComponent<Bullet>().slowPower;
            GetComponent<EntityMovement>().slowDuration = collision.GetComponent<Bullet>().slowDuration;
            Destroy(collision.gameObject);
            //print("Hp:" + hp + ", ArmorHp:" + armorHp);
            if (hp <= 0) Destroy(gameObject);
        }
    }
}
