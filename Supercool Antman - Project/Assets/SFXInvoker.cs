using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXInvoker : MonoBehaviour
{
    [SerializeField] PlayerWeaponTypes weaponType;


    void Start()
    {
        AudioManager.OnSwordImpact?.Invoke(weaponType);
    }

}
