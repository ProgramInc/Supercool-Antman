using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float maxEnergy = 100f;

    [SerializeField] Image healthImage;
    [SerializeField] Image energyImage;

    [SerializeField] GameObject playerDeathBloodSplatterPrefab;

    public float currentHealth;
    public float currentEnergy;
    public bool isAlive = true;

    public PlayerWeaponTypes currentWeapon;

    private void Start()
    {
        currentHealth = maxHealth;
        currentEnergy = 0;
        UpdateHealth();
        UpdateEnergy();
        currentWeapon = PlayerWeaponTypes.Sword;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            ChangeHealth(-10f);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeEnergy(10f);
        }
    }

    public void ChangeHealth(float value)
    {
        currentHealth += value;
        if (currentHealth < 0)
        {
            PlayerDeath();
        }
        else if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealth();
    }

    private void PlayerDeath()
    {
        print("you are dead");
        isAlive = false;
        Instantiate(playerDeathBloodSplatterPrefab, transform.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));
        Destroy(gameObject);
    }

    public void ChangeEnergy(float value)
    {
        currentEnergy += value;
        if (currentEnergy < 0)
        {
            currentEnergy = 0;
        }
        else if (currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
        UpdateEnergy();
    }

    public void UpdateHealth()
    {
        healthImage.fillAmount = currentHealth / maxHealth;
    }

    public void UpdateEnergy()
    {
        energyImage.fillAmount = currentEnergy / maxEnergy;
    }
}

public enum PlayerWeaponTypes
{
    Sword,
    Lightsaber
}