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
        rb.SetRotation(0);//to tu asi nemusi byt
        SetPath();//nastaveni prvniho bodu cesty enemaka
    }

    private void Update()
    {
        if(Vector2.Distance(path.position, transform.position) <= 0.5f){//nastaveni dalsiho bodu enemaka
            pathIndex++;
            if(pathIndex >= PathHolder.pH.path.Length)
            {
                Destroy(gameObject);//spazani enemaka pokud je na konci cesty
                return;
            }
            SetPath();
        }
    }

    private void FixedUpdate()
    {
        if(slowDuration > 0)//zpomaleni enemaka
        {
            slowDuration--;
            newSpeed = speed / slowPower;
        }
        else
        {
            newSpeed = speed;
        }
        
        direction = (path.position - transform.position).normalized;//zjisteni smeru pohybu enemaka
        rb.velocity = direction * newSpeed * Time.deltaTime;//pohyb enemaka

        Vector3 targetForwardDirection = rb.velocity;
        Quaternion targetRotation = Quaternion.LookRotation(targetForwardDirection);
        rb.MoveRotation(targetRotation);//nastaveni rotace enemaka
    }

    private void SetPath()
    {
        path = PathHolder.pH.path[pathIndex];//nastaveni cesty
    }
}
