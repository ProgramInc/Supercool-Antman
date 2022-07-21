using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public delegate void BeetleDeathAction();
    public static BeetleDeathAction OnBeetleDeath;

    public delegate void MantisDeathAction();
    public static MantisDeathAction OnMantisDeath;

    public TextMeshProUGUI beetleDeathCountersText;
    public TextMeshProUGUI mantisDeathCountersText;

    [SerializeField] private int beetleDeathCounterInteger;
    [SerializeField] private int mantisDeathCounterInteger;

    public int beetleHealth;
    public int mantisHealth;

    private void OnEnable()
    {
        OnBeetleDeath += UpdateBeetleDeathCounter;
        OnMantisDeath += UpdateMantisDeathCounter;
        OnBeetleDeath += IncrementEnemyHealth;
        OnMantisDeath += IncrementEnemyHealth;
    }

    private void OnDisable()
    {
        OnBeetleDeath -= UpdateBeetleDeathCounter;
        OnMantisDeath -= UpdateMantisDeathCounter;
        OnBeetleDeath -= IncrementEnemyHealth;
        OnMantisDeath -= IncrementEnemyHealth;
    }

    private void Start()
    {
        beetleHealth = 100;
        mantisHealth = 100;
    }

    private void IncrementEnemyHealth()
    {
        beetleHealth = 100 + beetleDeathCounterInteger;
        mantisHealth = 100 + mantisDeathCounterInteger;
    }

    private void UpdateBeetleDeathCounter()
    {
        beetleDeathCounterInteger++;
        beetleDeathCountersText.text = beetleDeathCounterInteger.ToString();
    }

    private void UpdateMantisDeathCounter()
    {
        mantisDeathCounterInteger++;
        beetleDeathCountersText.text = mantisDeathCounterInteger.ToString();
    }
}
