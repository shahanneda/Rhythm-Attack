using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageToBulletPattern : MonoBehaviour
{
    public Texture2D[] patterns;
    public ColorToBullet[] colors;

    private int currentPatternIndex;

    private Vector2 size;

    private void Start()
    {
        size = new Vector2(patterns[0].width, patterns[0].height);

        GameController.instance.songController.beat += LoadPattern;
    }

    private void LoadPattern()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Color currentColor = patterns[currentPatternIndex].GetPixel(x, y);

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
        if (currentPatternIndex >= patterns.Length)
        {
            currentPatternIndex = 0;
        }
    }

    private GameObject GetBulletFromColor(Color color)
    {
        foreach (ColorToBullet colorToBullet in colors)
        {
            if (color.Equals(colorToBullet.color))
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
