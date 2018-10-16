using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public PlayerMovement playerMovement;

    private PlayerHealth playerHealth;

    private int dashes = 3;

    private float lastReffilTime;
    private float timeBetween;

    void Start()
    {
        lastReffilTime = Time.time;
        timeBetween = GameController.instance.songController.secondsBetweenBeats;

        playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement == null)
        {
            throw new MissingReferenceException("Please add PlayerMovement to player!!");
        }

        playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth == null)
        {
            throw new MissingReferenceException("Please add playerHealth to player!!");
        }
    }

    void Update()
    {
        //print(GameController.instance.songController.beatCounter % GameController.instance.songController.song.beatsPerBar );
        if (GameController.instance.songController.beatCounter % GameController.instance.songController.song.beatsPerBar == 0 && lastReffilTime + timeBetween < Time.time)
        {
            AddDash();
            lastReffilTime = Time.time;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            TakeDamage(10);
        }
    }

    //  USE  *ONLY* THESE  METHODS WHEN YOU WANT TO INTRACT WITH HEALTH!
    public void TakeDamage(float count)
    {
        playerHealth.Decrease(count);
        GameController.instance.guiController.DamageOverlay();
    }

    public void AddHealth(float count)
    {
        playerHealth.Increase(count);
    }

    public bool UseDash()
    {
        dashes--;

        if (dashes < 0)
        {
            dashes = 0;
            return false;
        }

        GameController.instance.guiController.SetDashText(dashes.ToString());
        return true;
    }

    public bool AddDash()
    {
        dashes++;

        if (dashes > 3)
        {
            dashes = 3;
            return false;
        }

        GameController.instance.guiController.SetDashText(dashes.ToString());
        return true;
    }
}
