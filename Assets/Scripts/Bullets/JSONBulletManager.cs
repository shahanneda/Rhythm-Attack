using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONBulletManager : MonoBehaviour
{
    public static Level LoadFromJSON(string path)
    {
        return JsonUtility.FromJson<Level>(File.ReadAllText(path));
    }
}
