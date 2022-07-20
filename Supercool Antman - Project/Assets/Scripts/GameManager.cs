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


    private void OnEnable()
    {
        PlayerStats.OnPlayerDeath += Death;
    }


    private void OnDisable()
    {
        PlayerStats.OnPlayerDeath -= Death;
    }

    private void Start()
    {
        Time.timeScale = 1;
    }

    void Death()
    {
        foreach (Animator animator in onDeathAnimators)
        {
            animator.SetTrigger("onDeathTrigger");
        }
    }
}
