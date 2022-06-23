using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] GameObject dustEffect;
    [SerializeField] Transform feetPosition;
    PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponentInParent<PlayerInput>();
    }

    public void SpawnDust()
    {
        if (playerInput.isWalking)
        {
            Instantiate(dustEffect, feetPosition.position, Quaternion.identity);
        }
    }
}
