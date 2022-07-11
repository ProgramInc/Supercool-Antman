using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapons : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] GameObject ouchCloudPrefab;

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        *//*print("trigger entered");*//*
        if (other.gameObject.GetComponentInChildren<PlayerStats>())
        {
            other.gameObject.GetComponentInChildren<PlayerStats>().ChangeHealth(-damage);
            Instantiate(ouchCloudPrefab, other.gameObject.transform.position, Quaternion.Euler(0, 0, Random.Range(-45, 45)));
            *//*print(other.gameObject.GetComponentInChildren<PlayerStats>().currentHealth + "  Current player health ");*//*
        }
    }*/
}
