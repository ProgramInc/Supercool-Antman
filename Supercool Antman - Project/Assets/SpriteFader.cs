using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFader : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] float fadeOutTime;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void FadeOut()
    {
        Color spriteColor = GetComponent<SpriteRenderer>().color;
        while (spriteColor.a >= 0)
        {
            spriteColor.a -= fadeOutTime * Time.deltaTime;
            spriteRenderer.color = spriteColor;
        }
    }*/
}
