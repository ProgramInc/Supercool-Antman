using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLimiter : MonoBehaviour
{
    /*Vector3 leftLimit;
    Vector3 rightLimit;
    Vector3 topLimit;
    Vector3 bottomLimit;*/
    Camera cam;
    GameManager gameManager;
    [SerializeField] GameObject reticle;
    bool isMouseLimited;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        /*leftLimit = gameManager.leftLimit.position;*/
        Cursor.visible = false;
        cam = Camera.main;
        isMouseLimited = true;
    }

    private void OnEnable()
    {
        PlayerStats.OnPlayerDeath += UnlimitMouse;
    }

    private void OnDisable()
    {
        PlayerStats.OnPlayerDeath -= UnlimitMouse;
    }


    private void Update()
    {
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);


        if (isMouseLimited)
        {
            if (mousePosition.x < gameManager.leftLimit.transform.position.x)
            {
                mousePosition.x = gameManager.leftLimit.transform.position.x;
            }
            if (mousePosition.x > gameManager.rightLimit.transform.position.x)
            {
                mousePosition.x = gameManager.rightLimit.transform.position.x;
            }
            if (mousePosition.y < gameManager.bottomLimit.transform.position.y)
            {
                mousePosition.y = gameManager.bottomLimit.transform.position.y;
            }
            if (mousePosition.y > gameManager.topLimit.transform.position.y)
            {
                mousePosition.y = gameManager.topLimit.transform.position.y;
            } 
        }

        reticle.transform.position = mousePosition;
    }

    void UnlimitMouse()
    {
        isMouseLimited = false;
    }
}
