using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] GameObject feetPosition;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] GameObject wallLeft;
    [SerializeField] GameObject wallRight;
    [SerializeField] GameObject floor;
    [SerializeField] GameObject ceiling;
    [SerializeField] float shakeBuffer;

    public bool isOnWallLeft;
    public bool isOnWallRight;
    public bool isOnFloor;
    public bool isOnCeiling;

    private float timeSinceGrounded;
    private float timeSinceWalled;
    private Animator animator;

    Collider2D feetOverlapCircle;

    private void Start()
    {
        animator = GetComponent<Animator>();
        feetOverlapCircle = Physics2D.OverlapCircle(feetPosition.transform.position, 3f);
    }

    private void Update()
    {
        if (playerInput.isGrounded)
        {
            timeSinceGrounded = 0;
        }
        else
        {
            timeSinceGrounded += Time.time;
        }

        LandOnCeiling();
        LandOnFloor();
        LandOnWallLeft();
        LandOnWallRight();
        IsOnWhat();


    }

    private void IsOnWhat()
    {
        if (feetOverlapCircle.IsTouching(floor.GetComponent<BoxCollider2D>()))
        {
            isOnFloor = true;
            isOnCeiling = false;
            isOnWallLeft = false;
            isOnWallRight = false;
        }
        else if (feetOverlapCircle.IsTouching(ceiling.GetComponent<BoxCollider2D>()))
        {
            isOnFloor = false;
            isOnCeiling = true;
            isOnWallLeft = false;
            isOnWallRight = false;
        }
        else if (feetOverlapCircle.IsTouching(wallLeft.GetComponent<BoxCollider2D>()))
        {
            isOnFloor = false;
            isOnCeiling = false;
            isOnWallLeft = true;
            isOnWallRight = false;
        }
        else if (feetOverlapCircle.IsTouching(wallRight.GetComponent<BoxCollider2D>()))
        {
            isOnFloor = false;
            isOnCeiling = false;
            isOnWallLeft = false;
            isOnWallRight = true;
        }
    }

    private void LandOnCeiling()
    {
        bool isTouching = Physics2D.IsTouching(feetOverlapCircle, ceiling.GetComponent<BoxCollider2D>());

        if (timeSinceGrounded > shakeBuffer && isTouching && isOnCeiling == false && isOnWallRight == false && isOnWallLeft == false)
        {
            animator.SetTrigger("shakeCeiling");
        }
    }

    private void LandOnFloor()
    {
        bool isTouching = Physics2D.IsTouching(feetOverlapCircle, floor.GetComponent<BoxCollider2D>());

        if (timeSinceGrounded > shakeBuffer && isTouching && isOnFloor == false && isOnWallRight == false && isOnWallLeft == false)
        {
            animator.SetTrigger("shakeFloor");
        }
    }

    private void LandOnWallLeft()
    {
        bool isTouching = Physics2D.IsTouching(feetOverlapCircle, wallLeft.GetComponent<BoxCollider2D>());

        if (timeSinceGrounded > shakeBuffer && isTouching && isOnWallLeft == false && isOnFloor == false && isOnCeiling == false)
        {
            animator.SetTrigger("shakeWallLeft");
        }
    }

    private void LandOnWallRight()
    {
        bool isTouching = Physics2D.IsTouching(feetOverlapCircle, wallRight.GetComponent<BoxCollider2D>());

        if (timeSinceGrounded > shakeBuffer && isTouching && isOnWallRight == false && isOnFloor == false && isOnCeiling == false)
        {
            animator.SetTrigger("shakeWallRight");
        }
    }
}
