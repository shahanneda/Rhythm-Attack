using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject patternLoader;

    public Transform levelsPanel;
    public GameObject button;

    public TextAsset[] patterns;

    private void Start()
    {
        foreach (TextAsset pattern in patterns)
        {
            GameObject menuButton = Instantiate(button, levelsPanel);
            menuButton.GetComponent<MenuButton>().pattern = pattern;
            menuButton.GetComponentInChildren<Text>().text = pattern.name;
        }
    }

    public void LoadPattern(TextAsset pattern)
    {
        Instantiate(patternLoader);
        PatternLoader.instance.Pattern = pattern;

        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
