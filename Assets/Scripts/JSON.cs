using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSON : MonoBehaviour
{
    public Level LoadLevel(string path)
    {
        Level level = JsonUtility.FromJson<Level>(path);

        return level;
    }
}
