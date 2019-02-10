using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public TextAsset levelJson;
    public Level level = new Level();

    public PlayerController playerController;
    public SongController songController;
    public GUIController guiController;
    public GridGenerator gridGenerator;

    private void OnEnable()
    {
        instance = this;

        PatternLoader.instance.LoadPattern();

        if (level.name.Equals(string.Empty))
            level = JSON.LoadFromJSON(levelJson);
    }

    private void Start()
    {
        /*guiController = FindObjectOfType<GUIController>();

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

        gridGenerator = FindObjectOfType<GridGenerator>();*/
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}

[System.Serializable]
public class Level
{
    public string name = "New Level";
    public Vector2 size;
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

    public static Level Blank()
    {
        Level blankLevel = new Level
        {
            name = string.Empty,
            size = Vector2.zero,
            song = null,
            bpm = 0,
            amountOfFrames = 0,
            frames = new Frame[0]
        };

        return blankLevel;
    }
}
