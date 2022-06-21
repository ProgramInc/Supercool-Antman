using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    [SerializeField] Vector2 downDirection;
    [SerializeField] Vector3 rotationAfterCollision;
    PlayerInput playerInput;

    private void Awake()
    {
        playerInput = FindObjectOfType<PlayerInput>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("collided");
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponentInParent<Rigidbody2D>().transform.rotation = Quaternion.Euler(rotationAfterCollision);
            Physics2D.gravity = downDirection;
            playerInput.currentRotation = rotationAfterCollision;
            
        }
    }
}
