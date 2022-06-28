using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] int damage;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerStats>())
        {
            other.gameObject.GetComponent<PlayerStats>().ChangeHealth(-damage);
        }
    }
}
