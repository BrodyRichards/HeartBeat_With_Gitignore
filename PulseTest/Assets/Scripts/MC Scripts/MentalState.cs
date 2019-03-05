using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentalState : MonoBehaviour
{
    //public static int mood;

    public static Dictionary<string, int> moodLog;
    public static Dictionary<string, int> effectWeights;

    public static int moodBound = 10;
    // Played catch, Hit by ball, Held Rabbit, Bit by rabbit, Happy Song, Sad Song, Startled Song

    // -5 ~ 5 = neutral 
    // 5 up = happy
    // -5 down = sad 
    // Start is called before the first frame update
    void Start()
    {

        //Dictionary storing weights for each effect
        effectWeights = new Dictionary<string, int>
        {
            { "Played catch", 3},
            { "Hit by ball", -3},
            { "Held Rabbit", 1 },
            { "Bit by rabbit", -2},
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
        // call the mood equilibrium every 10 seconds
        InvokeRepeating("PacifyMood", 10f, 10f);
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

    public static int tallyEmotion()
    {
        var mood = 0;

        foreach (KeyValuePair<string, int> moodEntry in moodLog)
        {
            mood += moodEntry.Value * effectWeights[moodEntry.Key];
            
        }
        // Debug.Log("Total Mood: " + mood);
        return mood;
    }

    public void PacifyMood()
    {
        
        //Debug.Log("MC mood pacified " + mood);
    }
}
