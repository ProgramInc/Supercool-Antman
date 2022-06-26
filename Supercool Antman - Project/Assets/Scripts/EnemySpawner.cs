using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]GameObject[] enemyPrefabs;

    [SerializeField] float minSpawnTime;
    [SerializeField] float maxSpawnTime;
    float timeToNextSpawn;

    GameManager gameManager;

    Camera cam;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        cam = Camera.main;

        ResetSpawnTime();
    }

    private void ResetSpawnTime()
    {
        timeToNextSpawn = Random.Range(minSpawnTime, maxSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        timeToNextSpawn -= Time.deltaTime;
        if (timeToNextSpawn <= 0)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        FindSpawnPoint();

    }

    private void FindSpawnPoint()
    {
        float xSpawnPosition = Random.Range(gameManager.leftLimit.position.x, gameManager.rightLimit.position.x);
        float ySpawnPosition = Random.Range(gameManager.bottomLimit.position.y, gameManager.topLimit.position.y);
        Vector2 testSpawnPoint = new Vector2(xSpawnPosition, ySpawnPosition);
        RaycastHit2D[] raycastHit = Physics2D.RaycastAll(cam.transform.position, testSpawnPoint);
        for (int i = 0; i < raycastHit.Length; i++)
        {
            if (raycastHit[i].collider.CompareTag("Player"))
            {
                FindSpawnPoint();
                break;
            }
        }
        Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], testSpawnPoint, Quaternion.identity);
        ResetSpawnTime();
    }
}
