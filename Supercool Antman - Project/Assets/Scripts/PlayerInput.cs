using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool isWalking;
    private bool isGrounded;
    [SerializeField] Transform groundRayCaster;
    private bool isWalled;

    void Update()
    {
        IsThePlayerWalking();
    }

    private void IsThePlayerWalking()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundRayCaster.position, Vector2.down);

        if (hit.collider.CompareTag("FloorOrCeiling"))
        {
            isGrounded = true;
        }
        else if (hit.collider.CompareTag("Wall"))
        {
            isWalled = true;
        }
        if (Input.GetAxis("Horizontal") > 0 && isGrounded || Input.GetAxis("Vertical") > 0 && isWalled)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }
}

