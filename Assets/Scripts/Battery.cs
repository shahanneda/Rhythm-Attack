using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour {
    //TODO: Make battries spawn from the level editor
    // Use this for initialization
    public int Health = 3;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnAttack(){
        //TODO: Add Animations for the the death
        //TODO: Add multiple health states with diffrent sprites for each
        Health--;
        if(Health <= 0){
            Destroy(gameObject);
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnAttack();
    }


}
