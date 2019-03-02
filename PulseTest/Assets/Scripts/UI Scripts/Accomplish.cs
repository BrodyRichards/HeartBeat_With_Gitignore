using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Accomplish : ScriptableObject
{
    // Start is called before the first frame update
    public float[] alpha;
    public int[] threshold;
    public readonly bool[] finished;

    public Image[] images;


    private int interactNum;

    public Accomplish(int[] val, Image one, Image two, Image three)
    {
        interactNum = 0;
        alpha = new float[] { 0f, 0f, 0f };
        finished = new bool[] { false, false, false };
        threshold = val;
        images = new Image[] { one, two, three };
    }

    public int Num
    {
        get { return interactNum; }
        set { interactNum = value; }
    }
}
