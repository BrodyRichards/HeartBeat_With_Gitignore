using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playground: MonoBehaviour
{
    private float worldX;
    private float worldY;
    public static float RightX;
    public static float LeftX;
    public static float LowerY;
    public static float UpperY;
    // Start is called before the first frame update
    void Start()
    {
        worldX = GameObject.Find("/Quad").transform.localScale.x / 2;
        worldY = GameObject.Find("/Quad").transform.localScale.y / 2;
        UpperY = worldY * 0.25f;
        LowerY = -worldY + 15f;
        LeftX = -worldX + 20f;
        RightX = worldX - 20f;
        
    }

    
}
