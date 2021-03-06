﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playground: MonoBehaviour
{
    public readonly static float RightX = 60f;
    public readonly static float LeftX = -100f;
    public readonly static float LowerY = -20f;
    public readonly static float UpperY = -3f;

    public readonly static int trLv2 = 3;
    public readonly static int trLv3 = 5;
    public readonly static int tbLv2 = 3;
    public readonly static int tbLv3 = 5;
    public readonly static int tmLv2 = 1;
    public readonly static int tmLv3 = 3;

    public readonly static string[] NpcTypes = { "NPC", "Frank", "Bobby", "Ryan", "Suzy" };
    // Start is called before the first frame update
    void Start()
    {
        //worldX = GameObject.Find("/Quad").transform.localScale.x / 2;
        //worldY = GameObject.Find("/Quad").transform.localScale.y / 2;
        //UpperY = worldY * 0.25f;
        //LowerY = -worldY + 15f;
        //LeftX = -worldX + 20f;
        //RightX = worldX - 20f;
        //Debug.Log("uppery" + UpperY);
        //Debug.Log("lowery" + LowerY);
        //Debug.Log("leftx" + LeftX);
        //Debug.Log("rightx" + RightX);
        
    }

    public static bool CheckDist(Vector3 pos1, Vector2 pos2, float aoe)
    {
        var dist = Vector3.Distance(pos1, pos2);
        //Debug.Log(dist);
        if (dist <= aoe) { return true; }
        return false;
    }

    public static void RandomizeNpcAssets(Animator ani, SpriteRenderer sr, GameObject go)
    {
        var selected = NpcTypes[Random.Range(0, 4)];


        ani.runtimeAnimatorController = Resources.Load(selected) as RuntimeAnimatorController;
        //if (selected.Equals("NPC"))
        //{
        //    sr.flipX = true;
        //}
    }



}
