using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveBounds;

    private float timeFromLastMove = 0f;

    private void Update()
    {
        timeFromLastMove -= Time.deltaTime;

        //PROOF OF CONCEPT TO BE CHANGED || Only checks the input twice instead of 4 times
        var xAxis = Input.GetAxisRaw("Horizontal");
        var yAxis = Input.GetAxisRaw("Vertical");

        //feels diffrent than other solution
        if (timeFromLastMove <= 0f)
        {
            MovePlayer(new Vector2(xAxis, yAxis));
        }


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

        timeFromLastMove = 0.15f;
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
