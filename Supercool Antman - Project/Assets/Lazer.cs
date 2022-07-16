using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] GameObject lazerImpactPrefab;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Beetle>())
        {
            Instantiate(lazerImpactPrefab, collision.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            collision.GetComponent<Beetle>().Health -= damage;
        }
        else if (collision.GetComponent<Mantis>())
        {
            Instantiate(lazerImpactPrefab, collision.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            collision.GetComponent<Mantis>().Health -= damage;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
