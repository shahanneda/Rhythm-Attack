using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    private Image health;
    private Image dash;

    private Animator damageOverlay;

    private Text songTitleText;

    void Start()
    {
        health = GameObject.FindWithTag("HealthUI").GetComponent<Image>();
        if (health == null)
        {
            throw new MissingReferenceException("Please add HealthText with tag to scene!");
        }

        damageOverlay = GameObject.FindWithTag("DamageOverlay").GetComponent<Animator>();
        if (damageOverlay == null)
        {
            throw new MissingReferenceException("Please add DamageOverlay with tag to scene!");
        }

        dash = GameObject.FindWithTag("DashUI").GetComponent<Image>();
        if (dash == null)
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
            songTitleText.text = "♪ " + GameController.instance.songController.song.name;
        }
    }

    public void SetHealthUI(float healthAmount)
    {
        health.fillAmount = healthAmount / 100f;
    }

    public void SetDashUI(float dashAmount)
    {
        dash.fillAmount = dashAmount / 3f;
    }

    public void DamageOverlay()
    {
        damageOverlay.Play("Damage");
    }
}
