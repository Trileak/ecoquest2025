using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Player player;
    private float rotationX;
    private float rotationY;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        rotationX = player.transform.position.x;
        rotationY = player.transform.position.y;
    }

    private void Update()
    {
        Vector2 mouseMovement = player.GetMouseMovement();
        if ((rotationX - mouseMovement.y / 40f >=90) || (rotationY - mouseMovement.y / 40f >=90))
        {
            if (rotationX < 0)
            {
                gameObject.transform.Rotate(-90, 0, 0);
            }
            else
            {
                gameObject.transform.Rotate(90, 0, 0);
            }
        }
        else
        {
            gameObject.transform.Rotate(-mouseMovement.y/40f, 0, 0);
        }
    }
}

