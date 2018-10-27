using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public string jsonPath;
    public Level level;

    public PlayerController playerController;
    public SongController songController;
    public GUIController guiController;
    public BulletManager bulletManager;
    public GridGenerator gridGenerator;

    private void OnEnable()
    {
        instance = this;
        level = JSONBulletManager.LoadFromJSON(jsonPath);
    }

    private void Start()
    {
        guiController = FindObjectOfType<GUIController>();

        if (guiController == null)
        {
            throw new MissingReferenceException("Please add guiController to scene!!");
        }

        if (playerController == null)
        {
            throw new MissingReferenceException("Please add playerController to game controller!!");
        }
        if (songController == null)
        {
            throw new MissingReferenceException("Please add songController to game controller!!");
        }

        bulletManager = FindObjectOfType<BulletManager>();
        gridGenerator = FindObjectOfType<GridGenerator>();
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

    public Level(string name, Vector2 size, AudioClip song, int bpm, int amountOfFrames)
    {
        this.name = name;
        this.size = size;
        this.song = song;
        this.bpm = bpm;
        this.amountOfFrames = amountOfFrames;

        frames = new Frame[amountOfFrames];
        for (int i = 0; i < frames.Length; i++)
        {
            frames[i] = new Frame();
        }
    }

    public Level(string name, Vector2 size, int amountOfFrames)
    {
        this.name = name;
        this.size = size;
        this.amountOfFrames = amountOfFrames;

        frames = new Frame[amountOfFrames];
        for (int i = 0; i < frames.Length; i++)
        {
            frames[i] = new Frame();
        }
    }
}
