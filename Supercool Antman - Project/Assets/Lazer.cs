using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField] int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>())
        {
            print(collision.name);
            collision.GetComponent<Enemy>().Health -= damage;
        }
        else if (collision.GetComponent<Mantis>())
        {
            collision.GetComponent<Mantis>().Health -= damage;
        }
    }
}
