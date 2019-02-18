using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject patternLoader;

    public Transform levelsPanel;
    public GameObject levelButton;
    public Scrollbar levelsScroll;

    public InputField customLevelInput;
    public GameObject customLevelFailText;

    public TextAsset[] patterns;

    private void Start()
    {
        foreach (TextAsset pattern in patterns)
        {
            GameObject menuButton = Instantiate(levelButton, levelsPanel);
            menuButton.GetComponent<MenuButton>().pattern = pattern;

            string patternName = "";
            foreach (char c in pattern.name)
            {
                if (c == '_')
                {
                    patternName += " ";
                }
                else
                {
                    patternName += c;
                }
            }

            menuButton.GetComponentInChildren<Text>().text = patternName;
        }

        int rows = patterns.Length / 6;
        levelsPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(660, rows * 65);
        levelsScroll.value = 1;
    }

    public void LoadPattern(TextAsset pattern)
    {
        Instantiate(patternLoader);
        PatternLoader.instance.Pattern = pattern;

        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void LoadCustomPattern()
    {
        Instantiate(patternLoader);
        PatternLoader.instance.Level = JSON.LoadFromJSON(customLevelInput.text);

        SceneManager.LoadScene("Main", LoadSceneMode.Single);

        customLevelFailText.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
