using UnityEngine;

public class WeaponEffect : MonoBehaviour
{
    [SerializeField] GameObject swordImpactPrefab;
    [SerializeField] Weapon weapon;

    private bool isAlreadyInstantiated;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Enemy>())
        {
            collision.GetComponentInParent<Enemy>().Health -= weapon.Damage;
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
