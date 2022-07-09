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
    private bool isOnFloor = true;
    private bool isOnCeiling;

    private float timeSinceGrounded;
    private float timeSinceWalled;
    private Animator animator;

    Collider2D overlapCircle;

    private void Start()
    {
        animator = GetComponent<Animator>();
        overlapCircle = Physics2D.OverlapCircle(feetPosition.transform.position, 3f);
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
        bool isTouching = Physics2D.IsTouching(overlapCircle, ceiling.gameObject.GetComponent<BoxCollider2D>());

        if (timeSinceGrounded > 0 && isTouching && isOnCeiling == false)
        {
            print("SUPPOSE TO TRIGGER SHAKE");

            isOnCeiling = true;
            animator.SetTrigger("shakeCeiling");
            isOnFloor = false;
        }
    }

    private void LandOnFloor()
    {
        bool isTouching = Physics2D.IsTouching(overlapCircle, floor.gameObject.GetComponent<BoxCollider2D>());

        if (timeSinceGrounded > 0 && isTouching && isOnFloor == false)
        {
            print("SUPPOSE TO TRIGGER SHAKE");

            isOnFloor = true;
            animator.SetTrigger("shakeFloor");
            isOnCeiling = false;
        }
    }
}
