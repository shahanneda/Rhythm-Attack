using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour {
    /*This is what im thinking of it will read a text like this
     *  e e e e e e e e e e e b e e e,
     *  e e e e e e e b e e b e e e e,
     *  e e e e e e b b e b e e e e e,
     *  e e e e e b e b b e e e e e e,
     *  e b e e e e b b e e e e e e e,
     *  e e e e e e e b e e e e e e e.
     *  e e e e b e b b e e e e e e e,
     *  e e e b e e e e e e e e e e e,
     *  e e b e e e e e e e e e e e e,
     *  e e e e e e e e e e e e e e e
     * 
     * 
     * and it would spanw a bullet everywhere there is an B 
     * and e are just empty, and each pattern would have like 15 of these.
     */

    public char[ , ] bulletPattern1 = {
        {'-','-','-','-','-','-','-','-','-','-','-','-','r','-','-'},
        {'-','r','-','-','-','-','-','-','-','-','-','r','-','-','-'},
        {'-','-','r','-','-','-','-','-','-','-','r','-','-','-','-'},
        {'-','-','-','r','-','-','-','-','-','r','-','-','-','-','-'},
        {'-','-','-','-','r','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'}



        };

    public char[,] bulletPattern2 = {
        {'-','-','-','-','-','-','-','-','-','-','-','-','r','-','-'},
        {'-','r','-','-','-','-','-','-','-','-','-','r','-','-','-'},
        {'-','-','r','-','-','-','-','-','-','-','r','-','-','-','-'},
        {'-','-','-','r','-','-','-','-','-','r','-','-','-','-','-'},
        {'-','-','-','-','r','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','r','r','r','r','r','r','r','-','-','-','-'},
        {'-','-','-','r','-','-','-','-','-','-','-','r','-','-','-'},
        {'-','-','r','-','-','-','-','-','-','-','-','-','r','-','-'},
        {'-','r','-','-','-','-','-','-','-','-','-','-','-','r','-'},
        {'r','-','-','-','-','-','-','-','-','-','-','-','-','-','r'}



        };
    private float lastUpdateTime;
    public float timeBetweenFrames = 0.5f;

    private int counter = 0;
    // Use this for initialization
    void Start () {
        lastUpdateTime = Time.time;
      
        //for (int x = 0; x < 15 ; x++){
        //    for (int y = 0; y < 15; y++){
        //        bulletPattern1[x , y] = '-';
                
        //    }
        //}
        //bulletPattern1[5, 6] = 'r';
        
       

    }
	
	// Update is called once per frame
	void Update () {
        if(lastUpdateTime + timeBetweenFrames < Time.time){
            lastUpdateTime = Time.time;
            counter++;
            if(counter % 2 == 1){
                LoadBulletPattern(bulletPattern1);
            }else{
                LoadBulletPattern(bulletPattern2);
            }

        }

        if(Input.GetKeyDown(KeyCode.B)){
            LoadBulletPattern(bulletPattern1);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadBulletPattern(bulletPattern2);
        }
    }

    public void LoadBulletPattern(char[,] pattern){
        for (int x = 0; x < 15; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                if(pattern[x,y] == 'r'){
                    GameController.instance.gridGenerator.bulletGrid[x, y].gameObject.SetActive(true);
                }else{
                    GameController.instance.gridGenerator.bulletGrid[x, y].gameObject.SetActive(false);
                }


            }
        }
    }
    
}
