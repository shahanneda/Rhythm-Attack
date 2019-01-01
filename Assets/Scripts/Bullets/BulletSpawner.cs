using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public BulletTypeToGameObject[] bulletTypeToGameObjects;
    public List<Battery> batteries = new List<Battery>();

    private Level level;

    private int currentFrame = -1;
    private bool firstIteration = true;

    private List<GameObject> spawnedLasers = new List<GameObject>();

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

        if (currentFrame >= level.amountOfFrames)
        {
            currentFrame = 0;
            firstIteration = false;
        }

        foreach (GameObject laser in spawnedLasers)
        {
            Destroy(laser);
        }
        spawnedLasers = new List<GameObject>();

        foreach (BulletStats bulletStats in level.frames[currentFrame].bullets)
        {
            if (bulletStats.type == "None")
            {
                return;
            }

            if (bulletStats.type.Contains("Laser"))
            {
                SpawnLaser(new BulletStats(bulletStats.type, bulletStats.position, bulletStats.direction));
            }
            else if (bulletStats.type.Contains("Battery"))
            {
                if (firstIteration)
                {
                    batteries.Add(Instantiate(GetBulletTypeFromGameObject(bulletStats.type), GameController.instance.gridGenerator.GetPositionFromGrid(bulletStats.position), Quaternion.identity).GetComponent<Battery>());
                }
            }
            else
            {
                Instantiate(GetBulletTypeFromGameObject(bulletStats.type), GameController.instance.gridGenerator.GetPositionFromGrid(bulletStats.position), Quaternion.identity).GetComponent<Bullet>().bulletStats = bulletStats;
            }
        }
    }

    public void SpawnLaser(BulletStats laserStats)
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
            for (int i = 0; i < 13; i++)
            {
                Vector2 position = (laserStats.direction * i) + laserStats.position;

                if (position == new Vector2(2, 2) || position == new Vector2(10, 2) || position == new Vector2(2, 10) || position == new Vector2(10, 10) || position.x < 0 || position.x >= 13 || position.y < 0 || position.y >= 13)
                {
                    return;
                }

                spawnedLasers.Add(Instantiate(prefab, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + laserStats.position, Quaternion.identity));
            }
        }

        for (int i = 0; i < amountOfNodes; i++)
        {
            Vector2 position = (laserStats.direction * i) + laserStats.position;

            if (position == new Vector2(2, 2) || position == new Vector2(10, 2) || position == new Vector2(2, 10) || position == new Vector2(10, 10))
            {
                return;
            }

            spawnedLasers.Add(Instantiate(prefab, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + laserStats.position, Quaternion.identity));
        }
    }

    public Battery GetBatteryAtPosition(Vector2 position)
    {
        foreach (Battery battery in batteries)
        {
            if (battery.transform.position == new Vector3(position.x, position.y, battery.transform.position.z))
            {
                return battery;
            }
        }

        return null;
    }

    public static bool GetBossAtPosition(Vector2 position)
    {
        if (position.x >= -1 && position.x <= 1 && position.y >= -1 && position.y <= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

[System.Serializable]
public class BulletTypeToGameObject
{
    public string bulletType;
    public GameObject bulletObject;
}
