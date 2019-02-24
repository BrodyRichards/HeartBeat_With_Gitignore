using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentalState : MonoBehaviour
{
    public static int mood = 0;
    private static Dictionary<string, int> moodLog = new Dictionary<string, int>();
    // Played catch, Hit by ball, Held Rabbit, Bit by rabbit, Happy Song, Sad Song, Startled Song

    // 0 = neutral 
    // 1 = happy
    // 2 = sad 
    // 3 = startled
    // 4 = angry 
    // Start is called before the first frame update
    void Start()
    {
        moodLog = new Dictionary<string, int>{
            { "Played catch", 0},
            { "Hit by ball", 0},
            { "Held Rabbit", 0 },
            { "Bit by rabbit", 0 },
            { "Happy Song", 0 },
            { "Startled Song", 0 }

        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void sendMsg(string msg)
    {
        int currCount;

        moodLog.TryGetValue(msg, out currCount);
        moodLog[msg] = currCount + 1;
        Debug.Log("Action taken: " + msg + "/Emotion Level: " + moodLog[msg]);
    }
}
