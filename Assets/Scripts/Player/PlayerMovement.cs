﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool autoPlayer;

    private Vector2 moveBounds;
    private Vector2 movement;
    public float transitionSpeed = 0.1f;
    private Vector2 toLocation;

    private void Start()
    {
        GameController.instance.songController.beat += CheckPlayerMovement;
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, toLocation, transitionSpeed);
    }

    private void CheckPlayerMovement()
    {
        if (autoPlayer)
        {
            MovePlayer(new Vector2(Random.Range(0, 2) * 2 - 1, Random.Range(0, 2) * 2 - 1));
        }
        else
        {
            movement = new Vector2(Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0, Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (!(movement.x + movement.y).Equals(0))
                {
                    if (GameController.instance.playerController.UseDash() == true)
                    {
                        movement = new Vector2(movement.x * 2, movement.y * 2);
                    }

                }
            }

            if (movement.x.Equals(0) && movement.y.Equals(0))
            {
                GameController.instance.playerController.TakeDamage(2f);
            }

            if (!(movement.x + movement.y).Equals(0))
            {
                MovePlayer(movement);
            }
        }
    }

    public void MovePlayer(Vector2 move)
    {
        Vector3 newPosition = new Vector3(Mathf.Round(transform.position.x) + move.x, Mathf.Round(transform.position.y) + move.y, 0);
        if (newPosition.x >= -moveBounds.x && newPosition.x <= moveBounds.x && newPosition.y >= -moveBounds.y && newPosition.y <= moveBounds.y)
        {
            toLocation = newPosition;
        }
    }

    public void MovePlayerTo(Vector2 newPosition)
    {
        if (newPosition.x >= -moveBounds.x && newPosition.x <= moveBounds.x && newPosition.y >= -moveBounds.y && newPosition.y <= moveBounds.y)
        {
            toLocation = newPosition;
        }
    }

    public void SetBounds(Vector2 newBounds)
    {
        moveBounds = newBounds;
    }
}
