using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    private int pathIndex = 0;
    public int speed;
    private Vector3 direction;
    private Transform path;
    private int newSpeed;

    public int slowPower ;
    public int slowDuration;

    private void Start()
    {
        slowPower = 1;
        rb.SetRotation(0);
        SetPath();
    }

    private void Update()
    {
        if(Vector2.Distance(path.position, transform.position) <= 0.5f){
            pathIndex++;
            if(pathIndex >= PathHolder.pH.path.Length)
            {
                Destroy(gameObject);
                return;
            }
            SetPath();
        }
    }

    private void FixedUpdate()
    {
        print(slowDuration);
        if(slowDuration > 0)
        {
            slowDuration--;
            newSpeed = speed / slowPower;
        }
        else
        {
            newSpeed = speed;
        }
        
        direction = (path.position - transform.position).normalized;
        rb.velocity = direction * newSpeed * Time.deltaTime;

        Vector3 targetForwardDirection = rb.velocity;
        Quaternion targetRotation = Quaternion.LookRotation(targetForwardDirection);
        rb.MoveRotation(targetRotation);
    }

    private void SetPath()
    {
        path = PathHolder.pH.path[pathIndex];
    }
}
