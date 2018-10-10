using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            MovePlayer(Vector2.up);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            MovePlayer(Vector2.left);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            MovePlayer(Vector2.down);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            MovePlayer(Vector2.right);
        }
    }

    public void MovePlayer(Vector2 move)
    {
        Vector3 newPosition = transform.position + new Vector3(move.x, move.y, 0);

        if (newPosition.x >= -5 && newPosition.x <= 5 && newPosition.y >= -5 && newPosition.y <= 5)
        {
            transform.position = newPosition;
        }
    }
}
