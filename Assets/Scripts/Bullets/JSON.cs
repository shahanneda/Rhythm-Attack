using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSON : MonoBehaviour
{
    public static Level LoadFromJSON(string path)
    {
        return JsonUtility.FromJson<Level>(File.ReadAllText(path));
    }

    public static Level LoadFromJSON(TextAsset textAsset)
    {
        return JsonUtility.FromJson<Level>(textAsset.text);
    }
}
