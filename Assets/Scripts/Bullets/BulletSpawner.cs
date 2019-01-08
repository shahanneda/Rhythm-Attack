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

    private Boss boss;

    private void Start()
    {
        GameController.instance.songController.beat += SpawnBullets;

        level = GameController.instance.level;
        boss = FindObjectOfType<Boss>();

        for (int i = 0; i < level.frames.Length; i++)
        {
            foreach (BulletStats bulletStats in level.frames[i].bullets)
            {
                if (bulletStats.type.Contains("Laser"))
                {
                    try
                    {
                        if (bulletStats.type == "RedLaser" && i >= 2)
                        {
                            level.frames[i - 1].bullets.Add(new BulletStats("RedLaserWarning", bulletStats.position, bulletStats.direction));
                            level.frames[i - 2].bullets.Add(new BulletStats("RedLaserWarning", bulletStats.position, bulletStats.direction));
                        }
                        else if (bulletStats.type == "YellowLaser" && i >= 1)
                        {
                            level.frames[i - 1].bullets.Add(new BulletStats("YellowLaserWarning", bulletStats.position, bulletStats.direction));
                        }
                        else if (bulletStats.type == "OrangeLaser" && i >= 4)
                        {
                            level.frames[i - 1].bullets.Add(new BulletStats("OrangeLaserWarning", bulletStats.position, bulletStats.direction));
                            level.frames[i - 2].bullets.Add(new BulletStats("OrangeLaserWarning", bulletStats.position, bulletStats.direction));
                            level.frames[i - 3].bullets.Add(new BulletStats("OrangeLaserWarning", bulletStats.position, bulletStats.direction));
                            level.frames[i - 4].bullets.Add(new BulletStats("OrangeLaserWarning", bulletStats.position, bulletStats.direction));
                        }
                        else if (bulletStats.type == "BlueLaser" && i >= 4)
                        {
                            level.frames[i - 1].bullets.Add(new BulletStats("BlueLaserWarning", bulletStats.position, bulletStats.direction));
                            level.frames[i - 2].bullets.Add(new BulletStats("BlueLaserWarning", bulletStats.position, bulletStats.direction));
                            level.frames[i - 3].bullets.Add(new BulletStats("BlueLaserWarning", bulletStats.position, bulletStats.direction));
                            level.frames[i - 4].bullets.Add(new BulletStats("BlueLaserWarning", bulletStats.position, bulletStats.direction));
                        }
                        else if (bulletStats.type == "GreenLaser" && i >= 1)
                        {
                            level.frames[i - 2].bullets.Add(new BulletStats("GreenLaserWarning", bulletStats.position, bulletStats.direction));
                        }
                        else if (bulletStats.type == "PurpleLaser" && i >= 4)
                        {
                            level.frames[i - 1].bullets.Add(new BulletStats("PurpleLaserWarning", bulletStats.position, bulletStats.direction));
                            level.frames[i - 2].bullets.Add(new BulletStats("PurpleLaserWarning", bulletStats.position, bulletStats.direction));
                            level.frames[i - 3].bullets.Add(new BulletStats("PurpleLaserWarning", bulletStats.position, bulletStats.direction));
                            level.frames[i - 4].bullets.Add(new BulletStats("PurpleLaserWarning", bulletStats.position, bulletStats.direction));
                        }
                    }
                    finally
                    {

                    }
                }
                else
                {
                    continue;
                }
            }
        }
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
                continue;
            }

            Vector2 position = bulletStats.position;
            if (position == new Vector2(1, 2) || position == new Vector2(1, 3) || position == new Vector2(2, 3) || position == new Vector2(3, 3) || position == new Vector2(3, 2) || position == new Vector2(3, 1) || position == new Vector2(2, 1) || position == new Vector2(1, 1))
            {
                if (GetBatteryAtPosition(Vector2.one * -4) == null)
                {
                    continue;
                }
            }
            else if (position == new Vector2(9, 2) || position == new Vector2(9, 3) || position == new Vector2(10, 3) || position == new Vector2(11, 3) || position == new Vector2(11, 2) || position == new Vector2(11, 1) || position == new Vector2(10, 1) || position == new Vector2(9, 1))
            {
                if (GetBatteryAtPosition(new Vector2(4, -4)) == null)
                {
                    continue;
                }
            }
            else if (position == new Vector2(9, 10) || position == new Vector2(9, 11) || position == new Vector2(10, 11) || position == new Vector2(11, 11) || position == new Vector2(9, 11) || position == new Vector2(11, 10) || position == new Vector2(9, 9) || position == new Vector2(10, 9))
            {
                if (GetBatteryAtPosition(Vector2.one * 4) == null)
                {
                    continue;
                }
            }
            else if (position == new Vector2(1, 10) || position == new Vector2(1, 11) || position == new Vector2(2, 11) || position == new Vector2(3, 11) || position == new Vector2(3, 10) || position == new Vector2(3, 9) || position == new Vector2(2, 9) || position == new Vector2(1, 9))
            {
                if (GetBatteryAtPosition(new Vector2(-4, 4)) == null)
                {
                    continue;
                }
            }

            if (bulletStats.type.Contains("Laser"))
            {
                SpawnLaser(new BulletStats(bulletStats.type, position, bulletStats.direction));
            }
            else if (bulletStats.type.Contains("Battery"))
            {
                if (firstIteration)
                {
                    batteries.Add(Instantiate(GetBulletTypeFromGameObject(bulletStats.type), GameController.instance.gridGenerator.GetPositionFromGrid(position), Quaternion.identity).GetComponent<Battery>());
                }
            }
            else
            {
                Instantiate(GetBulletTypeFromGameObject(bulletStats.type), GameController.instance.gridGenerator.GetPositionFromGrid(position), Quaternion.identity).GetComponent<Bullet>().bulletStats = bulletStats;
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
                else if (position == new Vector2(4, 6) || position == new Vector2(6, 8) || position == new Vector2(8, 6) || position == new Vector2(6, 4))
                {
                    boss.LasersFromBoss.Add(position);
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
            else if (position == new Vector2(4, 6) || position == new Vector2(6, 8) || position == new Vector2(8, 6) || position == new Vector2(6, 4))
            {
                boss.LasersFromBoss.Add(position);
            }

            spawnedLasers.Add(Instantiate(prefab, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + laserStats.position, Quaternion.identity));
            if (laserStats.type == "RedLaser" || laserStats.type == "OrangeLaser")
            {
                Vector2 position2 = Vector2.zero;
                Vector2 position3 = Vector2.zero;

                if (laserStats.direction == Vector2.up || laserStats.direction == Vector2.down)
                {
                    position2 = new Vector2(laserStats.position.x + 1, laserStats.position.y);
                    position3 = new Vector2(laserStats.position.x - 1, laserStats.position.y);
                }
                else if (laserStats.direction == Vector2.right || laserStats.direction == Vector2.left)
                {
                    position2 = new Vector2(laserStats.position.x, laserStats.position.y + 1);
                    position3 = new Vector2(laserStats.position.x, laserStats.position.y - 1);
                }

                spawnedLasers.Add(Instantiate(prefab, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + position2, Quaternion.identity));
                spawnedLasers.Add(Instantiate(prefab, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + position3, Quaternion.identity));
            }
            else if (laserStats.type == "PurpleLaser")
            {
                Vector2 position2 = Vector2.zero;
                Vector2 position3 = Vector2.zero;
                Vector2 position4 = Vector2.zero;
                Vector2 position5 = Vector2.zero;

                if (laserStats.direction == Vector2.up || laserStats.direction == Vector2.down)
                {
                    position2 = new Vector2(laserStats.position.x + 1, laserStats.position.y);
                    position3 = new Vector2(laserStats.position.x + 2, laserStats.position.y);
                    position4 = new Vector2(laserStats.position.x - 1, laserStats.position.y);
                    position5 = new Vector2(laserStats.position.x - 2, laserStats.position.y);
                }
                else if (laserStats.direction == Vector2.right || laserStats.direction == Vector2.left)
                {
                    position2 = new Vector2(laserStats.position.x, laserStats.position.y + 1);
                    position3 = new Vector2(laserStats.position.x, laserStats.position.y + 2);
                    position4 = new Vector2(laserStats.position.x, laserStats.position.y - 1);
                    position5 = new Vector2(laserStats.position.x, laserStats.position.y - 2);
                }

                spawnedLasers.Add(Instantiate(prefab, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + position2, Quaternion.identity));
                spawnedLasers.Add(Instantiate(prefab, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + position3, Quaternion.identity));
                spawnedLasers.Add(Instantiate(prefab, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + position4, Quaternion.identity));
                spawnedLasers.Add(Instantiate(prefab, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + position5, Quaternion.identity));
            }
        }
    }

    public Battery GetBatteryAtPosition(Vector2 position)
    {
        foreach (Battery battery in batteries)
        {
            if (battery != null && battery.transform.position == new Vector3(position.x, position.y, battery.transform.position.z))
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
