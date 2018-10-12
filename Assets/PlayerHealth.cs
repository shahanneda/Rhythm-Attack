using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
   
    public float Health
    {
        get { return Health; }
        set
        {
            UpdateGui();
        }
    }
    public void Decrease(float count){
        //TODO: Check if less than zero and do appropiate actions;
        Health -= count;
    }
    private void UpdateGui(){
        GameController.instance.guiController.SetHealthText(Health.ToString());
    }

}
