using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {

            hp -= collision.gameObject.GetComponent<Bullet>().damage;
            Destroy(collision.gameObject);
            if (hp <= 0) Destroy(gameObject);
        }
    }
}
