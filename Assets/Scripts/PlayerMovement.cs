using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveBounds;
    private Vector2 movement;

    public float transitionSpeed = 0.01f;

    public bool autoPlayer;

    private void Start()
    {
        GameController.instance.songController.beat += CheckPlayerMovement;
    }

    private void CheckPlayerMovement()
    {
        if (autoPlayer) MovePlayer(new Vector2(Random.Range(0, 2) * 2 - 1, Random.Range(0, 2) * 2 - 1));
        else
        {
            movement = new Vector2(Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0, Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0);
            MovePlayer(movement);
        }
    }

    public void MovePlayer(Vector2 move)
    {
        Vector3 newPosition = transform.position + new Vector3(move.x, move.y, 0);

        if (newPosition.x >= -moveBounds.x && newPosition.x <= moveBounds.x && newPosition.y >= -moveBounds.y && newPosition.y <= moveBounds.y)
        {

            SlowMover(transform.position);
        }
    }

    public void MovePlayerTo(Vector2 newPosition)
    {
        if (newPosition.x >= -moveBounds.x && newPosition.x <= moveBounds.x && newPosition.y >= -moveBounds.y && newPosition.y <= moveBounds.y)
        {
            transform.position = newPosition;
        }
    }
    private void SlowMover(Vector2 newPosition){
        //If there is a better way please change
        if (new Vector2(transform.position.x, transform.position.y) != newPosition)
        {
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * transitionSpeed);
        }else
        {
            SlowMover(newPosition);
        }
           
    }
    public void SetBounds(Vector2 newBounds)
    {
        moveBounds = newBounds;
    }
}
