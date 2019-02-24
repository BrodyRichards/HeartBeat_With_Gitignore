using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playground: MonoBehaviour
{
    private float worldX;
    private float worldY;
    public static float RightX = 120f;
    public static float LeftX = -120f;
    public static float LowerY = -25f;
    public static float UpperY = 10f;
    public static float MusicAoe = 20f;

    public static string[] NpcTypes = { "NPC", "Frank", "Bobby", "Ryan", "Suzy" };
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

    public static void RandomizeNpcAssets(Animator ani, SpriteRenderer sr)
    {
        string selected = NpcTypes[Random.Range(0, 4)];
        ani.runtimeAnimatorController = Resources.Load(selected) as RuntimeAnimatorController;
        if (selected.Equals("NPC"))
        {
            sr.flipX = true;
        }
    }
}
