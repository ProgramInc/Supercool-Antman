using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int Damage;

    [SerializeField] GameObject impactPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(gameObject.name + " hit" + collision.name);
        if (CompareTag("Lazer"))
        {
            Instantiate(impactPrefab, collision.transform.position, Quaternion.Euler(0, 0, collision.transform.rotation.z));
            if (collision.GetComponentInParent<Enemy>())
            {
                collision.GetComponentInParent<Enemy>().Health -= Damage;
                print(collision.GetComponentInParent<Enemy>().Health);
            }
            Destroy(gameObject);
        }
        else
        {
            if (collision.GetComponentInParent<Enemy>())
            {
                collision.GetComponentInParent<Enemy>().Health -= Damage;
                print(collision.GetComponentInParent<Enemy>().Health);
                /*Instantiate(impactPrefab, collision.transform.position, Quaternion.Euler(0, 0, collision.transform.rotation.z));*/
            }
        }
    }

}
