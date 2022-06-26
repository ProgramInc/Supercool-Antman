using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameManager gameManager;
    Vector3 nextTarget;
    bool hasTarget;
    [SerializeField] float speed;
    int health;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasTarget)
        {
            Vector2.MoveTowards(transform.position, nextTarget, speed);
        }
        else
        {
            /*FindTarget();*/
        }
    }

    /* private void FindTarget()
     {
         nextTarget = 
     }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sword") || other.CompareTag("Lazer"))
        {
            print(other.tag);
            health -= other.GetComponent<Weapon>().Damage;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    /*private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Sword") || other.gameObject.CompareTag("Lazer"))
        {
            print(other.gameObject.tag);
            health -= other.gameObject.GetComponent<Weapon>().Damage;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }*/

}
