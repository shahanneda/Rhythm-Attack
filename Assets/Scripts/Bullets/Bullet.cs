using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletStats bulletStats;

    private GridGenerator gridGenerator;

    private Vector3 newPos;

    private bool firstBeat = true;

    public void Start()
    {
        newPos = transform.position;
        transform.rotation = BulletStats.DirectionToRotation(bulletStats.direction);

        GameController.instance.songController.beat += Move;
        gridGenerator = GameController.instance.gridGenerator;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 25f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (firstBeat && bullet.bulletStats.type.Equals(bulletStats.type))
            {
                bullet.DestroyBullet();
            }
        }
    }

    public void Move()
    {
        newPos += new Vector3(bulletStats.direction.x, bulletStats.direction.y, 0);
        firstBeat = false;

        if (newPos.x >= gridGenerator.size.x - gridGenerator.xHalf || newPos.y >= gridGenerator.size.y - gridGenerator.yHalf || newPos.x <= (gridGenerator.size.x - gridGenerator.xHalf) * -1 || newPos.y <= (gridGenerator.size.y - gridGenerator.yHalf) * -1)
        {
            try
            {
                GameController.instance.songController.beat -= Move;
                Destroy(gameObject);
            }
            catch
            {
                return;
            }
        }
    }

    public void DestroyBullet()
    {
        GreenBullet greenBullet = GetComponent<GreenBullet>();
        BlueBullet blueBullet = GetComponent<BlueBullet>();
        SwitchBullet switchBullet = GetComponent<SwitchBullet>();

        GameController.instance.songController.beat -= Move;

        if (greenBullet != null)
        {
            GameController.instance.songController.beat -= greenBullet.FollowPlayer;
        }
        if (blueBullet != null)
        {
            GameController.instance.songController.beat -= blueBullet.CheckSplit;
        }
        if (switchBullet != null)
        {
            GameController.instance.songController.beat -= switchBullet.CheckSwitch;
        }

        if (tag != "Laser")
        {
            Destroy(gameObject);
        }
    }
}

[System.Serializable]
public class BulletStats
{
    public string type;
    public Vector2 position;
    public Vector2 direction;
    public float specialtyNumber = 4;

    public BulletStats()
    {

    }

    public BulletStats(string type, Vector2 position, Vector2 direction)
    {
        this.type = type;
        this.position = position;
        this.direction = direction;
    }

    public BulletStats(string type, Vector2 direction)
    {
        this.type = type;
        this.direction = direction;
    }

    public BulletStats(string type, Vector2 direction, float specialtyNumber)
    {
        this.type = type;
        this.direction = direction;
        this.specialtyNumber = specialtyNumber;
    }

    public BulletStats(BulletStats bulletStats)
    {
        type = bulletStats.type;
        direction = bulletStats.direction;

        try
        {
            specialtyNumber = bulletStats.specialtyNumber;
        }
        catch
        {
            return;
        }
    }

    public override string ToString()
    {
        return type + ", " + position + ", " + direction;
    }

    public static Quaternion DirectionToRotation(Vector2 direction)
    {
        int directionIndex = 0;

        if (direction == Vector2.up)
        {
            directionIndex = 1;
        }
        else if (direction == Vector2.one)
        {
            directionIndex = 2;
        }
        else if (direction == Vector2.right)
        {
            directionIndex = 3;
        }
        else if (direction == new Vector2(1, -1))
        {
            directionIndex = 4;
        }
        else if (direction == Vector2.down)
        {
            directionIndex = 5;
        }
        else if (direction == -Vector2.one)
        {
            directionIndex = 6;
        }
        else if (direction == Vector2.left)
        {
            directionIndex = 7;
        }
        else if (direction == new Vector2(-1, 1))
        {
            directionIndex = 8;
        }
        else
        {
            directionIndex = -1;
        }

        return Quaternion.AngleAxis((directionIndex - 1) * -45, Vector3.forward);
    }
}

[System.Serializable]
public class Frame
{
    public List<BulletStats> bullets;
}
