using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletStats bulletStats;

    private GridGenerator gridGenerator;

    private Vector3 newPos;

    public void Start()
    {
        newPos = transform.position;

        GameController.instance.songController.beat += Move;

        gridGenerator = GameController.instance.gridGenerator;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 25f);
    }

    public void Move()
    {
        newPos += new Vector3(bulletStats.direction.x, bulletStats.direction.y, 0);

        if (newPos.x >= gridGenerator.size.x - gridGenerator.xHalf || newPos.y >= gridGenerator.size.y - gridGenerator.yHalf || newPos.x <= (gridGenerator.size.x - gridGenerator.xHalf) * -1 || newPos.y <= (gridGenerator.size.y - gridGenerator.yHalf) * -1)
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
