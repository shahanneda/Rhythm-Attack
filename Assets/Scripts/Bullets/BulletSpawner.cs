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
            Instantiate(GetBulletTypeFromGameObject(bulletStats.bulletType), GameController.instance.gridGenerator.GetPositionFromGrid(bulletStats.position), Quaternion.identity).GetComponent<Bullet>().bulletStats = bulletStats;
        }
    }
}

[System.Serializable]
public class BulletTypeToGameObject
{
    public string bulletType;
    public GameObject bulletObject;
}
