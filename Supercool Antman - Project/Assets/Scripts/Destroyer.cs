using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    /*void Update()
    {
        Destroy(gameObject, 1f);
    }*/

    public void DestroyMe()
    {
        /*print(gameObject.name + " destroyed");*/
        Destroy(gameObject);
    }
}
