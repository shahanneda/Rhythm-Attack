using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PatternLoader : MonoBehaviour
{
    public static PatternLoader instance;

    public TextAsset Pattern { get; set; }

    private void OnEnable()
    {
        instance = this;
        DontDestroyOnLoad(this);

        SceneManager.sceneLoaded += CheckLevel;
    }

    private void CheckLevel(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu")
        {
            Destroy(gameObject);
        }
    }

    public void LoadPattern()
    {
        GameController.instance.levelJson = Pattern;
    }
}
