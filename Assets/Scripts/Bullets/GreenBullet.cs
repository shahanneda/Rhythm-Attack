using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBullet : Bullet
{
    private GameController gameController;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        gameController = GameController.instance;
        playerMovement = gameController.playerController.playerMovement;

        gameController.songController.beat += FollowPlayer;
    }

    private void FollowPlayer()
    {
        Vector2 direction = playerMovement.transform.position - transform.position;

        if (direction.x > 1)
        {
            direction.x = 1;
        }
        else if (direction.x < -1)
        {
            direction.x = -1;
        }

        if (direction.y > 1)
        {
            direction.y = 1;
        }
        else if (direction.y < -1)
        {
            direction.y = -1;
        }

        bulletStats.direction = direction;
    }
}
