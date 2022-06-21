using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    PlayerInput playerInput;
    Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }
    void Update()
    {
        animator.SetBool("IsWalking", playerInput.isWalking);
    }
}
