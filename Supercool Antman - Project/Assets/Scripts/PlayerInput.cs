using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool isWalking;
    public bool isGrounded;
    [SerializeField] Transform groundRayCaster;
    [SerializeField] Transform knees;
    [SerializeField] Transform centerMass;
    [SerializeField] float speed = 6f;
    private bool isWalled;
    [SerializeField]private int flipSign;

    Quaternion upRight;
    Quaternion downRight;

    private void Start()
    {
        upRight = Quaternion.Euler(0, 0, 0);
        downRight = Quaternion.Euler(180, 0, 0);
        flipSign = 1;
    }

    void Update()
    {
        GroundCheck();
        IsThePlayerWalking();
        Flip();
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
        RaycastHit2D[] hit = Physics2D.RaycastAll(knees.transform.position, -transform.up * 2, 1.5f);
        Debug.DrawRay(knees.transform.position, -transform.up * 2, Color.red);
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

    void Flip()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                if (transform.rotation == upRight)
                {
                    transform.rotation = downRight;
                }
                else
                {
                    transform.rotation = upRight;
                }
                Physics2D.gravity = -Physics2D.gravity; 
            }
            else if (isWalled)
            {
                if (transform.rotation == upRight)
                {
                    transform.rotation = downRight;
                }
                else
                {
                    transform.rotation = upRight;
                }
                Physics2D.gravity = -Physics2D.gravity;
            }
        }
    }

    private IEnumerator SlideToTarget(Vector2 target)
    {
        print("coroutine started");
        while(true)
        {
            print("coroutine happening");
            transform.position += -transform.up * Time.deltaTime;
            yield return null;
        }
    }
}

