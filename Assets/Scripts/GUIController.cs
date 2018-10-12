using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GUIController : MonoBehaviour {
    private Text healthText;
	// Use this for initialization
	void Start () {
        healthText = GameObject.FindWithTag("HealthText").GetComponent<Text>();
        if (healthText == null)
        {
            throw new MissingReferenceException("Please add Healthtext with tag to scene!!");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetHealthText(string text){
        healthText.text = text;
    }
}
