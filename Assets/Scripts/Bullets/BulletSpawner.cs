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

    private Boss boss;

    private void Start()
    {
        GameController.instance.songController.beat += SpawnBullets;

        level = GameController.instance.level;
        boss = FindObjectOfType<Boss>();

        for (int i = 0; i < level.frames.Length; i++)
        {
            int addAmount = 0;
            BulletStats addStats = new BulletStats();

            foreach (BulletStats bulletStats in level.frames[i].bullets)
            {
                if (bulletStats.type.Contains("Laser"))
                {
                    if (bulletStats.type == "RedLaser")
                    {
                        addAmount = 2;
                        addStats = new BulletStats("RedLaserWarning", bulletStats.position, bulletStats.direction);
                    }
                    else if (bulletStats.type == "YellowLaser")
                    {
                        addAmount = 1;
                        addStats = new BulletStats("YellowLaserWarning", bulletStats.position, bulletStats.direction);
                    }
                    else if (bulletStats.type == "OrangeLaser")
                    {
                        addAmount = 4;
                        addStats = new BulletStats("OrangeLaserWarning", bulletStats.position, bulletStats.direction);
                    }
                    else if (bulletStats.type == "BlueLaser")
                    {
                        addAmount = 3;
                        addStats = new BulletStats("BlueLaserWarning", bulletStats.position, bulletStats.direction);
                    }
                    else if (bulletStats.type == "GreenLaser")
                    {
                        addAmount = 2;
                        addStats = new BulletStats("GreenLaserWarning", bulletStats.position, bulletStats.direction);
                    }
                    else if (bulletStats.type == "PurpleLaser")
                    {
                        addAmount = 4;
                        addStats = new BulletStats("PurpleLaserWarning", bulletStats.position, bulletStats.direction);
                    }
                }
            }

            if (addAmount > 0)
            {
                for (int x = 1; x < addAmount + 1; x++)
                {
                    int index = i - x;

                    if (index < 0)
                    {
                        index += level.amountOfFrames;
                    }

                    level.frames[index].bullets.Add(new BulletStats(addStats.type, addStats.position, addStats.direction));
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

        if (laserStats.type == "GreenLaser" || laserStats.type == "GreenLaserWarning")
        {
            PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();

            if (playerMovement.PlayerPositionOnGrid.x == 5)
            {
                if (playerMovement.PlayerPositionOnGrid.y <= 4)
                {
                    laserStats.position = new Vector2(5, 4);
                    laserStats.direction = Vector2.down;
                }
                else if (playerMovement.PlayerPositionOnGrid.y >= 8)
                {
                    laserStats.position = new Vector2(5, 8);
                    laserStats.direction = Vector2.up;
                }
            }
            else if (playerMovement.PlayerPositionOnGrid.x == 6)
            {
                if (playerMovement.PlayerPositionOnGrid.y <= 4)
                {
                    laserStats.position = new Vector2(6, 4);
                    laserStats.direction = Vector2.down;
                }
                else if (playerMovement.PlayerPositionOnGrid.y >= 8)
                {
                    laserStats.position = new Vector2(6, 8);
                    laserStats.direction = Vector2.up;
                }
            }
            else if (playerMovement.PlayerPositionOnGrid.x == 7)
            {
                if (playerMovement.PlayerPositionOnGrid.y <= 4)
                {
                    laserStats.position = new Vector2(7, 4);
                    laserStats.direction = Vector2.down;
                }
                else if (playerMovement.PlayerPositionOnGrid.y >= 8)
                {
                    laserStats.position = new Vector2(7, 8);
                    laserStats.direction = Vector2.up;
                }
            }
            else if (playerMovement.PlayerPositionOnGrid.y == 5)
            {
                if (playerMovement.PlayerPositionOnGrid.x <= 4)
                {
                    laserStats.position = new Vector2(4, 5);
                    laserStats.direction = Vector2.left;
                }
                else if (playerMovement.PlayerPositionOnGrid.x >= 8)
                {
                    laserStats.position = new Vector2(8, 5);
                    laserStats.direction = Vector2.right;
                }
            }
            else if (playerMovement.PlayerPositionOnGrid.y == 6)
            {
                if (playerMovement.PlayerPositionOnGrid.x <= 4)
                {
                    laserStats.position = new Vector2(4, 6);
                    laserStats.direction = Vector2.left;
                }
                else if (playerMovement.PlayerPositionOnGrid.x >= 8)
                {
                    laserStats.position = new Vector2(8, 6);
                    laserStats.direction = Vector2.right;
                }
            }
            else if (playerMovement.PlayerPositionOnGrid.y == 7)
            {
                if (playerMovement.PlayerPositionOnGrid.x <= 4)
                {
                    laserStats.position = new Vector2(4, 7);
                    laserStats.direction = Vector2.left;
                }
                else if (playerMovement.PlayerPositionOnGrid.x >= 8)
                {
                    laserStats.position = new Vector2(8, 7);
                    laserStats.direction = Vector2.right;
                }
            }
            else if (playerMovement.PlayerPositionOnGrid.x == playerMovement.PlayerPositionOnGrid.y)
            {
                if (playerMovement.PlayerPositionOnGrid.x <= 4)
                {
                    laserStats.position = new Vector2(4, 4);
                    laserStats.direction = Vector2.one * -1;
                }
                else if (playerMovement.PlayerPositionOnGrid.x >= 8)
                {
                    laserStats.position = new Vector2(8, 8);
                    laserStats.direction = Vector2.one;
                }
            }
            else if (playerMovement.PlayerPosition.x == -playerMovement.PlayerPosition.y)
            {
                if (playerMovement.PlayerPositionOnGrid.x <= 4)
                {
                    laserStats.position = new Vector2(4, 8);
                    laserStats.direction = new Vector2(-1, 1);
                }
                else if (playerMovement.PlayerPositionOnGrid.x >= 8)
                {
                    laserStats.position = new Vector2(8, 4);
                    laserStats.direction = new Vector2(1, -1);
                }
            }
            else
            {
                return;
            }
        }

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
                else if (position == new Vector2(4, 6) || position == new Vector2(4, 8) || position == new Vector2(5, 8) || position == new Vector2(6, 8) || position == new Vector2(7, 8) || position == new Vector2(8, 8) || position == new Vector2(8, 7) || position == new Vector2(8, 6) || position == new Vector2(8, 5) || position == new Vector2(8, 4) || position == new Vector2(7, 4) || position == new Vector2(6, 4) || position == new Vector2(5, 4) || position == new Vector2(4, 4))
                {
                    boss.LasersFromBoss.Add(position);
                }

                Instantiate(prefab, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + laserStats.position, Vector2ToRotation(laserStats.direction));
            }

            return;
        }

        bool laserFromBoss = false;
        Vector2 laserOrigin = laserStats.position;

        if (laserOrigin == new Vector2(4, 6) || laserOrigin == new Vector2(4, 8) || laserOrigin == new Vector2(5, 8) || laserOrigin == new Vector2(6, 8) || laserOrigin == new Vector2(7, 8) || laserOrigin == new Vector2(8, 8) || laserOrigin == new Vector2(8, 7) || laserOrigin == new Vector2(8, 6) || laserOrigin == new Vector2(8, 5) || laserOrigin == new Vector2(8, 4) || laserOrigin == new Vector2(7, 4) || laserOrigin == new Vector2(6, 4) || laserOrigin == new Vector2(5, 4) || laserOrigin == new Vector2(4, 4))
        {
            laserFromBoss = true;
        }

        for (int i = 0; i < amountOfNodes; i++)
        {
            Vector2 currentPosition = (laserStats.direction * i) + laserStats.position;

            if (currentPosition == new Vector2(2, 2) || currentPosition == new Vector2(10, 2) || currentPosition == new Vector2(2, 10) || currentPosition == new Vector2(10, 10))
            {
                return;
            }
            else if (currentPosition == new Vector2(4, 6) || currentPosition == new Vector2(4, 8) || currentPosition == new Vector2(5, 8) || currentPosition == new Vector2(6, 8) || currentPosition == new Vector2(7, 8) || currentPosition == new Vector2(8, 8) || currentPosition == new Vector2(8, 7) || currentPosition == new Vector2(8, 6) || currentPosition == new Vector2(8, 5) || currentPosition == new Vector2(8, 4) || currentPosition == new Vector2(7, 4) || currentPosition == new Vector2(6, 4) || currentPosition == new Vector2(5, 4) || currentPosition == new Vector2(4, 4))
            {
                boss.LasersFromBoss.Add(currentPosition);
            }

            Instantiate(prefab, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + laserStats.position, Vector2ToRotation(laserStats.direction));
            if (laserFromBoss)
            {
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

                    Instantiate(prefab, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + position2, Vector2ToRotation(laserStats.direction));
                    Instantiate(prefab, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + position3, Vector2ToRotation(laserStats.direction));
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

                    Instantiate(prefab, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + position2, Vector2ToRotation(laserStats.direction));
                    Instantiate(prefab, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + position3, Vector2ToRotation(laserStats.direction));
                    Instantiate(prefab, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + position4, Vector2ToRotation(laserStats.direction));
                    Instantiate(prefab, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + position5, Vector2ToRotation(laserStats.direction));
                }
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

    public static Quaternion Vector2ToRotation(Vector2 vector2)
    {
        if (vector2 == Vector2.one)
        {
            return Quaternion.Euler(Vector3.forward * -45);
        }
        else if (vector2 == Vector2.right)
        {
            return Quaternion.Euler(Vector3.forward * -90);
        }
        else if (vector2 == new Vector2(1, -1))
        {
            return Quaternion.Euler(Vector3.forward * -135);
        }
        else if (vector2 == Vector2.down)
        {
            return Quaternion.Euler(Vector3.forward * -180);
        }
        else if (vector2 == Vector2.one * -1)
        {
            return Quaternion.Euler(Vector3.forward * -225);
        }
        else if (vector2 == Vector2.left)
        {
            return Quaternion.Euler(Vector3.forward * -270);
        }
        else
        {
            return Quaternion.identity;
        }
    }
}

[System.Serializable]
public class BulletTypeToGameObject
{
    public string bulletType;
    public GameObject bulletObject;
}
