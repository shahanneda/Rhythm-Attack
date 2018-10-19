using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool autoPlayer;
    public bool freeMove = true;

    private Vector2 moveBounds;
    private Vector2 movement;
    public float transitionSpeed = 0.1f;
    private Vector2 toLocation;

    private bool timing;

    private void Start()
    {
        StartCoroutine("Wait");
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(20);

        GameController.instance.songController.beat += CheckPlayerMovement;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, toLocation, transitionSpeed);

        if (freeMove)
        {
            CheckPlayerMovement();
        }
    }

    private void CheckPlayerMovement()
    {
        if (autoPlayer)
        {
            MovePlayer(new Vector2(Random.Range(0, 2) * 2 - 1, Random.Range(0, 2) * 2 - 1));
        }
        else
        {
            if (freeMove)
            {
                movement = new Vector2(Input.GetKeyDown(KeyCode.D) ? 1 : Input.GetKeyDown(KeyCode.A) ? -1 : 0, Input.GetKeyDown(KeyCode.W) ? 1 : Input.GetKeyDown(KeyCode.S) ? -1 : 0);
            }
            else
            {
                movement = new Vector2(Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0, Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0);
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

            if (movement.x.Equals(0) && movement.y.Equals(0) && !freeMove)
            {
                GameController.instance.playerController.TakeDamage(5f);
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
