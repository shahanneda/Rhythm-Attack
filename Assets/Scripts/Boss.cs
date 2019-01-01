using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
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
            GetComponent<Animator>().SetBool("started", true);
        }
    }
}
