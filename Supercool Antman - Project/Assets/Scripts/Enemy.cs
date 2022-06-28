using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameManager gameManager;
    Vector3 nextTarget;
    bool hasTarget;
    [SerializeField] float movementSpeed;
    [SerializeField] Animator anim;
    int health;
    [SerializeField] float minDistanceFromPlayer = 0;
    [SerializeField] float maxDistanceFromPlayer = 5;
    GameObject player;
    EnemyStates currentState;
    [SerializeField] int damage;
    void Start()
    {
        health = 100;
        currentState = EnemyStates.KeepDistanceFromPlayer;
        player = FindObjectOfType<PlayerInput>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        switch (currentState)
        {
            case EnemyStates.KeepDistanceFromPlayer:

                CheckDeath();

                if (Vector2.Distance(transform.position, player.transform.position) > maxDistanceFromPlayer)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed);
                    print("too far");
                }
                else if (Vector2.Distance(transform.position, player.transform.position) < minDistanceFromPlayer)
                {
                    Vector2 fleeDestination = (transform.position - player.transform.position).normalized;
                    transform.position = Vector2.MoveTowards(transform.position, fleeDestination, movementSpeed / 2);
                }
                else
                {
                    currentState = EnemyStates.Attack;
                }
                break;
            case EnemyStates.Attack:
                AttackPlayer();
                print("attacking");
                currentState = EnemyStates.KeepDistanceFromPlayer;
                break;
            case EnemyStates.Death:
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            currentState = EnemyStates.Death;
        }
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

    private void OnCollisionEnter2D(Collision2D other)
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
        else if (other.gameObject.GetComponent<PlayerStats>())
        {
            other.gameObject.GetComponent<PlayerStats>().ChangeHealth(-damage);
        }
    }

}

enum EnemyStates
{
    KeepDistanceFromPlayer,
    Attack,
    Death
}
