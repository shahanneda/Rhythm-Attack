﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public PlayerMovement playerMovement;

    private void OnEnable()
    {
        instance = this;
    }
}
