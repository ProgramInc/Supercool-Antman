using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] GameObject lazerImpactPrefab;

    public delegate void LazerImpactedAction();
    public static LazerImpactedAction OnLazerImpact;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Beetle>())
        {
            Instantiate(lazerImpactPrefab, collision.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            OnLazerImpact?.Invoke();
            collision.GetComponent<Beetle>().Health -= damage;
        }
        else if (collision.GetComponent<Mantis>())
        {
            Instantiate(lazerImpactPrefab, collision.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            OnLazerImpact?.Invoke();
            collision.GetComponent<Mantis>().Health -= damage;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
