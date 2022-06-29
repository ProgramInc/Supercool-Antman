using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] int damage;

   /* private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerStats>())
        {
            other.gameObject.GetComponent<PlayerStats>().ChangeHealth(-damage);
        }
        Destroy(gameObject);
    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("trigger entered");
        if (other.gameObject.GetComponentInChildren<PlayerStats>())
        {
            other.gameObject.GetComponentInChildren<PlayerStats>().ChangeHealth(-damage);
            /*print(other.gameObject.GetComponentInChildren<PlayerStats>().currentHealth + "  Current player health ");*/
        }
    }
}
