using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public PlayerController playerController;
    public SongController songController;
    public GUIController guiController;
    public BulletManager bulletManager;
    public GridGenerator gridGenerator;
    private void OnEnable()
    {
        instance = this;
    }
    private void Start()
    {
        guiController = FindObjectOfType<GUIController>();

        if (guiController == null)
        {
            throw new MissingReferenceException("Please add guiController to scene!!");
        }

        if (playerController == null){
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
