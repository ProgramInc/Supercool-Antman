using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class PlayerAnimatorController : MonoBehaviour
{
    PlayerInput playerInput;
    Animator animator;
    PlayerStats playerStats;
    [SerializeField] GameObject lazerPrefab;
    [SerializeField] CCDSolver2D headSolver;
    [SerializeField] Solver2D leftArmSolver;
    [SerializeField] Transform leftShoulder;
    [SerializeField] Transform leftHandTarget;
    [Range(0.4f, 1)]
    [SerializeField] float swordDistanceFromBody;
    [SerializeField] Transform reticle;
    [SerializeField] Transform topLazerPoint;
    [SerializeField] Transform bottomLazerPoint;
    [SerializeField] float lazerSpeed;
    [SerializeField] GameObject fadingAttackPrefab;
    [SerializeField] Weapon[] weapons;

    public delegate void LazerShootAction();
    public static LazerShootAction OnLazerShot;

    public delegate void SwordSwooshAction();
    public static SwordSwooshAction OnSwordSwoosh;

    public delegate void LightSaberSwooshAction();
    public static LightSaberSwooshAction OnLightSaberSwoosh;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerInput = GetComponentInParent<PlayerInput>();
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (!playerInput.isPaused)
        {
            animator.SetBool("IsWalking", playerInput.isWalking);
            if (playerInput.isGrounded)
            {
                animator.SetFloat("AnimationSpeed", (reticle.position.x > transform.position.x ? 1 : -1) * playerInput.walkModifierHorizontal);
            }
            else if (playerInput.isWalled)
            {
                animator.SetFloat("AnimationSpeed", (reticle.position.y > transform.position.y ? 1 : -1) * playerInput.walkModifierVertical);
            }
            if (playerInput.IsAttacking)
            {
                if (playerStats.currentWeapon == PlayerWeaponTypes.Sword)
                {
                    animator.SetTrigger("IsAttacking");
                    OnSwordSwoosh?.Invoke();
                    /*print("invoked");*/
                    playerInput.IsAttacking = false;
                }
                else if (playerStats.currentWeapon == PlayerWeaponTypes.Lightsaber)
                {
                    animator.SetTrigger("IsAttacking");
                    OnLightSaberSwoosh?.Invoke();
                    playerInput.IsAttacking = false;
                    if (playerStats.currentEnergy <= 0)
                    {
                        /*print(playerStats.currentEnergy);*/
                        playerInput.ForceDrawSword();
                    }
                }
            }

            if (playerInput.isShooting)
            {
                if (playerStats.currentEnergy > 0)
                {
                    animator.SetTrigger("IsShooting");
                }
            } 
        }

    }

    void DecreaseEnergyByLightsaber()
    {
        if (playerStats.currentWeapon == PlayerWeaponTypes.Lightsaber)
        {
            playerStats.ChangeEnergy(-5);
        }
    }

    void DecreaseEnergyByLazer()
    {
        playerStats.ChangeEnergy(-10);
    }

    private void LateUpdate()
    {
        if (!playerInput.isPaused)
        {
            IKChain2D head = headSolver.GetChain(0);
            head.target.position = reticle.position;
            /*head.target.position = playerInput.mousePosition;*/

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

    public void ShootLazer()
    {
        /*float zRotationTopLazer = Mathf.Atan2(playerInput.mousePosition.y - topLazerPoint.position.y, playerInput.mousePosition.x - topLazerPoint.position.x) * Mathf.Rad2Deg;*/
        float zRotationBottomLazer = Mathf.Atan2(playerInput.mousePosition.y - bottomLazerPoint.position.y, playerInput.mousePosition.x - bottomLazerPoint.position.x) * Mathf.Rad2Deg;
        /*Rigidbody2D topLazer = Instantiate(lazerPrefab, topLazerPoint.position, Quaternion.Euler(0, 0, zRotationTopLazer + 90)).GetComponent<Rigidbody2D>();*/
        Rigidbody2D bottomLazer = Instantiate(lazerPrefab, bottomLazerPoint.position, Quaternion.Euler(0, 0, zRotationBottomLazer + 90)).GetComponent<Rigidbody2D>();
        /*topLazer.AddForce((playerInput.mousePosition - (Vector2)transform.position).normalized * lazerSpeed, ForceMode2D.Impulse);*/
        bottomLazer.AddForce((reticle.position - bottomLazerPoint.position).normalized * lazerSpeed, ForceMode2D.Impulse);
        OnLazerShot?.Invoke();
    }

    void SendDoDamageMessageToWeapon()
    {
        foreach (Weapon weapon in weapons)
        {
            if (weapon.gameObject.activeInHierarchy)
            {
                weapon.DoDamage();
            }
        }
    }

}
