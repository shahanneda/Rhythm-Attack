using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool autoPlayer;
    public float transitionSpeed = 0.1f;

    private Vector2 moveBounds;
    private Vector2 movement;

    private Vector2 toLocation;

    private PlayerController playerController;

    private void Start()
    {
        GameController.instance.songController.preBeat += CheckPlayerMovement;

        playerController = FindObjectOfType<PlayerController>();
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, toLocation, transitionSpeed);
    }

    private void CheckPlayerMovement()
    {
        if (!playerController.playerActedThisBeat)
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

                if (movement.x.Equals(0) && movement.y.Equals(0))
                {
                    //GameController.instance.playerController.TakeDamage(5f);
                }

                if (!(movement.x + movement.y).Equals(0))
                {
                    MovePlayer(movement);
                    playerController.PlayerActedThisBeat();
                }
            }
        }

        movement = Vector2.zero;
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
