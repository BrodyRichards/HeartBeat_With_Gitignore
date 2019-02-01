using SpriteGlow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
    private GameObject ballkid; //2
    private GameObject radioguy; // 3
    private GameObject randomkid; //0
    private GameObject rabbit; //1

    GameObject[] go;
    // Start is called before the first frame update
    void Start()
    {
        ballkid = GameObject.Find("/Bushes/2");
        radioguy = GameObject.Find("/Bushes/3");
        randomkid = GameObject.Find("/Bushes/0");
        rabbit = GameObject.Find("/Bushes/1");
        go = new GameObject[]{ ballkid, radioguy, randomkid, rabbit };
        ballkid.GetComponent<SpriteGlowEffect>().enabled = true;
        //Debug.Log(ballkid);
        Debug.Log(ballkid.name);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject tempGo in go)
        {
            
            if (tempGo.name != characterSwitcher.charChoice.ToString())
            {
                tempGo.GetComponent<SpriteGlowEffect>().enabled = false;
            }
            else
            {
                tempGo.GetComponent<SpriteGlowEffect>().enabled = true;
            }

                
        }

    }
}
