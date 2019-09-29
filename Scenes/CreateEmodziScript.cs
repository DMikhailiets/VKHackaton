using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;
using UnityEngine.UI;

public class CreateEmodziScript : MonoBehaviour
{

    public Transform prefab;
    public Canvas canvas;
    public Sprite sprt;
    System.Random rand;
    Vector3 clubPos;
    Vector3 clubPosRefl;
    int n = 0;
    bool firstTime = true;

    void InitClubPos()
    {
        if (!firstTime)
            return;
        firstTime = false;
        if(rand == null)
            rand = new System.Random();
        clubPos = new Vector3((float)( -0.7*rand.NextDouble() - 1.4), (float)(2.0 * rand.NextDouble() + 3.5), 0);
        Debug.Log(clubPos);
        clubPosRefl = clubPos;
        clubPosRefl.x = -clubPosRefl.x;
    }

    // Start is called before the first frame update
    public void CreateEmodzi(Sprite spr,string text,bool club)
    {
        InitClubPos();
        n++;
        if (n > 2)
            club = false;
      //  var img = prefab.gameObject.GetComponent<SpriteRenderer>();
      //  img.sprite = spr;
        Vector3 pos;
       // if (club)
       //     pos = (n == 1 ? clubPos : clubPosRefl);
       // else
            pos = getPos();
       
        Debug.Log(clubPos);
        var sprite = Instantiate(prefab, pos,Quaternion.identity,canvas.transform);
        sprite.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        sprite.GetComponent<Image>().sprite = spr;
        //sprite.GetComponent<Image>().sprite = spr;
    }

    void Start()
    {
        CreateEmodzi(sprt,"Сочи",true);
        CreateEmodzi(sprt,"Сочи",true);
        CreateEmodzi(sprt,"Сочи",true);
        CreateEmodzi(sprt,"Сочи",true);
    }

    Vector3 getPos()
    {
        Vector3 pos;
        if (rand.Next(3) != 0)
            pos = new Vector3(Random.Range(20f,180f), Random.Range(-200f,800f), 0);
        else
        {
            pos = new Vector3(Random.Range(750f,950f),  Random.Range(-200f,800f), 0);
           // pos.x = pos.x * (rand.Next(2) == 0 ? 1 : -1);
        }
        return pos;
    }


   /* Vector3 getPos()
    {
        Vector3 pos;
        if (rand.Next(3) != 0)
            pos = new Vector3((float)(5.4 * rand.NextDouble() ), (float)(2 * rand.NextDouble()*300 + 4*300), 0);
        else
        {
            pos = new Vector3((float)(0.6*rand.NextDouble()*300 ), (float)(5 * rand.NextDouble()*300 - 1*300), 0);
            pos.x = pos.x * (rand.Next(2) == 0 ? 1 : -1);
        }
        return pos;
    }*/



    // Update is called once per frame
    void Update()
    {
        
    }
}
