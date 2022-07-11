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
        Enemy.OnEnemyDeath += PlayEnemyDeathSound;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDeath -= PlayEnemyDeathSound;
    }

    void PlayEnemyDeathSound()
    {
        audioSource.PlayOneShot(enemyDeathSound);
    }
}
