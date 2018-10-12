using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public PlayerController playerController;
    public SongController songController;

    private void OnEnable()
    {
        instance = this;
    }
    private void Start()
    {
        if(playerController == null){
            throw new MissingReferenceException("Please add playerController to game controller!!");
        }
        if (songController == null)
        {
            throw new MissingReferenceException("Please add songController to game controller!!");
        }
    }
}
