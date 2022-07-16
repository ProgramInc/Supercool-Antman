using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public delegate void EnemyDeathAction(int enemyNumber);
    public static EnemyDeathAction OnEnemyDeath;

    public TextMeshProUGUI[] enemyDeathCountersText;

    private int beetleDeathCounterInteger;
    private int mantisDeathCounterInteger;

    public int beetleHealth;
    public int mantisHealth;

    private void OnEnable()
    {
        OnEnemyDeath += UpdateBeetleDeathCounter;
        OnEnemyDeath += UpdateMantisDeathCounter;
/*        OnEnemyDeath += IncrementEnemyHealth;*/
    }

    private void OnDisable()
    {
        OnEnemyDeath -= UpdateBeetleDeathCounter;
        OnEnemyDeath -= UpdateMantisDeathCounter;
/*        OnEnemyDeath -= IncrementEnemyHealth;*/
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

    private void UpdateBeetleDeathCounter(int enemyNumber)
    {
        beetleDeathCounterInteger++;
        enemyDeathCountersText[enemyNumber].text = beetleDeathCounterInteger.ToString();
    }

    private void UpdateMantisDeathCounter(int enemyNumber)
    {
        mantisDeathCounterInteger++;
        enemyDeathCountersText[enemyNumber].text = mantisDeathCounterInteger.ToString();
    }
}
