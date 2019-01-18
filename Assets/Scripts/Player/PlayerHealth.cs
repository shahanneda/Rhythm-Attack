using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    //use this healthOfPlayer sparingly as it dosen't update the gui;
    public float health = 100;

    private bool locked = true;

    // Use this public health when doing intractions from the outside or public
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            UpdateGUI();
        }
    }

    public void Decrease(float count)
    {
        if (!locked)
        {
            Health -= count;

            if (Health <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void Increase(float count)
    {
        if (!locked)
        {
            //TODO: Check if more than 100 and do appropiate actions;
            Health += count;
        }
    }

    private void UpdateGUI()
    {
        GameController.instance.guiController.SetHealthUI(Health);
    }

    public void ToggleLock(bool locked)
    {
        this.locked = locked;
    }
}
