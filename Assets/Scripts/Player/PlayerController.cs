using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [HideInInspector]
    public PlayerMovement playerMovement;

    private PlayerHealth playerHealth;

    private int dashes = 3;
    // Use this for initialization
    void Start () {
        playerMovement = GetComponent<PlayerMovement>();
        if(playerMovement == null){
            throw new MissingReferenceException("Please add PlayerMovement to player!!");
        }

        playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth == null)
        {
            throw new MissingReferenceException("Please add playerHealth to player!!");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

     //  USE  *ONLY* THESE  METHODS WHEN YOU WANT TO INTRACT WITH HEALTH!
    public void TakeDamage(float count){
        playerHealth.Decrease(count);
    }
    public void AddHealth(float count)
    {
        playerHealth.Increase(count);
    }

    public bool UseDash()
    {
        dashes--;
        if(dashes < 0){
            dashes = 0;
            return false;
        }
        GameController.instance.guiController.SetDashText(dashes.ToString());
        return true;
    }
}
