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

        if (transform.position.x >= gridGenerator.size.x - gridGenerator.xHalf || transform.position.y >= gridGenerator.size.y - gridGenerator.yHalf)
        {
            GameController.instance.songController.beat -= Move;
            Destroy(gameObject);
        }
    }

    public static void SpawnBullet(GameObject bullet, Vector2 position, BulletStats bulletStats)
    {
        Instantiate(bullet, position, Quaternion.identity).GetComponent<BulletStats>().Set(bulletStats);
    }
}

public class BulletStats
{
    public string bulletType;
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
