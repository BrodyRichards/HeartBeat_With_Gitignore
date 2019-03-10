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


    public static Color glowColor = Color.yellow;
    public static int outlineWidth = 5;
    public static float alphaThreshold = 0.3f;
    public static float glowBrightness = 3f;

    GameObject[] go;
    // Start is called before the first frame update
    void Start()
    {
        ballkid = GameObject.Find("2");
        radioguy = GameObject.Find("3");
        //randomkid = GameObject.Find("0");
        rabbit = GameObject.Find("1");
        go = new GameObject[]{ ballkid, radioguy, rabbit };
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
