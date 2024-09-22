using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet" && collision.GetComponent<Bullet>().active)
        {
            collision.GetComponent<Bullet>().active = false;
            hp -= collision.gameObject.GetComponent<Bullet>().damage;
            GetComponent<EntityMovement>().slowPower = collision.GetComponent<Bullet>().slowPower;
            GetComponent<EntityMovement>().slowDuration = collision.GetComponent<Bullet>().slowDuration;
            Destroy(collision.gameObject);
            if (hp <= 0) Destroy(gameObject);
        }
    }
}
