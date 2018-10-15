using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    private Text healthText;
    private Animator damageOverlay;
    private Text dashText;
    private Text songTitleText;

    void Start()
    {
        healthText = GameObject.FindWithTag("HealthText").GetComponent<Text>();
        if (healthText == null)
        {
            throw new MissingReferenceException("Please add HealthText with tag to scene!");
        }

        damageOverlay = GameObject.FindWithTag("DamageOverlay").GetComponent<Animator>();
        if (damageOverlay == null)
        {
            throw new MissingReferenceException("Please add DamageOverlay with tag to scene!");
        }

        dashText = GameObject.FindWithTag("DashText").GetComponent<Text>();
        if (dashText == null)
        {
            throw new MissingReferenceException("Please add DashText with tag to scene!");
        }

        songTitleText = GameObject.FindWithTag("SongTitle").GetComponent<Text>();
        if (songTitleText == null)
        {
            throw new MissingReferenceException("Please add SongTitleText with tag to scene!");
        }
        else
        {
            songTitleText.text = GameController.instance.songController.song.name;
        }
    }

    public void SetHealthText(string text)
    {
        healthText.text = text;
    }

    public void SetDashText(string text)
    {
        dashText.text = text;
    }

    public void DamageOverlay()
    {
        damageOverlay.Play("Damage");
    }
}
