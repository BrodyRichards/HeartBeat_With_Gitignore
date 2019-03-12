using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoPlot : ScriptableObject
{
    // Start is called before the first frame update
    private int happenTime;
    private int mood;
    private string causingEvent;
    

    public void Init(int ht, int moo, string ce)
    {
        this.happenTime = ht;
        this.causingEvent = ce;
        this.mood = moo;
    }

    public static EmoPlot CreateInstance(int ht, int moo, string ce)
    {
        var emo = ScriptableObject.CreateInstance<EmoPlot>();
        emo.Init(ht, moo, ce);
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

    public int Mood
    {
        get { return mood; }
    }
}
