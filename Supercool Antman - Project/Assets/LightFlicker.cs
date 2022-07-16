using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    Light2D light2D;
    [SerializeField] float minFLicker;
    [SerializeField] float maxFLicker;
    // Start is called before the first frame update
    void Start()
    {
        light2D = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        light2D.intensity = Random.Range(minFLicker, maxFLicker);
    }
}
