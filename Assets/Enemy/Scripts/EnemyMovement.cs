using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    private int pathIndex = 0;
    public int speed;
    private Vector3 direction;
    private Transform path;
    private float newSpeed;

    public int slowPower ;
    public int slowDuration;

    public int freezeDuration;

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
                HpManager.hM.hp -= gameObject.GetComponent<Enemy>().damage;
                if(HpManager.hM.hp <= 0) SceneManager.LoadScene(sceneName: "Game");
                Destroy(gameObject);//spazani enemaka pokud je na konci cesty
                return;
            }
            SetPath();
        }
    }

    private void FixedUpdate()
    {
        print(freezeDuration);
        if(freezeDuration > 0)
        {
            freezeDuration--;
            newSpeed = 0.1f;
        }
        else if(slowDuration > 0)//zpomaleni enemaka
        {
            slowDuration--;
            newSpeed = speed / slowPower;
        }
        else
        {
            newSpeed = speed;
        }
        
        direction = (path.position - transform.position).normalized;//zjisteni smeru pohybu enemaka
        rb.linearVelocity = direction * newSpeed * Time.deltaTime;//pohyb enemaka
        
        Vector3 targetForwardDirection = rb.linearVelocity;
        Quaternion targetRotation = Quaternion.LookRotation(targetForwardDirection);
        if (targetRotation.y > 0.5f) targetRotation = Quaternion.Euler(0f, 0f, -180f) * targetRotation;//oprava divne rotace xd
        rb.MoveRotation(targetRotation);//nastaveni rotace enemaka
    }

    private void SetPath()
    {
        path = PathHolder.pH.path[pathIndex];//nastaveni cesty
    }
}
