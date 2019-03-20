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
        //GameController.instance.songController.beat += UpdateAnimation;

        songController = FindObjectOfType<SongController>();
    }

    private void UpdateAnimation()
    {
        animator.SetBool("Up", up);
        animator.SetBool("Right", right);
        animator.SetBool("Down", down);
        animator.SetBool("Left", left);

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
