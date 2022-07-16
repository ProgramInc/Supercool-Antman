using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public TextMeshProUGUI beetleTextField;
    public TextMeshProUGUI mantisTextField;

    private int beetleDeathCounterInteger;
    private int mantisDeathCounterInteger;

    public int beetleHealth;
    public int mantisHealth;

    private void OnEnable()
    {
        Enemy.OnEnemyDeath += UpdateBeetleDeathCounter;
        Enemy.OnEnemyDeath += IncrementEnemyHealth;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDeath -= UpdateBeetleDeathCounter;
        Enemy.OnEnemyDeath -= IncrementEnemyHealth;
    }

    private void Start()
    {
        IncrementEnemyHealth();
    }

    private void IncrementEnemyHealth()
    {
        beetleHealth = 100 + beetleDeathCounterInteger;
        mantisHealth = 100 + mantisDeathCounterInteger;
    }

    private void UpdateBeetleDeathCounter()
    {
        beetleDeathCounterInteger++;
        beetleTextField.text = beetleDeathCounterInteger.ToString();
    }

    private void UpdateMantisDeathCounter()
    {
        mantisDeathCounterInteger++;
        mantisTextField.text = mantisDeathCounterInteger.ToString();
    }
}
