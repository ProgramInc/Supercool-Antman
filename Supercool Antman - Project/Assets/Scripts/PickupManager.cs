using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    [SerializeField] GameObject[] pickupPrefabs;
    public delegate void DropPickupAction(Vector2 position);
    public static DropPickupAction OnDropPickup;

    private void OnEnable()
    {
        OnDropPickup += DropPickup;
    }

    private void OnDisable()
    {
        OnDropPickup -= DropPickup;
    }

    void DropPickup(Vector2 positionToInstantiate)
    {
        Instantiate(pickupPrefabs[Random.Range(0,pickupPrefabs.Length)], positionToInstantiate, Quaternion.identity);
        print(positionToInstantiate + " pickup instantiated");
    }
}
