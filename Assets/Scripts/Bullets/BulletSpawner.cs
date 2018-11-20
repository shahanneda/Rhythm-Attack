using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public BulletTypeToGameObject[] bulletTypeToGameObjects;

    private Level level;

    private int currentFrame = 0;

    private void Start()
    {
        GameController.instance.songController.beat += SpawnBullets;

        level = GameController.instance.level;

        SpawnLaser(new LaserStats("RedLaser", Vector2.zero, Vector2.up));
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
        if (currentFrame == level.amountOfFrames - 1)
        {
            currentFrame = 0;
        }
        else
        {
            currentFrame++;
        }

        foreach (BulletStats bulletStats in level.frames[currentFrame].bullets)
        {
            if (bulletStats.type.Contains("Laser"))
            {
                SpawnLaser(new LaserStats(bulletStats.type, bulletStats.position, bulletStats.direction));
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
            amountOfNodes = (int)(GameController.instance.gridGenerator.size.x - laserStats.position.x);
        }
        else if (laserStats.direction.x == 0 && laserStats.direction.y != 0)
        {
            amountOfNodes = (int)(GameController.instance.gridGenerator.size.y - laserStats.position.y);
        }
        else
        {
            amountOfNodes = Mathf.CeilToInt(Mathf.Sqrt(Mathf.Pow(GameController.instance.gridGenerator.size.x - laserStats.position.x, 2) + Mathf.Pow(GameController.instance.gridGenerator.size.y - laserStats.position.y, 2)));
        }

        for (int i = 0; i < amountOfNodes; i++)
        {
            Instantiate(prefab, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + laserStats.position, Quaternion.identity);
        }
    }
}

[System.Serializable]
public class BulletTypeToGameObject
{
    public string bulletType;
    public GameObject bulletObject;
}
