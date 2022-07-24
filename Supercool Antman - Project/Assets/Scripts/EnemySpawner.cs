using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]GameObject[] enemyPrefabs;

    /*[SerializeField] float minSpawnTime;
    [SerializeField] float maxSpawnTime;*/
    [SerializeField] float beetleSpawnTime;
    float beetleSpawnTimer;
    [SerializeField] float beetleTimeReduction;
    [SerializeField] float mantisTimeReduction;

    [SerializeField] float mantisSpawnTime;
    float mantisSpawnTimer;



    /*private float beetleTimeToNextSpawn;*/
    private bool shouldSpawn = true;
    private GameManager gameManager;
    private Camera cam;

    private void OnEnable()
    {
        PlayerStats.OnPlayerDeath += ShouldntSpawn;
        EnemyManager.OnBeetleDeath += ReduceBeetleSpawnTime;
        EnemyManager.OnMantisDeath += ReduceMantisSpawnTime;
    }

    private void onDisable()
    {
        PlayerStats.OnPlayerDeath -= ShouldntSpawn;
        EnemyManager.OnBeetleDeath -= ReduceBeetleSpawnTime;
        EnemyManager.OnMantisDeath -= ReduceMantisSpawnTime;
    }

    private void Awake()
    {
        beetleSpawnTimer = beetleSpawnTime;
        mantisSpawnTimer = mantisSpawnTime;

        gameManager = FindObjectOfType<GameManager>();
        cam = Camera.main;

        /*ResetSpawnTime();*/
    }

    private void ShouldntSpawn()
    {
        shouldSpawn = false;
    }

    /*private void ResetSpawnTime()
    {
        beetleTimeToNextSpawn = Random.Range(minSpawnTime, maxSpawnTime);
    }*/

    // Update is called once per frame
    void Update()
    {
        /*beetleTimeToNextSpawn -= Time.deltaTime;*/
        beetleSpawnTimer -= Time.deltaTime;
        mantisSpawnTimer -= Time.deltaTime;

        if (beetleSpawnTimer <= 0)
        {
            if (shouldSpawn)
            {
                Spawn(0);
                beetleSpawnTimer = beetleSpawnTime;
            }
        }
        if (mantisSpawnTimer<= 0)
        {
            if (shouldSpawn)
            {
                Spawn(1);
                mantisSpawnTimer = mantisSpawnTime;
            }
        }
    }

    void ReduceMantisSpawnTime()
    {
        if (mantisSpawnTime >=0.1f)
        {
            mantisSpawnTime -= mantisTimeReduction;
        }
        else
        {
            mantisSpawnTime = 0.1f;
        }
    }
    void ReduceBeetleSpawnTime()
    {
        if (beetleSpawnTime>= 0.1f)
        {
            beetleSpawnTime -= beetleTimeReduction;
        }
        else
        {
            beetleSpawnTime = 0.1f;
        }
    }

    private void Spawn(int prefabNumber)
    {
        FindSpawnPoint(prefabNumber);

    }

    private void FindSpawnPoint(int prefabNumber)
    {
        float xSpawnPosition = Random.Range(gameManager.leftLimit.position.x, gameManager.rightLimit.position.x);
        float ySpawnPosition = Random.Range(gameManager.bottomLimit.position.y, gameManager.topLimit.position.y);
        Vector2 testSpawnPoint = new Vector2(xSpawnPosition, ySpawnPosition);
        RaycastHit2D[] raycastHit = Physics2D.RaycastAll(cam.transform.position, testSpawnPoint);
        for (int i = 0; i < raycastHit.Length; i++)
        {
            if (raycastHit[i].collider.CompareTag("Player"))
            {
                FindSpawnPoint(prefabNumber);
                break;
            }
        }
        Instantiate(enemyPrefabs[prefabNumber], testSpawnPoint, Quaternion.identity);
        /*ResetSpawnTime();*/
    }
}
