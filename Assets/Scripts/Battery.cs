﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    //TODO: Make battries spawn from the level editor
    // Use this for initialization
    public int Health = 3;

    public void OnAttack()
    {
        //TODO: Add Animations for the the death
        //TODO: Add multiple health states with diffrent sprites for each
        Health--;
        if (Health <= 0)
        {
            BulletSpawner bulletSpawner = FindObjectOfType<BulletSpawner>();
            bulletSpawner.batteries.Remove(this);

            if (bulletSpawner.batteries.Count <= 0)
            {
                FindObjectOfType<SongController>().NextPhase();
            }

            Destroy(gameObject);
        }
    }
}
