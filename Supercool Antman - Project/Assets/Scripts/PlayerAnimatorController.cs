using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class PlayerAnimatorController : MonoBehaviour
{
    PlayerInput playerInput;
    Animator animator;
    
    [SerializeField] CCDSolver2D headSolver;
    [SerializeField] Solver2D leftArmSolver;
    [SerializeField] Transform leftShoulder;
    [SerializeField] Transform leftHandTarget;
    [Range(0.4f, 1)]
    [SerializeField] float swordDistanceFromBody;
    [SerializeField] Transform reticle;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerInput = GetComponentInParent<PlayerInput>();
    }
    void Update()
    {
        animator.SetBool("IsWalking", playerInput.isWalking);
        if (playerInput.IsAttacking)
        {
            animator.SetTrigger("IsAttacking");
            playerInput.IsAttacking = false;

        }
    }


    private void LateUpdate()
    {
        IKChain2D head = headSolver.GetChain(0);
        head.target.position = playerInput.mousePosition;

        Vector3 ikTargetOffset = (leftShoulder.position - (Vector3)playerInput.mousePosition).normalized * swordDistanceFromBody;
        IKChain2D leftHand = leftArmSolver.GetChain(0);
        
        if (!animator.GetCurrentAnimatorStateInfo(1).IsTag("Attack"))
        {
            leftHandTarget.position = leftShoulder.position + ikTargetOffset;
            leftHand.target = leftHandTarget;
        }
        else
        {
            leftHand.target = reticle;
        }
    }
}
