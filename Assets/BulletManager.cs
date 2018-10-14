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

    public ArrayLayout bulletPattern1;

    public char[,] bulletPattern2 = {
        {'*','*','-','-','-','-','-','-','-','-','-','-','-','*','*'},
        {'*','-','-','-','-','-','-','-','-','-','-','-','-','-','*'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'*','-','-','-','-','-','-','-','-','-','-','-','-','-','*'},
        {'*','*','-','-','-','-','-','-','-','-','-','-','-','*','*'}
        };
    public char[,] bulletPattern3 = {
        {'*','*','*','-','-','-','-','-','-','-','-','-','*','*','*'},
        {'*','*','-','-','-','-','-','-','-','-','-','-','-','*','*'},
        {'*','-','-','-','-','-','-','-','-','-','-','-','-','-','*'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'*','-','-','-','-','-','-','-','-','-','-','-','-','-','*'},
        {'*','*','-','-','-','-','-','-','-','-','-','-','-','*','*'},
        {'*','*','*','-','-','-','-','-','-','-','-','-','*','*','*'}
        };
    public char[,] bulletPattern4 = {
        {'*','*','*','*','-','-','-','-','-','-','-','*','*','*','*'},
        {'*','*','*','-','-','-','-','-','-','-','-','-','*','*','*'},
        {'*','*','-','-','-','-','-','-','-','-','-','-','-','*','*'},
        {'*','-','-','-','-','-','-','-','-','-','-','-','-','-','*'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
        {'*','-','-','-','-','-','-','-','-','-','-','-','-','-','*'},
        {'*','*','-','-','-','-','-','-','-','-','-','-','-','*','*'},
        {'*','*','*','-','-','-','-','-','-','-','-','-','*','*','*'},
        {'*','*','*','*','-','-','-','-','-','-','-','*','*','*','*'}
        };
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
        //if(lastUpdateTime + timeBetweenFrames < Time.time){
        //    lastUpdateTime = Time.time;

        //    if(counter  == 1){
        //        counter = 2;
        //        LoadBulletPattern(bulletPattern1);
        //        return;
        //    }
        //    if (counter ==2)
        //    {
        //        counter = 3;
        //        LoadBulletPattern(bulletPattern2);
        //        return;
        //    }
        //    if (counter == 3)
        //    {
        //        counter = 4;
        //        LoadBulletPattern(bulletPattern3);
        //        return;
        //    }
        //    if(counter == 4)
        //    {
        //        counter = 1;
        //        LoadBulletPattern(bulletPattern4);
        //        return;
        //    }

        //}
        LoadBulletPattern(bulletPattern1);
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
                if(pattern[x,y] == '*'){
                    GameController.instance.gridGenerator.bulletGrid[x, y].gameObject.SetActive(true);
                }else{
                    GameController.instance.gridGenerator.bulletGrid[x, y].gameObject.SetActive(false);
                }


            }
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

}
