using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mantis : MonoBehaviour
{
    GameManager gameManager;

    [Range(0.01f, 0.2f)]
    [SerializeField] float mantisStopDistance;
    [SerializeField] float movementSpeed;
    [SerializeField]Animator anim;
    [SerializeField] Transform bodyRootRotation;
    [SerializeField] GameObject mantisHandsPrefab;
    [SerializeField] float mantisProjectileSpeed;
    [SerializeField] float weaponHitRadius;
    [SerializeField] Transform handsLocation;
    [SerializeField] float maxWalkDistance;
    [SerializeField] int damage;
    [SerializeField] int pickupDropChance;
    [SerializeField] GameObject[] bloodSplatterArray;
    [SerializeField] float chargeDistance = 3;
    [SerializeField] float chargeStopDistance = 1f;
    [SerializeField] float mantisMinWalkDistance;

    public int Health;

    private GameObject player;
    private MantisStates currentState;
    private GameObject tempBloodStain;
    private int bloodSplattersInstantiated;
    private Vector3[] positions = new Vector3[2];
    private Rigidbody2D rb;
    

    private void ChooseRandomPosition()
    {
        positions[1] = new Vector3(UnityEngine.Random.Range(gameManager.leftLimit.position.x, gameManager.rightLimit.position.x), UnityEngine.Random.Range(gameManager.topLimit.position.y, gameManager.bottomLimit.position.y));
        if (!ValidateRandomPosition())
        {
            ChooseRandomPosition();
        }
    }

    private bool ValidateRandomPosition()
    {
        if (Vector2.Distance(positions[0], positions[1]) < maxWalkDistance && Vector2.Distance(positions[0], positions[1]) > mantisMinWalkDistance)
        {
            return true;
        }
        return false;
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
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        Health = gameManager.GetComponent<EnemyManager>().mantisHealth;
        currentState = MantisStates.MoveToDestination;
        player = FindObjectOfType<PlayerInput>().gameObject;
        positions[0] = transform.position;
        ChooseRandomPosition();
        print(positions[0]);
        print(positions[1]);
        /*ValidateRandomPosition();*/
    }


    void MoveToDestination()
    {
        if (Vector2.Distance(transform.position, positions[1]) > mantisStopDistance)
        {
            anim.SetBool("IsCrawling", true);
            rb.MovePosition(Vector2.MoveTowards(transform.position, positions[1], movementSpeed));
        }
        else
        {
            anim.SetBool("IsCrawling", false);
            currentState = MantisStates.Attack;
            Vector3 tempPosition = positions[0];
            positions[0] = positions[1];
            positions[1] = tempPosition;
        }
    }


    void Update()
    {
        print(player.name);
        transform.rotation = (transform.position.x < player.transform.position.x ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0));

        switch (currentState)
        {
            case MantisStates.MoveToDestination:
                {
                    if (CheckDeath())
                    {
                        currentState = MantisStates.Death;
                        break;
                    }
                    MoveToDestination();
                    break;
                }
            case MantisStates.Attack:
                if (CheckDeath())
                {
                    currentState = MantisStates.Death;
                    break;
                }

                AttackPlayer();
                currentState = MantisStates.MoveToDestination;
                break;

            case MantisStates.Death:
                EnemyDeath();

                break;
            case MantisStates.Victory:
                Victory();
                break;
            default:
                break;
        }
    }

    
    private bool CheckDeath()
    {
        if (Health <= 0)
        {
            return true;
        }
        return false;
    }

    
    private float GetDistanceToPlayer()
    {
        return Vector2.Distance(transform.position, player.transform.position);
    }

    void AttackPlayer()
    {
        /*float directionToPlayer = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;*/
        Rigidbody2D hands = Instantiate(mantisHandsPrefab, handsLocation.position, bodyRootRotation.rotation).GetComponent<Rigidbody2D>();
        hands.AddForce((player.transform.position - hands.transform.position).normalized * mantisProjectileSpeed, ForceMode2D.Impulse);
        anim.SetTrigger("Attack");
        /*currentState = EnemyStates.KeepDistanceFromPlayer;*/
    }

    void DoDamageToPlayer()
    {
        if (GetDistanceToPlayer() < weaponHitRadius)
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

        EnemyManager.OnEnemyDeath?.Invoke(1);
        Destroy(tempBloodStain, 0.7f);
        Destroy(gameObject);
    }

    void SetCurrentStateToVictory()
    {
        currentState = MantisStates.Victory;
    }
}

enum MantisStates
{
    MoveToDestination,
    Attack,
    Death,
    Victory
}

