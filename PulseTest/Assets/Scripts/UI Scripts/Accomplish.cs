using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Accomplish : ScriptableObject
{
    // Start is called before the first frame update
    public float[] alpha;
    public int[] threshold;
    public bool[] finished = new bool[] { false, false, false };

    public Image[] images;


    private int interactNum;

    public void Init(int[] val, Image one, Image two, Image three)
    {
        this.interactNum = 0;
        this.alpha = new float[] { 0f, 0f, 0f };
        this.threshold = val;
        this.images = new Image[] { one, two, three };
    }

    public static Accomplish CreateInstance(int[] val, Image one, Image two, Image three)
    {
        var accom = ScriptableObject.CreateInstance<Accomplish>();
        accom.Init(val, one, two, three);
        return accom;
    }

    public int Num
    {
        get { return interactNum; }
        set { interactNum = value; }
    }
}
