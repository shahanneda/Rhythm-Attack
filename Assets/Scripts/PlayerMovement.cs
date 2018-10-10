using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveBounds;
    public float sensitivity;

    private void Update()
    {
        //PROOF OF CONCEPT TO BE CHANGED || Only checks the input twice instead of 4 times
        var xAxis = Input.GetAxis("Horizontal");
        var yAxis = Input.GetAxis("Vertical");
        
        //feels diffrent than other solution
        MovePlayer(yAxis < -sensitivity ? Vector2.down : Vector2.zero);
        MovePlayer(yAxis > sensitivity  ? Vector2.up  : Vector2.zero);
        MovePlayer(xAxis < -sensitivity ? Vector2.left : Vector2.zero);
        MovePlayer(xAxis > sensitivity  ? Vector2.right : Vector2.zero);

      
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    MovePlayer(Vector2.up);
        //}

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    MovePlayer(Vector2.left);
        //}

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    MovePlayer(Vector2.down);
        //}

        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    MovePlayer(Vector2.right);
        //}
    }

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
