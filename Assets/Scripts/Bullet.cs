using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletStats bulletStats;

    private GridGenerator gridGenerator;

    private void Start()
    {
        GameController.instance.songController.beat += Move;

        gridGenerator = GameController.instance.gridGenerator;
    }

    public void Move()
    {
        transform.position += new Vector3(bulletStats.direction.x, bulletStats.direction.y, 0);

        if (transform.position.x >= gridGenerator.size.x - gridGenerator.xHalf || transform.position.y >= gridGenerator.size.y - gridGenerator.yHalf || transform.position.x <= (gridGenerator.size.x - gridGenerator.xHalf) * -1 || transform.position.y <= (gridGenerator.size.y - gridGenerator.yHalf) * -1)
        {
            GameController.instance.songController.beat -= Move;
            Destroy(gameObject);
        }
    }
}

[System.Serializable]
public class BulletStats
{
    public string bulletType;
    public Vector2 position;
    public Vector2 direction;

    public BulletStats(string bulletType, Vector2 direction)
    {
        this.bulletType = bulletType;
        this.direction = direction;
    }

    public void Set(string bulletType, Vector2 direction)
    {
        this.bulletType = bulletType;
        this.direction = direction;
    }

    public void Set(BulletStats bulletStats)
    {
        bulletType = bulletStats.bulletType;
        direction = bulletStats.direction;
    }
}

[System.Serializable]
public class Frame
{
    public List<BulletStats> bullets;
}
