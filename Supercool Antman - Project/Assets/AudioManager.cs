using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip[] enemyDeathSound;
    [SerializeField] AudioClip lazerSound;
    [SerializeField] AudioClip[] ouchSound;
    [SerializeField] AudioClip[] swordWhoosh;
    [SerializeField] AudioClip[] lightSaberSwoosh;
    [SerializeField] AudioClip[] swordHit;
    [SerializeField] AudioClip[] lightSaberHit;
    [SerializeField] AudioClip[] JumpSound;
    [SerializeField] AudioClip[] landingSound;



    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        EnemyManager.OnEnemyDeath += PlayEnemyDeathSound;
        PlayerStats.OnPlayerWasHit += PlayOuchSound;
        PlayerAnimatorController.OnLazerShot += PlayLazerSound;
        Weapon.OnEnemyWasHit += PlaySwordHitSound;
        Weapon.OnSwordSwoosh += PlaySwordWhooshSound;
        Weapon.OnLightSaberSwoosh += PlayLightSaberWhooshSound;
        PlayerInput.OnFLip += PlayJumpSound;
        WallScript.OnPlayerLand += PlayLandingSound;
    }
    private void OnDisable()
    {
        EnemyManager.OnEnemyDeath -= PlayEnemyDeathSound;
        PlayerStats.OnPlayerWasHit -= PlayOuchSound;
        PlayerAnimatorController.OnLazerShot -= PlayLazerSound;
        Weapon.OnEnemyWasHit -= PlaySwordHitSound;
        Weapon.OnSwordSwoosh -= PlaySwordWhooshSound;
        Weapon.OnLightSaberSwoosh -= PlayLightSaberWhooshSound;
        PlayerInput.OnFLip -= PlayJumpSound;
        WallScript.OnPlayerLand -= PlayLandingSound;
    }

    void PlayEnemyDeathSound(int unused)
    {
        audioSource.PlayOneShot(enemyDeathSound[(Random.Range(0,enemyDeathSound.Length))]);
    }

    void PlayOuchSound()
    {
        audioSource.PlayOneShot(ouchSound[Random.Range(0,ouchSound.Length)]);
    }

    void PlayLazerSound()
    {
        audioSource.PlayOneShot(lazerSound);
    }

    void PlaySwordWhooshSound()
    {
        audioSource.PlayOneShot(swordWhoosh[Random.Range(0,swordWhoosh.Length)]);
    }
    void PlayLightSaberWhooshSound()
    {
        audioSource.PlayOneShot(lightSaberSwoosh[Random.Range(0,lightSaberSwoosh.Length)]);
    }

    void PlaySwordHitSound()
    {
        audioSource.PlayOneShot(swordHit[Random.Range(0, swordHit.Length)]);
    }
    void PlayLightSaberHitSound()
    {
        audioSource.PlayOneShot(lightSaberHit[Random.Range(0, lightSaberHit.Length)]);
    }
    void PlayJumpSound()
    {
        audioSource.PlayOneShot(JumpSound[Random.Range(0, JumpSound.Length)]);
    }
    void PlayLandingSound()
    {
        audioSource.PlayOneShot(landingSound[Random.Range(0, landingSound.Length)]);
    }
}
