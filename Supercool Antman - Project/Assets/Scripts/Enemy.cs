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
    public int Health;
    [SerializeField] float minDistanceFromPlayer = 0;
    [SerializeField] float maxDistanceFromPlayer = 5;
    GameObject player;
    EnemyStates currentState;
    [SerializeField] int damage;
    [SerializeField] int pickupDropChance;
    Rigidbody2D rb;
    [SerializeField] GameObject[] bloodSplatterArray;
    GameObject tempBloodStain;
    int bloodSplattersInstantiated;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        Health = 100;
        currentState = EnemyStates.KeepDistanceFromPlayer;
        player = FindObjectOfType<PlayerInput>().gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        

        transform.rotation = (transform.position.x < player.transform.position.x ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0));

        switch (currentState)
        {
            case EnemyStates.KeepDistanceFromPlayer:

                if (CheckDeath())
                {
                    currentState = EnemyStates.Death;
                    break;
                }

                if (Vector2.Distance(transform.position, player.transform.position) > maxDistanceFromPlayer)
                {
                    rb.MovePosition(Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed));
                }
                else if (Vector2.Distance(transform.position, player.transform.position) < minDistanceFromPlayer)
                {
                    moveAwayFromPlayer();
                }
                else
                {
                    currentState = EnemyStates.Attack;
                }
                break;
            case EnemyStates.Attack:
                if (CheckDeath())
                {
                    currentState = EnemyStates.Death;
                    break;
                }
                AttackPlayer();
                /*print("attacking");*/
                currentState = EnemyStates.KeepDistanceFromPlayer;
                break;
            case EnemyStates.Death:
                EnemyDeath();
                Destroy(tempBloodStain, 0.5f);
                break;
            default:
                break;
        }
    }

    private void moveAwayFromPlayer()
    {
        Vector2 fleeDestination = (transform.position - player.transform.position).normalized;
        rb.MovePosition(Vector2.MoveTowards(transform.position, fleeDestination, movementSpeed / 2));
    }

    private bool CheckDeath()
    {
        if (Health <= 0)
        {
            
            return true;
        }
        return false;
    }


    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sword") || other.CompareTag("Lazer"))
        {
            *//*print(other.tag);*//*
            health -= other.GetComponent<Weapon>().Damage;

            CheckDeath();
        }
        else if (other.gameObject.GetComponent<PlayerStats>())
        {
            other.gameObject.GetComponent<PlayerStats>().ChangeHealth(-damage);

        }
    }*/

    void AttackPlayer()
    {
        minDistanceFromPlayer = 0;
        rb.MovePosition(Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * 8));
        anim.SetTrigger("Attack");
    }

    /* private void OnCollisionEnter2D(Collision2D other)
     {
         if (other.gameObject.CompareTag("Sword") || other.gameObject.CompareTag("Lazer"))
         {
             print(other.gameObject.tag);
             health -= other.gameObject.GetComponent<Weapon>().Damage;
             CheckDeath();
         }
         else if (other.gameObject.GetComponent<PlayerStats>())
         {
             other.gameObject.GetComponent<PlayerStats>().ChangeHealth(-damage);
         }
     }*/

    void EnemyDeath()
    {
        if (UnityEngine.Random.Range(0, 100) < pickupDropChance)
        {
            PickupManager.OnDropPickup(transform.position);
            Destroy(gameObject);
        }

        int randomBloodIndex = UnityEngine.Random.Range(0, 3);
        if (bloodSplattersInstantiated < 1)
        {
            tempBloodStain = Instantiate(bloodSplatterArray[randomBloodIndex], transform.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(-45, 45)));
            bloodSplattersInstantiated += 1;
        }
    }
}

enum EnemyStates
{
    KeepDistanceFromPlayer,
    Attack,
    Death
}
