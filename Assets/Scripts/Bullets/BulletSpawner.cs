using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public BulletTypeToGameObject[] bulletTypeToGameObjects;
    public LaserTypeToGameObject[] laserTypeToGameObjects;

    public List<Battery> batteries = new List<Battery>();

    [SerializeField] private Level level;

    private int currentFrame = -1;

    private BulletStats greenLaser;

    private Boss boss;
    private GridGenerator gridGenerator;

    private void Start()
    {
        GameController.instance.songController.beat += SpawnBullets;

        level = GameController.instance.level;
        boss = FindObjectOfType<Boss>();
        gridGenerator = FindObjectOfType<GridGenerator>();

        foreach (BulletStats bulletStats in level.frames[0].bullets)
        {
            if (bulletStats.type.Contains("Battery"))
            {
                batteries.Add(Instantiate(GetBulletTypeFromGameObject(bulletStats.type), gridGenerator.GetPositionFromGrid(bulletStats.position), Quaternion.identity).GetComponent<Battery>());
            }
        }

        for (int i = 0; i < level.frames.Length; i++)
        {
            int addAmount = 0;
            List<BulletStats> warnings = new List<BulletStats>();

            foreach (BulletStats bulletStats in level.frames[i].bullets)
            {
                Vector2 position = bulletStats.position;

                if (!bulletStats.type.Contains("Battery") && !bulletStats.type.Contains("Warning"))
                {
                    if (bulletStats.type.Contains("Laser"))
                    {
                        bool batteryLaser = (GetBatteryAtPosition(gridGenerator.GetPositionFromGrid(position + Vector2.up)) != null || GetBatteryAtPosition(gridGenerator.GetPositionFromGrid(position + Vector2.down)) || GetBatteryAtPosition(gridGenerator.GetPositionFromGrid(position + Vector2.right)) || GetBatteryAtPosition(gridGenerator.GetPositionFromGrid(position + Vector2.left)) != null || GetBatteryAtPosition(gridGenerator.GetPositionFromGrid(position + Vector2.one)) != null || GetBatteryAtPosition(gridGenerator.GetPositionFromGrid(position - Vector2.one)) != null || GetBatteryAtPosition(gridGenerator.GetPositionFromGrid(position + new Vector2(1, -1))) != null || GetBatteryAtPosition(gridGenerator.GetPositionFromGrid(position + new Vector2(-1, 1))) != null);

                        if (bulletStats.type == "RedLaser")
                        {
                            addAmount = (batteryLaser) ? 1 : 2;
                            warnings.Add(new BulletStats("RedLaserWarning", position, bulletStats.direction));
                        }
                        else if (bulletStats.type == "YellowLaser")
                        {
                            addAmount = (batteryLaser) ? 3 : 1;
                            warnings.Add(new BulletStats("YellowLaserWarning", position, bulletStats.direction));
                        }
                        else if (bulletStats.type == "OrangeLaser")
                        {
                            addAmount = (batteryLaser) ? 3 : 4;
                            warnings.Add(new BulletStats("OrangeLaserWarning", position, bulletStats.direction));
                        }
                        else if (bulletStats.type == "BlueLaser")
                        {
                            addAmount = (batteryLaser) ? 3 : 4;
                            warnings.Add(new BulletStats("BlueLaserWarning", position, bulletStats.direction));
                        }
                        else if (bulletStats.type == "GreenLaser")
                        {
                            addAmount = 2;
                            warnings.Add(new BulletStats("GreenLaserWarning", position, bulletStats.direction));
                        }
                        else if (bulletStats.type == "PurpleLaser")
                        {
                            addAmount = (batteryLaser) ? 5 : 4;
                            warnings.Add(new BulletStats("PurpleLaserWarning", position, bulletStats.direction));
                        }
                    }
                    else
                    {
                        addAmount = 1;
                        warnings.Add(new BulletStats(bulletStats.type + "Warning", position, bulletStats.direction));
                    }
                }
            }

            if (addAmount > 0)
            {
                foreach (BulletStats warning in warnings)
                {
                    for (int x = 1; x < addAmount + 1; x++)
                    {
                        int index = i - x;

                        if (index < 0)
                        {
                            index += level.amountOfFrames;
                        }

                        level.frames[index].bullets.Add(warning);
                    }
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

    public LaserTypeToGameObject GetLaser(string type)
    {
        foreach (LaserTypeToGameObject laserTypeToGameObject in laserTypeToGameObjects)
        {
            if (laserTypeToGameObject.bulletType == type)
            {
                return laserTypeToGameObject;
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
        }

        foreach (BulletStats bulletStats in level.frames[currentFrame].bullets)
        {
            Vector2 position = bulletStats.position;

            if (bulletStats.type == "None" || bulletStats.type.Contains("Battery"))
            {
                continue;
            }

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
            else
            {
                Bullet spawnedBullet = Instantiate(GetBulletTypeFromGameObject(bulletStats.type), GameController.instance.gridGenerator.GetPositionFromGrid(position), LaserRotation(bulletStats.direction)).GetComponent<Bullet>();

                if (spawnedBullet != null)
                    spawnedBullet.bulletStats = bulletStats;
            }
        }
    }

    public void SpawnLaser(BulletStats laserStats)
    {
        LaserTypeToGameObject laserType;
        if (laserStats.type.Contains("Warning"))
        {
            laserType = GetLaser(laserStats.type.Replace("Warning", ""));
        }
        else
        {
            laserType = GetLaser(laserStats.type);
        }

        int amountOfNodes = 0;

        if (/*laserStats.type == "GreenLaser" || */laserStats.type == "GreenLaserWarning")
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

            greenLaser = new BulletStats("GreenLaser", laserStats.position, laserStats.direction);
        }
        else if (laserStats.type == "GreenLaser")
        {
            if (greenLaser != null)
            {
                laserStats = greenLaser;
                greenLaser = null;
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

                if (GetBatteryAtPosition(gridGenerator.GetPositionFromGrid(position)) != null || position.x < 0 || position.x >= 13 || position.y < 0 || position.y >= 13)
                {
                    return;
                }
                else if (position == new Vector2(4, 6) || position == new Vector2(4, 8) || position == new Vector2(5, 8) || position == new Vector2(6, 8) || position == new Vector2(7, 8) || position == new Vector2(8, 8) || position == new Vector2(8, 7) || position == new Vector2(8, 6) || position == new Vector2(8, 5) || position == new Vector2(8, 4) || position == new Vector2(7, 4) || position == new Vector2(6, 4) || position == new Vector2(5, 4) || position == new Vector2(4, 4))
                {
                    boss.LasersFromBoss.Add(position);
                }

                if (laserStats.type.Contains("Warning"))
                {
                    Instantiate(laserType.warning, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + laserStats.position, LaserRotation(laserStats.direction));
                }
                else
                {
                    Instantiate(laserType.oneBlock, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + laserStats.position, LaserRotation(laserStats.direction));
                }
            }

            return;
        }

        bool laserFromBoss = false;
        Vector2 laserOrigin = laserStats.position;

        if (laserOrigin == new Vector2(4, 6) || laserOrigin == new Vector2(4, 8) || laserOrigin == new Vector2(5, 8) || laserOrigin == new Vector2(6, 8) || laserOrigin == new Vector2(7, 8) || laserOrigin == new Vector2(8, 8) || laserOrigin == new Vector2(8, 7) || laserOrigin == new Vector2(8, 6) || laserOrigin == new Vector2(8, 5) || laserOrigin == new Vector2(8, 4) || laserOrigin == new Vector2(7, 4) || laserOrigin == new Vector2(6, 4) || laserOrigin == new Vector2(5, 4) || laserOrigin == new Vector2(4, 4))
        {
            laserFromBoss = true;
            boss.LasersFromBoss.Add(laserOrigin);
        }

        for (int i = 0; i < amountOfNodes; i++)
        {
            Vector2 currentPosition = (laserStats.direction * i) + laserStats.position;

            if (currentPosition == new Vector2(2, 2) || currentPosition == new Vector2(10, 2) || currentPosition == new Vector2(2, 10) || currentPosition == new Vector2(10, 10))
            {
                return;
            }

            if (laserFromBoss && !laserStats.type.Contains("Warning"))
            {
                if (laserStats.type == "RedLaser" || laserStats.type == "OrangeLaser")
                {
                    Instantiate(laserType.thickMiddle, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + laserStats.position, LaserRotation(laserStats.direction));

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

                    Instantiate(laserType.thickRight, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + position2, LaserRotation(laserStats.direction));
                    Instantiate(laserType.thickLeft, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + position3, LaserRotation(laserStats.direction));
                }
                else if (laserStats.type == "PurpleLaser")
                {
                    Instantiate(laserType.thickMiddle, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + laserStats.position, LaserRotation(laserStats.direction));

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

                    Instantiate(laserType.thickMiddle, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + position2, LaserRotation(laserStats.direction));
                    Instantiate(laserType.thickRight, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + position3, LaserRotation(laserStats.direction));
                    Instantiate(laserType.thickMiddle, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + position4, LaserRotation(laserStats.direction));
                    Instantiate(laserType.thickLeft, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + position5, LaserRotation(laserStats.direction));
                }
                else
                {
                    Instantiate(laserType.oneBlock, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + laserStats.position, LaserRotation(laserStats.direction));
                }
            }
            else
            {
                if (laserStats.type.Contains("Warning"))
                {
                    Instantiate(laserType.warning, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + laserStats.position, LaserRotation(laserStats.direction));
                }
                else
                {
                    Instantiate(laserType.oneBlock, GameController.instance.gridGenerator.GetPositionFromGrid(laserStats.direction * i) + laserStats.position, LaserRotation(laserStats.direction));
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

    public static bool IsBossAtPosition(Vector2 position)
    {
        if (position.x >= -1 && position.x <= 1 && position.y >= -1 && position.y <= 1 && FindObjectOfType<Boss>() != null)
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
        else if (vector2 == -Vector2.one)
        {
            return Quaternion.Euler(Vector3.forward * -225);
        }
        else if (vector2 == Vector2.left)
        {
            return Quaternion.Euler(Vector3.forward * -270);
        }
        else if (vector2 == new Vector2(-1, 1))
        {
            return Quaternion.Euler(Vector3.forward * -315);
        }
        else
        {
            return Quaternion.identity;
        }
    }

    public static Quaternion LaserRotation(Vector2 vector2)
    {
        if (vector2 == Vector2.one)
        {
            return Quaternion.Euler(Vector3.forward * -45);
        }
        else if (vector2 == new Vector2(1, -1))
        {
            return Quaternion.Euler(Vector3.forward * -135);
        }
        else if (vector2 == -Vector2.one)
        {
            return Quaternion.Euler(Vector3.forward * -225);
        }
        else if (vector2 == Vector2.left || vector2 == Vector2.right)
        {
            return Quaternion.Euler(Vector3.forward * -270);
        }
        else if (vector2 == new Vector2(-1, 1))
        {
            return Quaternion.Euler(Vector3.forward * -315);
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

[System.Serializable]
public class LaserTypeToGameObject
{
    public string bulletType;
    public GameObject warning;
    public GameObject oneStart;
    public GameObject oneBlock;
    public GameObject thickLeft;
    public GameObject thickMiddle;
    public GameObject thickRight;
}
