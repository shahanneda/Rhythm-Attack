using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public BulletTypeToGameObject[] bulletTypeToGameObjects;

    private Level level;

    private int currentFrame = -1;
    private bool firstIteration = true;

    private void Start()
    {
        GameController.instance.songController.beat += SpawnBullets;

        level = GameController.instance.level;
    }

    public GameObject GetBulletTypeFromGameObject(string type)
    {
        foreach (BulletTypeToGameObject bulletTypeToGameObject in bulletTypeToGameObjects)
        {
            if (bulletTypeToGameObject.bulletType == type)
            {
                return bulletTypeToGameObject.bulletObject;
            }
        }

        return null;
    }

    public void SpawnBullets()
    {
        currentFrame++;

        int greenBulletsInCurrentFrame = 0;

        if (currentFrame >= level.amountOfFrames)
        {
            currentFrame = 0;
            firstIteration = false;
        }

        foreach (BulletStats bulletStats in level.frames[currentFrame].bullets)
        {
            if (bulletStats.type == "Green")
            {
                greenBulletsInCurrentFrame++;
            }
        }

        foreach (BulletStats bulletStats in level.frames[currentFrame].bullets)
        {
            if (bulletStats.type == "None")
            {
                return;
            }

            if (bulletStats.type.Contains("Laser"))
            {
                SpawnLaser(new LaserStats(bulletStats.type, bulletStats.position, bulletStats.direction));
            }
            else if (bulletStats.type.Contains("Battery"))
            {
                if (firstIteration)
                {
                    Instantiate(GetBulletTypeFromGameObject(bulletStats.type), GameController.instance.gridGenerator.GetPositionFromGrid(bulletStats.position), Quaternion.identity);
                }
            }
            else
            {
                Instantiate(GetBulletTypeFromGameObject(bulletStats.type), GameController.instance.gridGenerator.GetPositionFromGrid(bulletStats.position), Quaternion.identity).GetComponent<Bullet>().bulletStats = bulletStats;
            }
        }
    }

    public void SpawnLaser(LaserStats laserStats)
    {
        GameObject prefab = GetBulletTypeFromGameObject(laserStats.type);
        int amountOfNodes = 0;

        if (laserStats.direction.x != 0 && laserStats.direction.y == 0)
        {
            if (laserStats.direction.x < 0)
            {
                int distFromHalf = (int)(GameController.instance.gridGenerator.xHalf - laserStats.position.x);
                amountOfNodes = (int)(GameController.instance.gridGenerator.size.x - (distFromHalf + GameController.instance.gridGenerator.xHalf));
            }
            else
            {
                amountOfNodes = (int)(GameController.instance.gridGenerator.size.x - laserStats.position.x);
            }
        }
        else if (laserStats.direction.x == 0 && laserStats.direction.y != 0)
        {
            if (laserStats.direction.y < 0)
            {
                int distFromHalf = (int)(GameController.instance.gridGenerator.yHalf - laserStats.position.y);
                amountOfNodes = (int)(GameController.instance.gridGenerator.size.y - (distFromHalf + GameController.instance.gridGenerator.yHalf));
            }
            else
            {
                amountOfNodes = (int)(GameController.instance.gridGenerator.size.y - laserStats.position.y);
            }
        }
        else
        {
            amountOfNodes = Mathf.CeilToInt(Mathf.Sqrt(Mathf.Pow(GameController.instance.gridGenerator.size.x - laserStats.position.x, 2) + Mathf.Pow(GameController.instance.gridGenerator.size.y - laserStats.position.y, 2)));
        }

        for (int i = 0; i < amountOfNodes; i++)
        {
            Vector2 position = (laserStats.direction * i) + laserStats.position;

            if (position.x > 1 && position.x < 11 && position.y > 1 && position.y < 11)
            {
                Instantiate(prefab, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + laserStats.position, Quaternion.identity);
            }
        }
    }
}

[System.Serializable]
public class BulletTypeToGameObject
{
    public string bulletType;
    public GameObject bulletObject;
}
