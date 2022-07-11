using UnityEngine;

public class WeaponEffect : MonoBehaviour
{
    [SerializeField] GameObject swordImpactPrefab;
    [SerializeField] Weapon weapon;
    [SerializeField] Animator cameraAnimator;

    private bool isAlreadyInstantiated;
    private bool isAlreadyZooming;
    private float zoomCooldown = 1f;
    private float timeSinceLastZoom;

    private void Start()
    {
        timeSinceLastZoom = 0;
    }

    private void Update()
    {
        if (isAlreadyZooming)
        {
            timeSinceLastZoom = 0;
            isAlreadyZooming = true;
        }
        else
        {
            timeSinceLastZoom += Time.deltaTime;
            isAlreadyZooming = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Enemy>())
        {
            collision.GetComponentInParent<Enemy>().Health -= weapon.Damage;

            if (!isAlreadyZooming && timeSinceLastZoom > zoomCooldown)
            {
                cameraAnimator.SetTrigger("zoomHit");
            }

            if (!isAlreadyInstantiated)
            {
                isAlreadyInstantiated = true;
                Instantiate(swordImpactPrefab, collision.GetComponentInParent<Enemy>().transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            }
            else
            {
                isAlreadyInstantiated = false;
            }
            
            print(collision.GetComponentInParent<Enemy>().Health);
            /*Instantiate(impactPrefab, collision.transform.position, Quaternion.Euler(0, 0, collision.transform.rotation.z));*/
        }
    }
}
