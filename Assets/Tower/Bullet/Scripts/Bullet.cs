using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    private Vector3 direction;
    public Transform target;
    private int hp = 20;
    public bool active = true;
    public Vector3 targetForwardDirection;

    public int damage;
    public int armorDamage;
    public int slowPower;
    public int slowDuration;

    private void Start()
    {
        direction = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetForwardDirection);
        rb.MoveRotation(targetRotation);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + direction * Time.deltaTime * speed);
        hp--;
        if(hp <= 0)Destroy(gameObject);
    }
}
