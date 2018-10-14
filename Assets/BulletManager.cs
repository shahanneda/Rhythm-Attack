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

    public char[ , ] bulletPattern1 = new char[15 , 15];
    // Use this for initialization
    void Start () {
   
      
        for (int x = 0; x < 15 ; x++){
            for (int y = 0; y < 15; y++){
                bulletPattern1[x , y] = 'r';
            }
        }


       

    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey(KeyCode.L)){
            //GameController.instance.gridGenerator.bulletGrid[2, 2].gameObject.SetActive(false);
            LoadBulletPattern();
        }
	}

    public void LoadBulletPattern(){
        for (int x = 0; x < 15; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                if(bulletPattern1[x,y] == 'r'){
                    GameController.instance.gridGenerator.bulletGrid[x, y].gameObject.SetActive(true);
                }


            }
        }
    }
    
}
