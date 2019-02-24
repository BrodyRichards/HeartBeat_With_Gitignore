using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentalState : MonoBehaviour
{
    public static int mood = 0;
    // 0 = neutral 
    // 1 = happy
    // 2 = sad 
    // 3 = startled
    // 4 = angry 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void sendMsg(string msg, int effect)
    {
        mood += effect;
        Debug.Log("Action taken: " + msg + "/Emotion Level: " + mood);
    }
}
