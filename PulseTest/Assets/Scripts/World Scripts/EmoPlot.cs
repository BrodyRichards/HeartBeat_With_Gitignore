using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoPlot : ScriptableObject
{
    // Start is called before the first frame update
    private int happenTime;
    private string causingEvent;

    public void Init(int ht, string ce)
    {
        this.happenTime = ht;
        this.causingEvent = ce;
    }

    public static EmoPlot CreateInstance(int ht, string ce)
    {
        var emo = ScriptableObject.CreateInstance<EmoPlot>();
        emo.Init(ht, ce);
        return emo;
    }

    public override string ToString()
    {
        return "happening time: " + this.Time + "causing event" + this.Event;
    }

    public int Time
    {
       get { return happenTime; }
    }

    public string Event
    {
        get { return causingEvent; }
    }
}
