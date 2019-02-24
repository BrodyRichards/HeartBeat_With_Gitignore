using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionTracker: MonoBehaviour
{
    public static float emotionLevel;

    // Start is called before the first frame update
    void Start()
    {
        emotionLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sendMsg(string msg, float emotionGain)
    {
        emotionLevel += emotionGain;
        Debug.Log("Action taken: " + msg + "/Emotion Level: " + emotionLevel);
    }
}
