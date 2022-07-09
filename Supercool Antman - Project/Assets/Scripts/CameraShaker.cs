using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] GameObject feetPosition;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] GameObject wallLeft;
    [SerializeField] GameObject wallRight;
    [SerializeField] GameObject floor;
    [SerializeField] GameObject ceiling;

    private bool isOnWallLeft;
    private bool isOnWallRight;
    private bool isOnFloor;
    public bool isOnCeiling;

    private float timeSinceGrounded;
    private float timeSinceWalled;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    { 
        if (playerInput.isGrounded)
        {
            timeSinceGrounded = 0;
        }
        else
        {
            timeSinceGrounded += Time.deltaTime;
        }
        LandOnCeiling();
        LandOnFloor();
    }


    private void LandOnCeiling()
    {
        var colliderChecker = Physics2D.OverlapCircle(feetPosition.transform.position, 3);

        if (timeSinceGrounded > 0 && colliderChecker.IsTouching(ceiling.GetComponent<BoxCollider2D>()) && isOnCeiling == false)
        {
            print("SUPPOSE TO TRIGGER SHAKE");
            animator.SetTrigger("shakeCeiling");
            isOnCeiling = true;
            isOnFloor = false;
        }

    }

    private void LandOnFloor()
    {
        var colliderChecker = Physics2D.OverlapCircle(feetPosition.transform.position, 3);

        if (timeSinceGrounded > 0 && colliderChecker.IsTouching(floor.GetComponent<BoxCollider2D>()) && isOnFloor == false)
        {
            print("SUPPOSE TO TRIGGER SHAKE");
            animator.SetTrigger("shakeFloor");
            isOnFloor = true;
            isOnCeiling = false;
        }
    }
}
