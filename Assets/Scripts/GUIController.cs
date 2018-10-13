using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GUIController : MonoBehaviour {
    private Text healthText;
    private Text dashText;
	// Use this for initialization
	void Start () {
        healthText = GameObject.FindWithTag("HealthText").GetComponent<Text>();
        if (healthText == null)
        {
            throw new MissingReferenceException("Please add Healthtext with tag to scene!!");
        }
        dashText = GameObject.FindWithTag("DashText").GetComponent<Text>();
        if (dashText == null)
        {
            throw new MissingReferenceException("Please add dashText with tag to scene!!");
        }

    }
    public void SetHealthText(string text){
        healthText.text = text;
    }

    public void SetDashText(string text){
        dashText.text = text;
    }

 
}
