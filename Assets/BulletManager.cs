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
    public BulletPattern firstPattern;
    private float lastUpdateTime;
    public float timeBetweenFrames = 0.5f;

    private int counter = 1;
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
            loadPatterns(firstPattern);

        }
       
    }


    public void LoadBulletPattern(ArrayLayout pattern)
    {
        for (int x = 0; x < 15; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                if (pattern.rows[x].row[y] == true)
                {
                    GameController.instance.gridGenerator.bulletGrid[x, y].gameObject.SetActive(true);
                }
                else
                {
                    GameController.instance.gridGenerator.bulletGrid[x, y].gameObject.SetActive(false);
                }


            }
        }
    }

    private void loadPatterns(BulletPattern pattern){

        if (counter == 1)
        {
            counter = 2;
            LoadBulletPattern(pattern.Frame1);
            return;
        }
        if (counter == 2)
        {
            counter = 3;
            LoadBulletPattern(pattern.Frame2);
            return;
        }
        if (counter == 3)
        {
            counter = 4;
            LoadBulletPattern(pattern.Frame3);
            return;
        }
        if (counter == 4)
        {
            counter = 5;
            LoadBulletPattern(pattern.Frame4);
            return;
        }
        if (counter == 5)
        {
            counter = 6;
            LoadBulletPattern(pattern.Frame5);
            return;
        }
        if (counter == 6)
        {
            counter = 7;
            LoadBulletPattern(pattern.Frame6);
            return;
        }
        if (counter == 7)
        {
            counter = 8;
            LoadBulletPattern(pattern.Frame7);
            return;
        }
        if (counter == 8)
        {
            counter = 9;
            LoadBulletPattern(pattern.Frame8);
            return;
        }
        if (counter == 9)
        {
            counter = 10;
            LoadBulletPattern(pattern.Frame9);
            return;
        }
        if (counter == 10)
        {
            counter = 11;
            LoadBulletPattern(pattern.Frame10);
            return;
        }
        if (counter == 11)
        {
            counter = 12;
            LoadBulletPattern(pattern.Frame11);
            return;
        }
        if (counter == 12)
        {
            counter = 13;
            LoadBulletPattern(pattern.Frame12);
            return;
        }
        if (counter == 13)
        {
            counter = 14;
            LoadBulletPattern(pattern.Frame13);
            return;
        }
        if (counter == 14)
        {
            counter = 15;
            LoadBulletPattern(pattern.Frame14);
            return;
        }
        if (counter == 15)
        {
            counter = 1;
            LoadBulletPattern(pattern.Frame15);
            return;
        }
    }

}
