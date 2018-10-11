using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public PlayerMovement playerMovement;
    public SongController songController;

    private void OnEnable()
    {
        instance = this;
    }
}
