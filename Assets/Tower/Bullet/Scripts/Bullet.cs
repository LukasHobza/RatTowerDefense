using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    private Vector3 direction;
    public Transform target;
    private int hp = 20;

    public int damage;
    public int slowPower;
    public int slowDuration;

    private void Start()
    {
        direction = (target.position - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * speed * Time.deltaTime;
        Vector3 targetForwardDirection = rb.velocity;
        Quaternion targetRotation = Quaternion.LookRotation(targetForwardDirection);
        rb.MoveRotation(targetRotation);
        hp--;
        if(hp <= 0)Destroy(gameObject);
    }
}
