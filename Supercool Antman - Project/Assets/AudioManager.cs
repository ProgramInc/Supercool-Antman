using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip[] enemyDeathSound;
    [SerializeField] AudioClip[] lazerSound;
    [SerializeField] AudioClip[] ouchSound;
    [SerializeField] AudioClip[] swordWhoosh;
    [SerializeField] AudioClip[] lightSaberSwoosh;
    [SerializeField] AudioClip[] swordHit;
    [SerializeField] AudioClip[] lightSaberHit;
    [SerializeField] AudioClip[] JumpSound;
    [SerializeField] AudioClip[] landingSound;
    [SerializeField] AudioClip[] chewSounds;
    [SerializeField] AudioClip[] energyCollectedSounds;
    [SerializeField] AudioClip lazerImpactSound;


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
        PlayerAnimatorController.OnSwordSwoosh += PlaySwordWhooshSound;
        PlayerAnimatorController.OnLightSaberSwoosh += PlayLightSaberWhooshSound;
        PlayerInput.OnFLip += PlayJumpSound;
        WallScript.OnPlayerLand += PlayLandingSound;
        PlayerStats.OnLifeCollcted += PlayChewingSound;
        PlayerStats.OnEnergyColleted += PlayEnergyCollectedSound;
        Lazer.OnLazerImpact += PlayLazerImpactSound; 
    }

    private void OnDisable()
    {
        EnemyManager.OnEnemyDeath -= PlayEnemyDeathSound;
        PlayerStats.OnPlayerWasHit -= PlayOuchSound;
        PlayerAnimatorController.OnLazerShot -= PlayLazerSound;
        Weapon.OnEnemyWasHit -= PlaySwordHitSound;
        PlayerAnimatorController.OnSwordSwoosh -= PlaySwordWhooshSound;
        PlayerAnimatorController.OnLightSaberSwoosh -= PlayLightSaberWhooshSound;
        PlayerInput.OnFLip -= PlayJumpSound;
        WallScript.OnPlayerLand -= PlayLandingSound;
        PlayerStats.OnLifeCollcted -= PlayChewingSound;
        PlayerStats.OnEnergyColleted -= PlayEnergyCollectedSound;
        Lazer.OnLazerImpact -= PlayLazerImpactSound;
    }


    void PlayEnemyDeathSound(int unused)
    {
        audioSource.PlayOneShot(enemyDeathSound[(Random.Range(0,enemyDeathSound.Length))]);
    }

    void PlayLazerImpactSound()
    {
        audioSource.PlayOneShot(lazerImpactSound);
    }

    void PlayOuchSound()
    {
        audioSource.PlayOneShot(ouchSound[Random.Range(0,ouchSound.Length)]);
    }

    void PlayLazerSound()
    {
        audioSource.PlayOneShot(lazerSound[Random.Range(0,lazerSound.Length)]);
    }

    void PlaySwordWhooshSound()
    {
        print("plating swoosh");
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

    void PlayChewingSound()
    {
        audioSource.PlayOneShot(chewSounds[Random.Range(0, chewSounds.Length)]);
    }

    void PlayEnergyCollectedSound()
    {
        audioSource.PlayOneShot(energyCollectedSounds[Random.Range(0, energyCollectedSounds.Length)]);
    }
}
