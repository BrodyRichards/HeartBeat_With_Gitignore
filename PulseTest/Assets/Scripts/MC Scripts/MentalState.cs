using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentalState : MonoBehaviour
{
    public static int currentState;

    public static Dictionary<string, int> moodLog;
    public static Dictionary<string, int> effectWeights;

    public static int moodUpperBound = 11;
    public static int moodLowerBound = -11;
    // Played catch, Hit by ball, Held Rabbit, Bit by rabbit, Happy Song, Sad Song, Startled Song

    // -3 to 3 = neutral 
    // 4 to 10 = happy
    // -10 to -4 = sad 
    // Start is called before the first frame update
    void Start()
    {

        //Dictionary storing weights for each effect
        effectWeights = new Dictionary<string, int>
        {
            { "Played catch", 2},
            { "Hit by ball", -2},
            { "Held Rabbit", 1 },
            { "Bit by rabbit", -2},
            { "Happy Song", 1},
            { "Sad Song", -1},
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

        if (WithinRange(currentState, moodUpperBound, moodLowerBound))
        {
            currentState += effectWeights[msg];
            currentState = Mathf.Clamp(currentState, moodLowerBound + 1 , moodUpperBound - 1);
        }

        Debug.Log("Action taken: " + msg + "/Emotion Level: " + moodLog[msg] + "/Current Mood" + currentState);
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
        if (WithinRange(currentState, 3, -3))
        {
            return;
        }
        else if (currentState > 3)
        {
            currentState -= 1;
        }
        else if (currentState < -3)
        {
            currentState += 1;
        }
        
        Debug.Log("MC mood pacified " + currentState);
    }
    /// <summary>
    /// whether the val is smaller than upper (exclusively) and bigger than the lower (exclusively)
    /// </summary>
    /// <param name="val"></param>
    /// <param name="upper"></param>
    /// <param name="lower"></param>
    /// <returns>true of false</returns>
    public static bool WithinRange( int val, int upper, int lower)
    {
        return (val < upper && val > lower) ? true : false;

    }
}
