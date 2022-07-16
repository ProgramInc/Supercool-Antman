using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool isWalking;
    public bool isGrounded;
    [SerializeField] bool isWalled;
    [SerializeField] Transform groundRayCaster;
    [SerializeField] float speed = 6f;
    [SerializeField] int flipSign;
    [SerializeField] GameObject sword;
    [SerializeField] GameObject lightSaber;
    [SerializeField] Sprite swordSlash;
    [SerializeField] Sprite lightSaberSlash;
    [SerializeField] SpriteRenderer fadingAttackRenderer;
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material lightSaberMaterial;

    PlayerStats playerStats;
    public Vector2 mousePosition { get; private set; }

    public Vector3 currentRotation;
    Camera cam;

    public bool IsAttacking;
    public bool isShooting;

    public delegate void FLipAction();
    public static FLipAction OnFLip;

    private void Awake()
    {
        cam = Camera.main;
        playerStats = GetComponentInChildren<PlayerStats>();
    }

    void Update()
    {
        if (playerStats.isAlive)
        {
            GroundCheck();
            IsThePlayerWalking();
            Flip();
            OrientPlayer();
            CheckForAttacks();
            CheckForShooting();
            DrawSword();
            DrawLightSaber(); 
        }
    }

    private void CheckForShooting()
    {
        isShooting = Input.GetMouseButtonDown(1);
    }

    private void DrawLightSaber()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && playerStats.currentEnergy > 0)
        {
            sword.SetActive(false);
            lightSaber.SetActive(true);
            playerStats.currentWeapon = PlayerWeaponTypes.Lightsaber;
            fadingAttackRenderer.sprite = lightSaberSlash;
            fadingAttackRenderer.material = lightSaberMaterial;
            
        }
    }

    void DrawSword()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            sword.SetActive(true);
            lightSaber.SetActive(false);
            playerStats.currentWeapon = PlayerWeaponTypes.Sword;
            fadingAttackRenderer.sprite = swordSlash;
            fadingAttackRenderer.material = defaultMaterial;
        }
    }

    public void ForceDrawSword()
    {
        sword.SetActive(true);
        lightSaber.SetActive(false);
        playerStats.currentWeapon = PlayerWeaponTypes.Sword;
        fadingAttackRenderer.sprite = swordSlash;
        fadingAttackRenderer.material = defaultMaterial;
    }

    private void CheckForAttacks()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IsAttacking = true;
        }
    }

    private void IsThePlayerWalking()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 && isGrounded || Input.GetAxisRaw("Vertical") != 0 && isWalled)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        if (Input.GetAxisRaw("Horizontal") > 0 && isWalking)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        if (Input.GetAxisRaw("Horizontal") < 0 && isWalking)
        {
            transform.position -= Vector3.right * speed * Time.deltaTime;
        }

        if (Input.GetAxisRaw("Vertical") > 0 && isWalking)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }

        if (Input.GetAxisRaw("Vertical") < 0 && isWalking)
        {
            transform.position -= Vector3.up * speed * Time.deltaTime;
        }
    }

    private void GroundCheck()
    {
        if (playerStats.isAlive)
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(groundRayCaster.transform.position, -transform.up * 2, 1.5f);

            Debug.DrawRay(groundRayCaster.transform.position, -transform.up * 2, Color.red);
            foreach (RaycastHit2D raycastHit in hit)
            {
                if (raycastHit.collider.CompareTag("FloorOrCeiling"))
                {
                    isGrounded = true;

                }
                else
                {
                    isGrounded = false;
                }
                if (raycastHit.collider.CompareTag("Wall"))
                {
                    isWalled = true;
                }
                else
                {
                    isWalled = false;
                }
            } 
        }
    }

    void Flip()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded || isWalled)
            {
                Physics2D.gravity = -Physics2D.gravity;
                OnFLip?.Invoke();
            }
        }
    }



    void OrientPlayer()
    {
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        if (isGrounded)
        {
            if (transform.position.x > mousePosition.x)
            {
                transform.rotation = currentRotation.z == 0 ? Quaternion.Euler(0, 180, currentRotation.z) : Quaternion.Euler(0, 0, currentRotation.z);
            }
            else
            {
                transform.rotation = currentRotation.z == 0 ? Quaternion.Euler(0, 0, currentRotation.z) : Quaternion.Euler(0, 180, currentRotation.z);
            }
        }
        else if (isWalled)
        {
            if (transform.position.y > mousePosition.y)
            {
                if (transform.position.x > mousePosition.x)
                {
                    transform.rotation = Quaternion.Euler(0, 180, -currentRotation.z);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, currentRotation.z);
                }
            }
            else
            {
                if (transform.position.x > mousePosition.x)
                {
                    transform.rotation = Quaternion.Euler(0, 0, currentRotation.z);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 180, -currentRotation.z);
                }
            }
        }
    }
}

