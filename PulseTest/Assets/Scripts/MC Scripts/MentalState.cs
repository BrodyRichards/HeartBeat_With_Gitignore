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

    public static bool journalInProgress = true;
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
        if (journalInProgress)
        {
            EventTracking();
            CheckForTween();
            UpdateCurrentMood(msg);
        }
        
        Debug.Log("Action taken: " + msg + "/Emotion Level: " + moodLog[msg] + "/Current Mood" + currentState);
    }

    public static void CheckForTween()
    {
        bool doTweening = false;
        foreach (var comm in JournalTween.accomplishments)
        {
            doTweening = doTweening || ActivateTweening(comm);
        }
        
        // check if doing the tween is needed 
        IconControl.journalTweening = doTweening;

    }

    public static bool ActivateTweening(Accomplish com)
    {
        //Debug.LogError("com.Num :" + com.Num + "com.threshold :" + com.threshold[0] + "com.finished :" + com.finished[0]);
        if (com.Num > com.threshold[0] && !com.finished[0])
        {
            return true;
        }
        else if (com.Num > com.threshold[1] && !com.finished[1])
        {
            return true;
        }
        else if (com.Num > com.threshold[2] && !com.finished[2])
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void EventTracking()
    {

        JournalTween.ball.Num = moodLog["Played catch"] + moodLog["Hit by ball"];
        JournalTween.rabbit.Num = moodLog["Held Rabbit"] + moodLog["Bit by rabbit"];
        JournalTween.music.Num = moodLog["Happy Song"] + moodLog["Sad Song"];
    }

    public static void UpdateCurrentMood(string m)
    {
        if (WithinRange(currentState, moodUpperBound, moodLowerBound))
        {
            currentState += effectWeights[m];
            currentState = Mathf.Clamp(currentState, moodLowerBound + 1, moodUpperBound - 1);
        }
    }
    

    public static int tallyEmotion()
    {
        var mood = 0;

        foreach (KeyValuePair<string, int> moodEntry in moodLog)
        {
            mood += moodEntry.Value * effectWeights[moodEntry.Key];

        }

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

    //-------------------------------
    //Helper functions
    /// <summary>
    /// whether the val is smaller than upper (exclusively) and bigger than the lower (exclusively)
    /// </summary>
    /// <param name="val"></param>
    /// <param name="upper"></param>
    /// <param name="lower"></param>
    /// <returns>true of false</returns>
    public static bool WithinRange(int val, int upper, int lower)
    {
        return (val < upper && val > lower) ? true : false;

    }
}
