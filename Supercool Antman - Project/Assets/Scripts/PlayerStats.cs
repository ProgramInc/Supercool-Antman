using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float maxEnergy = 100f;

    public float currentHealth;
    public float currentEnergy;

    private void Start()
    {
        //currentHealth = maxHealth;
        currentHealth = 50;
        currentEnergy = 0;
    }

    public void ChangeHealth(float value)
    {
        currentHealth += value;
    }

    public void ChangeEnergy(float value)
    {
        currentEnergy += value;
    }
}
