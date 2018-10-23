using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageToBulletPattern : MonoBehaviour
{
    public Texture2D patterns;
    public ColorToBullet[] colors;

    public float bulletSpeed = 0.1f;

    private int currentPatternIndex;
    private Vector2 size;

    private void Start()
    {
        size = GameController.instance.gridGenerator.size;
        InvokeRepeating("LoadPattern", 0, bulletSpeed);

        foreach (ColorToBullet colortobullet in colors)
        {
            GameController.instance.gridGenerator.GenerateBulletGrid(colortobullet.bullet);
        }
    }

    private void LoadPattern()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Color currentColor = patterns.GetPixel(x + currentPatternIndex * (int)size.x, y);

                if (currentColor.a > 0)
                {
                    if (GetBulletFromColor(currentColor) != null)
                    {
                        GameController.instance.gridGenerator.bulletGrid[x, y].gameObject.SetActive(true);
                    }
                }
                else
                {
                    GameController.instance.gridGenerator.bulletGrid[x, y].gameObject.SetActive(false);
                }
            }
        }

        currentPatternIndex++;
        if (currentPatternIndex >= 13)
        {
            currentPatternIndex = 0;
        }
    }

    private GameObject GetBulletFromColor(Color color)
    {
        foreach (ColorToBullet colorToBullet in colors)
        {
            if (color.IsSimlarTo(colorToBullet.color, 25))
            {
                return colorToBullet.bullet;
            }
        }

        return null;
    }
}

[System.Serializable]
public class ColorToBullet
{
    public Color color;
    public GameObject bullet;

    public ColorToBullet()
    {

    }

    public ColorToBullet(Color color, GameObject bullet)
    {
        this.color = color;
        this.bullet = bullet;
    }
}

static class Extension
{
    public static bool IsSimlarTo(this Color me, Color other, int closnessValue)
    {
        if (me.r + closnessValue > other.r && me.r - closnessValue < other.r)
        {
            if (me.r + closnessValue > other.r && me.r - closnessValue < other.r)
            {
                if (me.b + closnessValue > other.b && me.b - closnessValue < other.b)
                {
                    if (me.g + closnessValue > other.g && me.g - closnessValue < other.g)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}