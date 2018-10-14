using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public Vector2 size;

    public GameObject nodePrefab;

    public GameObject[,] bulletGrid;
    public GameObject bulletPrefab;
    private void Start()
    {
        GenerateGrid();
        bulletGrid = new GameObject[16 , 16];
        GenerateBulletGrid();
     
    }

    public void GenerateGrid()
    {
        if (size.x % 2 == 0)
        {
            size.x++;
        }

        if (size.y % 2 == 0)
        {
            size.y++;
        }

        int xHalf = Mathf.FloorToInt(size.x / 2f);
        int yHalf = Mathf.FloorToInt(size.y / 2f);

        GameController.instance.playerController.playerMovement.SetBounds(new Vector2(xHalf, yHalf));

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Transform generatedNode = Instantiate(nodePrefab, new Vector2(x - xHalf, y - yHalf), Quaternion.identity).transform;
                generatedNode.parent = transform;

                if (x == xHalf && y == yHalf)
                {
                    generatedNode.GetComponent<SpriteRenderer>().color = Color.magenta;
                }
            }
        }

        Camera.main.orthographicSize = 0.635f * ((size.x > size.y) ? size.x : size.y);
    }

    public void GenerateBulletGrid()
    {
        if (size.x % 2 == 0)
        {
            size.x++;
        }

        if (size.y % 2 == 0)
        {
            size.y++;
        }

        int xHalf = Mathf.FloorToInt(size.x / 2f);
        int yHalf = Mathf.FloorToInt(size.y / 2f);


        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                GameObject generatedBullet = Instantiate(bulletPrefab, new Vector2(x - xHalf, y - yHalf), Quaternion.identity) as GameObject;
                generatedBullet.transform.parent = transform;
                bulletGrid[x , y] = generatedBullet;
                //if (x == xHalf && y == yHalf)
                //{
                //    generatedNode.GetComponent<SpriteRenderer>().color = Color.magenta;
                //}
            }
        }


    }
}
