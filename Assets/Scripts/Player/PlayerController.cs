using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public PlayerMovement playerMovement;
    [SerializeField]
    private PlayerHealth playerHealth;

    private int dashes = 3;
    private int beatsSinceLastDash;

    private bool playerActedThisBeat;
    public bool PlayerActedThisBeat { get { return playerActedThisBeat; } set { playerActedThisBeat = value; } }

    private bool locked = true;

    private void OnEnable()
    {
        instance = this;
    }

    void Start()
    {
        GameController.instance.songController.beat += DashRefill;
        GameController.instance.songController.postBeat += CheckPlayerActedThisBeat;
    }

    void Update()
    {
        CheckAttack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet" || collision.tag == "Laser")
        {
            TakeDamage(10);

            if (collision.CompareTag("Bullet"))
                collision.GetComponent<Bullet>().DestroyBullet();
        }

        if (collision.tag == "BlueBullet")
        {
            TakeDamage(5);
            print("Blue Bullet");
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
        if (!GameController.instance.songController.currentlyInBeat)
        {
            TakeDamage(5);
        }

        Vector3 attackPosition = transform.position + new Vector3(playerMovement.lastDirectionMoved.x, playerMovement.lastDirectionMoved.y, transform.position.z);
        Battery battery = FindObjectOfType<BulletSpawner>().GetBatteryAtPosition(attackPosition);

        if (battery != null)
        {
            battery.OnAttack();
        }
        else if (BossAtPosition(attackPosition))
        {
            if (FindObjectOfType<BulletSpawner>().batteries.Count <= 0)
                FindObjectOfType<Boss>().OnAttack();
        }

        PlayerActedThisBeat = true;
    }

    private void CheckAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    //  USE  *ONLY* THESE  METHODS WHEN YOU WANT TO INTRACT WITH HEALTH!
    public void TakeDamage(float damage)
    {
        if (!locked)
        {
            playerHealth.Decrease(damage);
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

        GameController.instance.guiController.SetDashUI(dashes);
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

        GameController.instance.guiController.SetDashUI(dashes);
        return true;
    }

    public void ToggleLock(bool locked)
    {
        this.locked = locked;

        playerMovement.ToggleLock(locked);
        playerHealth.ToggleLock(locked);
    }

    public static bool BossAtPosition(Vector3 position)
    {
        if ((position == Vector3.zero || position == Vector3.up || position == new Vector3(1, 1) || position == Vector3.right || position == new Vector3(1, -1) || position == Vector3.down || position == new Vector3(-1, -1) || position == Vector3.left || position == new Vector3(-1, 1)) && FindObjectOfType<Boss>() != null)
            return true;
        else
            return false;
    }
}
