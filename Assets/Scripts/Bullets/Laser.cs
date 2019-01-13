using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public int beatsToDie = 1;

    private int beatCounter;

    private void Start()
    {
        FindObjectOfType<SongController>().beat += CheckIfDie;
    }

    private void CheckIfDie()
    {
        beatCounter++;

        if (beatCounter >= beatsToDie)
        {
            FindObjectOfType<SongController>().beat -= CheckIfDie;
            Destroy(gameObject);
        }
    }
}
