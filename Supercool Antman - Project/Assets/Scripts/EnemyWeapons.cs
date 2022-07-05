using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapons : MonoBehaviour
{
    [SerializeField] int damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*print("trigger entered");*/
        if (other.gameObject.GetComponentInChildren<PlayerStats>())
        {
            other.gameObject.GetComponentInChildren<PlayerStats>().ChangeHealth(-damage);
            /*print(other.gameObject.GetComponentInChildren<PlayerStats>().currentHealth + "  Current player health ");*/
        }
    }
}
