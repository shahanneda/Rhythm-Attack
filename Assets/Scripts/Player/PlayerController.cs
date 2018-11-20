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

    private int beatsSinceLastDash;

    public bool playerActedThisBeat;

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

        GameController.instance.songController.beat += DashRefill;
        GameController.instance.songController.beat += CheckPlayerActedThisBeat;
    }

    void Update()
    {
        //print(GameController.instance.songController.beatCounter % GameController.instance.songController.song.beatsPerBar );
        /*if (GameController.instance.songController.beatCounter % GameController.instance.songController.song.beatsPerBar == 0 && lastReffilTime + timeBetween < Time.time)
        {
            AddDash();
            lastReffilTime = Time.time;

        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            TakeDamage(10);
        }
    }

    private void DashRefill()
    {
        if (beatsSinceLastDash == GameController.instance.songController.song.beatsPerBar)
        {
            beatsSinceLastDash = 0;
            AddDash();
        }
        else
        {
            beatsSinceLastDash++;
        }
    }

    private void CheckPlayerActedThisBeat()
    {
        if (!playerActedThisBeat)
        {
            TakeDamage(5);
        }

        playerActedThisBeat = false;
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
        beatsSinceLastDash = 0;

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

    public void PlayerActedThisBeat()
    {
        playerActedThisBeat = true;
    }
}
