﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBullet : Bullet
{
    public GameObject smallBullet;

    private int beatsPassed = 0;

    private  void OnEnable()
    {
        GameController.instance.songController.beat += CheckSplit;
    }

    private void CheckSplit()
    {
        beatsPassed++;

        if (beatsPassed >= bulletStats.specialtyNumber)
        {
            GameController.instance.songController.beat -= CheckSplit;
            Split();
        }
    }

    private void Split()
    {
        Instantiate(smallBullet, transform.position, transform.rotation).GetComponent<Bullet>().bulletStats.direction = bulletStats.direction;
        Instantiate(smallBullet, transform.position, Quaternion.Inverse(transform.rotation)).GetComponent<Bullet>().bulletStats.direction = -bulletStats.direction;
        Destroy(gameObject);
    }
}
