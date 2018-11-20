using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private void Start()
    {
        GameController.instance.songController.beat += Destroy;
    }

    private void Destroy()
    {
        GameController.instance.songController.beat -= Destroy;
        Destroy(gameObject);
    }
}
