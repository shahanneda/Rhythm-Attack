﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int Health = 3;

    private bool started;
    public bool Started
    {
        get
        {
            return started;
        }
        set
        {
            started = value;
            animator.SetBool("Started", true);
        }
    }

    public List<Vector2> lasersFromBoss = new List<Vector2>();
    public List<Vector2> LasersFromBoss
    {
        get
        {
            return lasersFromBoss;
        }
    }

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        GameController.instance.songController.beat += UpdateAnimation;
    }

    private void UpdateAnimation()
    {
        animator.SetBool("Up", lasersFromBoss.Contains(new Vector2(6, 8)));
        animator.SetBool("Right", lasersFromBoss.Contains(new Vector2(8, 6)));
        animator.SetBool("Down", lasersFromBoss.Contains(new Vector2(6, 4)));
        animator.SetBool("Left", lasersFromBoss.Contains(new Vector2(4, 6)));

        lasersFromBoss = new List<Vector2>();
    }

    public void OnAttack()
    {
        //TODO: Add Animations for the the death
        //TODO: Add multiple health states with diffrent sprites for each
        Health--;
        if (Health <= 0)
        {
            FindObjectOfType<SongController>().bossAlive = false;
            Destroy(gameObject);
        }
    }
}
