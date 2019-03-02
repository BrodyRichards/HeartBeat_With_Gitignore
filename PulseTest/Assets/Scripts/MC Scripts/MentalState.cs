using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentalState : MonoBehaviour
{
    public static int mood = 0;
    public static Dictionary<string, int> moodLog;
    public static Dictionary<string, int> effectWeights;
    // Played catch, Hit by ball, Held Rabbit, Bit by rabbit, Happy Song, Sad Song, Startled Song

    // 0 = neutral 
    // 1 = happy
    // 2 = sad 
    // 3 = startled
    // 4 = angry 
    // Start is called before the first frame update
    void Start()
    {
        //Dictionary storing weights for each effect
        effectWeights = new Dictionary<string, int>
        {
            { "Played catch", 3},
            { "Hit by ball", -3},
            { "Held Rabbit", 1 },
            { "Bit by rabbit", 2},
            { "Happy Song", 1},
            { "Sad Song", -1},
            { "Startled Song", -1}
        };

        //Dictionary storing number of interactions
        moodLog = new Dictionary<string, int>
        {
            { "Played catch", 0},
            { "Hit by ball", 0},
            { "Held Rabbit", 0 },
            { "Bit by rabbit", 0 },
            { "Happy Song", 0 },
            { "Sad Song", 0 },
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
        tallyEmotion();
    }

    public static void tallyEmotion()
    {
        foreach(KeyValuePair<string, int> moodEntry in moodLog)
        {
            mood += moodEntry.Value * effectWeights[moodEntry.Key];
        }
        Debug.Log("Total Mood: " + mood);
    }
}
