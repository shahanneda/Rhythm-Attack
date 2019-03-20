using System.Collections;
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

    public bool up;
    public bool right;
    public bool down;
    public bool left;

    private Animator animator;

    private SongController songController;

    private void Start()
    {
        animator = GetComponent<Animator>();
        songController = FindObjectOfType<SongController>();

        songController.beat += UpdateAnimation;
    }

    private void UpdateAnimation()
    {
        if (up && right && down && left)
        {
            animator.Play("All");
        }

        else if (right && down && left)
        {
            animator.Play("No-Up");
        }
        else if (up && down && left)
        {
            animator.Play("No-Right");
        }
        else if (up && right && left)
        {
            animator.Play("No-Down");
        }
        else if (up && right && down)
        {
            animator.Play("No-Left");
        }

        else if (up && right)
        {
            animator.Play("Up-Right");
        }
        else if (up && down)
        {
            animator.Play("Up-Down");
        }
        else if (up && left)
        {
            animator.Play("Up-Left");
        }
        else if (right && down)
        {
            animator.Play("Down-Right");
        }
        else if (down && left)
        {
            animator.Play("Down-Left");
        }
        else if (right && left)
        {
            animator.Play("Right-Left");
        }

        else if (up)
        {
            animator.Play("Up");
        }
        else if (right)
        {
            animator.Play("Right");
        }
        else if (down)
        {
            animator.Play("Down");
        }
        else if (left)
        {
            animator.Play("Left");
        }

        else
        {
            animator.Play("Passive");
        }

        up = false;
        right = false;
        down = false;
        left = false;
    }

    public void OnAttack()
    {
        //TODO: Add Animations for the the death
        //TODO: Add multiple health states with diffrent sprites for each

        if (songController.CurrentPhase == "Hyper")
        {
            Health--;
            if (Health <= 0)
            {
                FindObjectOfType<SongController>().bossAlive = false;
                GameController.instance.songController.beat -= UpdateAnimation;
                Destroy(gameObject);
            }
        }
    }
}
