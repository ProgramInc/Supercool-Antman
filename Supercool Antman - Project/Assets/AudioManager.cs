using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip enemyDeathSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        EnemyManager.OnEnemyDeath += PlayEnemyDeathSound;
    }

    private void OnDisable()
    {
        EnemyManager.OnEnemyDeath -= PlayEnemyDeathSound;
    }

    void PlayEnemyDeathSound(int unused)
    {
        audioSource.PlayOneShot(enemyDeathSound);
    }
}
