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
    [SerializeField] Animator anim;
    int health;


    EnemyStates currentState;
    void Start()
    {
        health = 100;
        currentState = EnemyStates.KeepDistanceFromPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

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

    void AttackPlayer()
    {
        anim.SetTrigger("Attack");
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

enum EnemyStates
{
    KeepDistanceFromPlayer,
    Attack,
    Death
}
