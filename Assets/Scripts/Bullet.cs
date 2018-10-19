using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string bulletType;
    public Vector2 direction;

    private GridGenerator gridGenerator;

    private void Start()
    {
        GameController.instance.songController.beat += Move;

        gridGenerator = GameController.instance.gridGenerator;
    }

    public void Move()
    {
        transform.position += new Vector3(direction.x, direction.y, 0);

        if (transform.position.x >= gridGenerator.size.x - gridGenerator.xHalf || transform.position.y >= gridGenerator.size.y - gridGenerator.yHalf)
        {
            GameController.instance.songController.beat -= Move;
            Destroy(gameObject);
        }
    }
}
