﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    //use this healthOfPlayer sparingly as it dosen't update the gui;
    private float HealthOfPlayer = 100;

    // Use this public health when doing intractions from the outside or public
    public float Health
    {
        get
        {
            return HealthOfPlayer;
        }

        set
        {
            HealthOfPlayer = value;
            UpdateGui();
        }
    }
    public void Decrease(float count){
        //TODO: Check if less than zero and do appropiate actions;
        Health -= count;
    }
    private void UpdateGui(){
        GameController.instance.guiController.SetHealthText(Health.ToString());
    }

}
