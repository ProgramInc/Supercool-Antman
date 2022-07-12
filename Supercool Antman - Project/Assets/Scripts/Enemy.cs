using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void EnemyDeathAction();
    public static EnemyDeathAction OnEnemyDeath;

    GameManager gameManager;
    Vector3 nextTarget;
    bool hasTarget;
    [SerializeField] float movementSpeed;
    [SerializeField] Animator anim;
    public int Health;
    [SerializeField] float minDistanceFromPlayer;
    /*[SerializeField] float maxDistanceFromPlayer = 5;*/
    [SerializeField] float chargeDistance = 3;
    [SerializeField] float chargeStopDistance = 1f;
    GameObject player;
    EnemyStates currentState;
    [SerializeField] int damage;
    [SerializeField] int pickupDropChance;
    Rigidbody2D rb;
    [SerializeField] GameObject[] bloodSplatterArray;
    GameObject tempBloodStain;
    int bloodSplattersInstantiated;
    [SerializeField] float weaponHitRadius;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        PlayerStats.OnPlayerDeath += SetCurrentStateToVictory;
    }

    private void OnDisable()
    {
        PlayerStats.OnPlayerDeath -= SetCurrentStateToVictory;
    }
    void Start()
    {
        Health = 100;
        currentState = EnemyStates.KeepDistanceFromPlayer;
        player = FindObjectOfType<PlayerInput>().gameObject;

    }

    void KeepDistanceFromPlayer()
    {
        if (Vector2.Distance(transform.position, player.transform.position) > chargeDistance)
        {
            anim.SetBool("IsCrawling", true);
            MoveTowardsPlayer();
        }
        else if (Vector2.Distance(transform.position, player.transform.position) < chargeStopDistance)
        {
            if (GetDistanceToPlayer() < minDistanceFromPlayer)
            {
                anim.SetBool("IsCrawling", true);
                moveAwayFromPlayer(); 
            }
            else
            {
                anim.SetBool("IsCrawling", false);
                currentState = EnemyStates.Attack;
            }
        }
        
        else
        {
            anim.SetBool("IsCrawling", false);
            currentState = EnemyStates.Charge;
        }
    }



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

                KeepDistanceFromPlayer();
                break;
            case EnemyStates.Charge:

                if (CheckDeath())
                {
                    currentState = EnemyStates.Death;
                    break;
                }


                ChargePlayer();
                break;

            case EnemyStates.Attack:
                if (CheckDeath())
                {
                    currentState = EnemyStates.Death;
                    break;
                }
                else if (GetDistanceToPlayer() > chargeDistance || GetDistanceToPlayer() < minDistanceFromPlayer)
                {
                    currentState = EnemyStates.KeepDistanceFromPlayer;
                    break;
                }
                
                AttackPlayer();
                currentState = EnemyStates.KeepDistanceFromPlayer;
                break;

            case EnemyStates.Death:
                EnemyDeath();

                break;
            case EnemyStates.Victory:
                Victory();
                break;
            default:
                break;
        }
    }

    /*void Update()
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
                    MoveTowardsPlayer();
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
                else if (Vector2.Distance(transform.position, player.transform.position) > maxDistanceFromPlayer || Vector2.Distance(transform.position, player.transform.position) < minDistanceFromPlayer)
                {
                    currentState = EnemyStates.KeepDistanceFromPlayer;
                    break;
                }
                ChargePlayer();
                *//*print("attacking");*//*
                currentState = EnemyStates.KeepDistanceFromPlayer;
                break;
            case EnemyStates.Death:
                EnemyDeath();

                break;
            default:
                break;
        }
    }*/

    private void MoveTowardsPlayer()
    {
        rb.MovePosition(Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed));
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

    void ChargePlayer()
    {
        /*minDistanceFromPlayer = 1;*/
        if (GetDistanceToPlayer() > chargeStopDistance && GetDistanceToPlayer() <= chargeDistance)
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * 8));
            anim.SetBool("IsCharging", true);
            return;
        }
        else if (GetDistanceToPlayer() > chargeDistance)
        {
            currentState = EnemyStates.KeepDistanceFromPlayer;
            anim.SetBool("IsCharging", false);
            return;
        }
        anim.SetBool("IsCharging", false);
        currentState = EnemyStates.Attack;
    }

    private float GetDistanceToPlayer()
    {
        return Vector2.Distance(transform.position, player.transform.position);
    }

    void AttackPlayer()
    {
        anim.SetTrigger("Attack");
        /*currentState = EnemyStates.KeepDistanceFromPlayer;*/
    }

    void DoDamageToPlayer()
    {
        if (GetDistanceToPlayer()<weaponHitRadius)
        {
            PlayerStats playerStats = player.GetComponentInChildren<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.ChangeHealth(-damage);
                playerStats.InstantiateOuchCloud();
            }
        }
    }

    void Victory()
    {
        anim.SetBool("IsCrawling", false);
        anim.SetBool("IsCharging", false);
        anim.SetTrigger("Victory");
    }

    void EnemyDeath()
    {
        if (UnityEngine.Random.Range(0, 100) < pickupDropChance)
        {
            PickupManager.OnDropPickup(transform.position);
        }

        int randomBloodIndex = UnityEngine.Random.Range(0, 3);
        if (bloodSplattersInstantiated < 1)
        {
            tempBloodStain = Instantiate(bloodSplatterArray[randomBloodIndex], transform.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(-45, 45)));
            bloodSplattersInstantiated += 1;
        }

        OnEnemyDeath?.Invoke();
        Destroy(tempBloodStain, 0.7f);
        Destroy(gameObject);
    }

    void SetCurrentStateToVictory()
    {
        currentState = EnemyStates.Victory;
    }
}

enum EnemyStates
{
    KeepDistanceFromPlayer,
    Charge,
    Attack,
    Victory,
    Death
}
