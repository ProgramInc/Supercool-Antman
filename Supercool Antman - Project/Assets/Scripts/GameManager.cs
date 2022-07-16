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

    public int beetleDeathCounterInteger = 0;


    private void OnEnable()
    {
        PlayerStats.OnPlayerDeath += Death;
        Enemy.OnEnemyDeath += UpdateDeathCounter;
    }


    private void OnDisable()
    {
        PlayerStats.OnPlayerDeath -= Death;
        Enemy.OnEnemyDeath -= UpdateDeathCounter;
    }


    private void UpdateDeathCounter()
    {
        beetleDeathCounterInteger++;
        beetleTextField.text = beetleDeathCounterInteger.ToString();
    }


    void Death()
    {
        foreach (Animator animator in onDeathAnimators)
        {
            animator.SetTrigger("onDeathTrigger");
        }
    }
}
