﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool autoPlayer;
    public float transitionSpeed = 0.1f;

    public Vector2 lastDirectionMoved;

    private Vector2 moveBounds;
    private Vector2 movement;
    private Vector2 prevDirection;

    [HideInInspector] public Vector2 toLocation = Vector2.down * 7;
    public Vector2 PlayerPosition { get { return toLocation; } }

    private GridGenerator gridGenerator;
    public Vector2 PlayerPositionOnGrid
    {
        get
        {
            return gridGenerator.GetPositionOnGrid(toLocation);
        }
    }

    private PlayerController playerController;
    private SongController songController;

    public bool locked = false;

    private BulletSpawner bulletSpawner;
    private bool started;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        songController = FindObjectOfType<SongController>();
        bulletSpawner = FindObjectOfType<BulletSpawner>();
        gridGenerator = FindObjectOfType<GridGenerator>();
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, toLocation, transitionSpeed);
        CheckPlayerMovement();
    }

    private void CheckPlayerMovement()
    {
        if (!locked || !started)
        {
            if (autoPlayer)
            {
                MovePlayer(new Vector2(Random.Range(0, 2) * 2 - 1, Random.Range(0, 2) * 2 - 1));
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    movement = Vector2.right;
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    movement = Vector2.left;
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    movement = Vector2.up;
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    movement = Vector2.down;
                }

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

                if (!movement.Equals(Vector2.zero))
                {
                    if (!started && movement.Equals(Vector2.up))
                    {
                        FindObjectOfType<SongController>().StartSong();
                        started = true;
                        FindObjectOfType<Boss>().Started = true;
                        locked = true;
                    }

                    lastDirectionMoved = movement.normalized;

                    MovePlayer(movement);
                    playerController.PlayerActedThisBeat = true;
                }
            }

            movement = Vector2.zero;
        }
    }

    public void MovePlayer(Vector2 move)
    {
        Vector3 newPosition = new Vector3(Mathf.Round(transform.position.x) + move.x, Mathf.Round(transform.position.y) + move.y, 0);

        Vector3 moveNormalized = move.normalized;
        Vector3 newPositionNormalized = new Vector3(Mathf.Round(transform.position.x) + move.normalized.x, Mathf.Round(transform.position.y) + move.normalized.y, 0);

        bool moved = false;

        if (bulletSpawner.GetBatteryAtPosition(newPosition) == null && bulletSpawner.GetBatteryAtPosition(newPositionNormalized) == null && !BulletSpawner.IsBossAtPosition(newPosition))
        {
            if (newPosition.x >= -moveBounds.x && newPosition.x <= moveBounds.x && newPosition.y >= -moveBounds.y && newPosition.y <= moveBounds.y)
            {
                toLocation = newPosition;
                moved = true;

                if (!songController.currentlyInBeat)
                {
                    playerController.TakeDamage(5);
                }
            }
        }

        if (!moved && prevDirection == lastDirectionMoved)
        {
            playerController.TakeDamage(5);
        }

        prevDirection = lastDirectionMoved;
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

    public void ToggleLock(bool locked)
    {
        this.locked = locked;
    }
}
