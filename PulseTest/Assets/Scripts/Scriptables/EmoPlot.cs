using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoPlot : ScriptableObject
{
    // this is the class object that store the event's happening time, the event type, and the MC's mood when that happen
    // all these member variables are readonly after they are created, can't be modified 
    
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
        return "happening time: " + this.Time + "/causing event: " + this.Event + "/MC current mood: " + this.Mood;
    }
    
    // use as emoPlotObject.Time
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
