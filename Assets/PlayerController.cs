using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [HideInInspector]
    public PlayerMovement playerMovement;
	// Use this for initialization
	void Start () {
        playerMovement = GetComponent<PlayerMovement>();
        if(playerMovement == null){
            throw new MissingReferenceException("Please add PlayerMovement to player!!");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
