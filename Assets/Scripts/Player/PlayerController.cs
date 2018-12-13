using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [HideInInspector]
    public PlayerMovement playerMovement;
    private PlayerHealth playerHealth;

    private int dashes = 3;
    private int beatsSinceLastDash;

    private bool playerActedThisBeat;

    private bool locked = true;

    private void OnEnable()
    {
        instance = this;
    }

    void Start()
    {
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
        CheckAttack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            TakeDamage(10);

            GreenBullet greenBullet = collision.GetComponent<GreenBullet>();
            if (greenBullet != null)
            {
                GameController.instance.songController.beat -= greenBullet.FollowPlayer;
            }

            Destroy(collision.gameObject);
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

    private void Attack()
    {
        //Attack

        if (!GameController.instance.songController.currentlyInBeat)
        {
            TakeDamage(5);
        }

        PlayerActedThisBeat();
    }

    private void CheckAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    //  USE  *ONLY* THESE  METHODS WHEN YOU WANT TO INTRACT WITH HEALTH!
    public void TakeDamage(float count)
    {
        if (!locked)
        {
            playerHealth.Decrease(count);
            GameController.instance.guiController.DamageOverlay();
        }
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

    public void ToggleLock(bool locked)
    {
        this.locked = locked;

        playerMovement.ToggleLock(locked);
        playerHealth.ToggleLock(locked);
    }
}
