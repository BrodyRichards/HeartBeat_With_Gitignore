using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playground: MonoBehaviour
{
    private double worldX;
    private double worldY;
    public static double RightX;
    public static double LeftX;
    public static double LowerY;
    public static double UpperY;
    // Start is called before the first frame update
    void Start()
    {
        worldX = GameObject.Find("/Quad").transform.localScale.x / 2;
        worldY = GameObject.Find("/Quad").transform.localScale.y / 2;
        UpperY = worldY * 0.25;
        LowerY = -worldY + 15f;
        LeftX = -worldX + 20f;
        RightX = worldX - 20f;
        
    }

    
}
