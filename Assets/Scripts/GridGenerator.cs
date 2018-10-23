using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public Vector2 size;
    public int xHalf;
    public int yHalf;

    public GameObject nodePrefab;

    public GameObject[,] bulletGrid;
    public GameObject[,] bulletBlue;
    public GameObject[,] bulletOther;

    private void Start()
    {
        GenerateGrid();

        bulletGrid = new GameObject[(int)size.x, (int)size.y];
        bulletBlue = new GameObject[(int)size.x, (int)size.y];
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

        xHalf = Mathf.FloorToInt(size.x / 2f);
        yHalf = Mathf.FloorToInt(size.y / 2f);

        GameController.instance.playerController.playerMovement.SetBounds(new Vector2(xHalf, yHalf));

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Transform generatedNode = Instantiate(nodePrefab, new Vector2(x - xHalf, y - yHalf), Quaternion.identity).transform;
                generatedNode.parent = transform.GetChild(0);
            }
        }

        Camera.main.orthographicSize = 0.635f * ((size.x > size.y) ? size.x : size.y);
    }

    public void GenerateBulletGrid(GameObject bulletPrefab)
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
                generatedBullet.transform.parent = transform.GetChild(1).GetChild(0);
                bulletGrid[x, y] = generatedBullet;

                generatedBullet.SetActive(false);
            }
        }
    }

    public Vector2 GetPositionFromGrid(Vector2 positionOnGrid)
    {
        return positionOnGrid - new Vector2(xHalf, yHalf);
    }
}

[System.Serializable]
public class Level
{
    public string name = "New Level";
    public Vector2 size = Vector2.one * 13f;
    public AudioClip song;
    public int bpm;
    public int amountOfFrames;

    public Frame[] frames = new Frame[0];

    public Level()
    {

    }

    public Level(string name, Vector2 size, AudioClip song, int bpm, Frame[] frames)
    {
        this.name = name;
        this.size = size;
        this.song = song;
        this.bpm = bpm;
        this.frames = frames;
    }
}

[System.Serializable]
public class Frame
{
    public List<BulletStats> bullets = new List<BulletStats>();

    public Frame()
    {

    }
}
