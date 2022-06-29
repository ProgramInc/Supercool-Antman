using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int Damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CompareTag("Lazer"))
        {
            Destroy(gameObject);
        }
    }

}
