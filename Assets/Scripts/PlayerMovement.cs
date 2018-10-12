using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public bool autoPlayer;

    private Vector2 moveBounds;
    private Vector2 movement;

    private void Start()
    {
        GameController.instance.songController.beat += CheckPlayerMovement;
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
            // IF PLAYERCONTROLLER.UseDash == true
            // TODO: Make playercontroller TODO: Smooth out dash
            if (Input.GetKey(KeyCode.LeftShift))
            {
                movement = new Vector2(movement.x * 2, movement.y * 2);
            }
            if(movement.x.Equals(0) && movement.y.Equals(0)){
                GameController.instance.playerController.TakeDamage(2f);
                print("PLAYER MISSED BEAT!");
            }
            MovePlayer(movement);
        }
    }
    //TODO: SMOOTh the movement between each beat
    public void MovePlayer(Vector2 move)
    {
        Vector3 newPosition = transform.position + new Vector3(move.x, move.y, 0);

        if (newPosition.x >= -moveBounds.x && newPosition.x <= moveBounds.x && newPosition.y >= -moveBounds.y && newPosition.y <= moveBounds.y)
        {

            transform.position = newPosition;
        }
    }

    public void MovePlayerTo(Vector2 newPosition)
    {
        if (newPosition.x >= -moveBounds.x && newPosition.x <= moveBounds.x && newPosition.y >= -moveBounds.y && newPosition.y <= moveBounds.y)
        {
            transform.position = newPosition;
        }
    }

    public void SetBounds(Vector2 newBounds)
    {
        moveBounds = newBounds;
    }
}
