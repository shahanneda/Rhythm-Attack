using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBullet : Bullet
{
    public GameObject nextBullet;

    private int beatsPassed = 0;

    private void OnEnable()
    {
        GameController.instance.songController.beat += CheckSwitch;
    }

    private void Switch()
    {
        GameController.instance.songController.beat -= CheckSwitch;

        Bullet newBullet = Instantiate(nextBullet, transform.position, Quaternion.Inverse(transform.rotation)).GetComponent<Bullet>();
        newBullet.bulletStats.direction = -bulletStats.direction;
        newBullet.bulletStats.specialtyNumber = bulletStats.specialtyNumber;

        Destroy(gameObject);
    }

    public void CheckSwitch()
    {
        beatsPassed++;

        if (beatsPassed >= bulletStats.specialtyNumber)
        {
            Switch();
        }
    }
}
