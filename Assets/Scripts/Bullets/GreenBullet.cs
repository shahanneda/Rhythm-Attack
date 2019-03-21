using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBullet : Bullet
{
    private GameController gameController;
    private PlayerMovement playerMovement;

    private int beatsAlive;

    private void Awake()
    {
        gameController = GameController.instance;
        playerMovement = gameController.playerController.playerMovement;

        gameController.songController.beat += FollowPlayer;
    }

    public void FollowPlayer()
    {
        if (this != null)
        {
            Vector2 direction = playerMovement.transform.position - transform.position;
            float absX = Mathf.Abs(direction.x);
            float absY = Mathf.Abs(direction.y);

            if (direction.x != 0 && direction.y != 0)
            {
                if (absX > absY)
                {
                    direction.y = 0;
                }
                else if (absY > absX)
                {
                    direction.x = 0;
                }
                else
                {
                    direction.y = 0;
                }
            }

            direction.Normalize();
            transform.rotation = BulletSpawner.Vector2ToRotation(direction);

            bulletStats.direction = direction;
            beatsAlive++;
        }

        if (beatsAlive > bulletStats.specialtyNumber)
        {
            gameController.songController.beat -= Move;
            gameController.songController.beat -= FollowPlayer;
            Destroy(gameObject);
        }
    }
}
