using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public Transform leftLimit;
    public Transform rightLimit;
    public Transform topLimit;
    public Transform bottomLimit;

    [SerializeField] Animator[] onDeathAnimators;
    public TextMeshProUGUI beetleTextField;
    public TextMeshProUGUI mantisTextField;

    private int beetleDeathCounterInteger;
    private int mantisDeathCounterInteger;

    private void OnEnable()
    {
        PlayerStats.OnPlayerDeath += Death;
        Enemy.OnEnemyDeath += UpdateBeetleDeathCounter;
    }


    private void OnDisable()
    {
        PlayerStats.OnPlayerDeath -= Death;
        Enemy.OnEnemyDeath -= UpdateBeetleDeathCounter;
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


    void Death()
    {
        foreach (Animator animator in onDeathAnimators)
        {
            animator.SetTrigger("onDeathTrigger");
        }
    }
}
