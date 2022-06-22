using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int Damage;
    public void ActivateCollider()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }
    public void DeactivateCollider()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
