using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public TextAsset pattern;

    public void TriggerMenu()
    {
        FindObjectOfType<Menu>().LoadPattern(pattern);
    }
}
