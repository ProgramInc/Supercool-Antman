using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool isWalking;
    public bool isGrounded;

    [SerializeField] Transform groundRayCaster;
    [SerializeField] float speed = 6f;
    [SerializeField] bool isWalled;
    [SerializeField] int flipSign;


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

    void Flip()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                if (transform.rotation == Quaternion.Euler(0, 0, 0))
                {
                    transform.rotation = Quaternion.Euler(0, 0, -180);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
            else if (isWalled)
            {
                if (transform.rotation == Quaternion.Euler(0, 0, 90))
                {
                    transform.rotation = Quaternion.Euler(0, 0, -90);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                }
            }
            Physics2D.gravity = -Physics2D.gravity;
        }
    }

    private IEnumerator SlideToTarget(Vector2 target)
    {
        print("coroutine started");
        while (true)
        {
            print("coroutine happening");
            transform.position += -transform.up * Time.deltaTime;
            yield return null;
        }
    }
}

