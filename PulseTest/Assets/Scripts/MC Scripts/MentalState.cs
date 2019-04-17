using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentalState : MonoBehaviour
{
    public static int currentState;
    public static int currentActionCombo;
    public static float coolDownCounting = 0.0f;
    public static string lastEvent;

    public static Dictionary<string, int> moodLog;
    public static Dictionary<string, int> effectWeights;
    public static Dictionary<int, int> npcEffectWeights;
    public static Queue<EmoPlot> emoTimeline;


    
    public static bool journalInProgress;
    public static float noEventCounting;

    public readonly static Vector2Int happyBound = new Vector2Int(6, 30);
    public readonly static Vector2Int sadBound = new Vector2Int(-30, -6);
    public readonly static Vector2Int normalBound = new Vector2Int(-5, 5);
    public readonly static int comboBound = 4;
    public readonly static List<string> positiveAct 
        = new List<string>() { "Played catch", "Held Rabbit", "Happy Song" };
    public readonly static List<string> negativeAct 
        = new List<string>() { "Hit by ball", "Bit by rabbit", "Sad Song" };

    public static string message = "";          //for the thought system
    public static int firstTime = 99;           //for the though system

    // Played catch, Hit by ball, Held Rabbit, Bit by rabbit, Happy Song, Sad Song
    private void Awake()
    {
        journalInProgress = true;
        noEventCounting = 0.0f;
        lastEvent = "";
        currentActionCombo = 0;
    }
    void Start()
    {
        emoTimeline = new Queue<EmoPlot> { };

        //Dictionary storing weights for NPC interactions
        npcEffectWeights = new Dictionary<int, int>
        {
            {1, 1},
            {2, -1},
            {3, 1},
            {4, -1},
            {5, 1},
            {6, -1}
        };

        //Dictionary storing weights for each effect
        effectWeights = new Dictionary<string, int>
        {
            { "Played catch", 2},
            { "Hit by ball", -3},
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
        
    }

    // Update is called once per frame
    void Update()
    {
        noEventCounting += Time.deltaTime;
        
        //Debug.Log(noEventCounting);
        // if more than 10 seconds without event happening, do the pacifying thing 
        if (noEventCounting > 10f)
        {
            PacifyMood();
        }
    }

    public static void sendMsg(string msg)
    {
        if (positiveAct.Contains(msg) || negativeAct.Contains(msg))
        {
            message = msg;
            var before = currentActionCombo;
            var comboFactor = positiveAct.Contains(msg) ? 1 : -1;
            currentActionCombo += comboFactor;
            if (currentActionCombo == 0) { currentActionCombo = comboFactor; }
            var after = currentActionCombo;
            if (before * after <= 0) { EmoControl.emoChanged = true; }
        }
        int currCount;
        moodLog.TryGetValue(msg, out currCount);
        firstTime = moodLog[msg];
        moodLog[msg] = currCount + 1;
        
        if (journalInProgress)
        {
            EventTracking();
            CheckForTween();
        }
        UpdateCurrentMood(msg);
        PlotingTimeline(msg);
        noEventCounting *= 0.0f;
        Debug.Log("Action taken: " + msg + "/Event count: " + moodLog[msg] + "/Current Mood: " + currentState + "/happening time: " + Mathf.RoundToInt(Time.timeSinceLevelLoad));
    }
    
   

    
    //=================Justin's TODO ============================== 
    /// <summary>
    /// this function should only be called after the playground scene is finished! The formfactor for how 
    /// the end scene light will look
    /// </summary>
    /// <returns>(int) Charlie's overall mood in the end</returns>
    public static int OverallResult()
    {
        var average = 0;
        if (emoTimeline != null)
        {
            foreach (var emo in emoTimeline)
            {
                average += emo.Mood;
            }
            if (emoTimeline.Count != 0)
            {
                average /= emoTimeline.Count;
            }
        }
            
        return (int)average;


        return 0;
    }
    //============== End of TODO ==========================




    // After some thinking, I think Peter made a good point with the queue thing
    // If the player wants to redeem their previous actions, they can by doing enough actions
    // That the beginning effects that go out of bound will get dequeue and won't be considered 

    public static void PlotingTimeline(string m)
    {
        var tempEmo = EmoPlot.CreateInstance(Mathf.RoundToInt(Time.timeSinceLevelLoad), currentState, m);
        emoTimeline.Enqueue(tempEmo);
        lastEvent = m;
        // if the emotiontime line already contain more than 30 objects, dequeue 
        if (emoTimeline.Count > 30) // can playaround with the value 
        {
            emoTimeline.Dequeue();
        }
    }

    //Helper functions
    /// <summary>
    /// whether the val is smaller than upper (inclusively) and bigger than the lower (inclusively)
    /// </summary>
    /// <param name="val"></param>
    /// <param name="lower"></param>
    /// <param name="upper"></param>
    /// <returns>true of false</returns>
    public static bool WithinRange(int val, int lower, int upper)
    {
        return (val <= upper && val >= lower) ? true : false;
    }

    public static void UpdateCurrentMood(string m)
    {
        if (WithinRange(currentState, sadBound.x, happyBound.y))
        {
            currentState += effectWeights[m];
            currentState = Mathf.Clamp(currentState, sadBound.x, happyBound.y);
        }
    }

    public static void UpdateNPCMood(int newAction)
    {
        //Add up the actions taken in NPC event queue and add it to currentState
        //Queue<int> tempQueue = new Queue<int>();
        //tempQueue = NPCs.actions;

        /*foreach(int action in tempQueue)
        {
            Debug.Log("Updating emotion");
            currentState += npcEffectWeights[action];
            Debug.Log("Current State: " + currentState);
        }*/
        currentState += npcEffectWeights[newAction];
        //Debug.Log("Updating emotion");
        Debug.Log("Current State: " + currentState);
    }

    public void PacifyMood()
    {
        noEventCounting *= 0.0f;
        currentActionCombo = -999;

        if (WithinRange(currentState, normalBound.x, normalBound.y))
        {
            return;
        }
        else if (WithinRange(currentState, happyBound.x, happyBound.y))
        {

            var msg = "Calm down";
            PlotingTimeline(msg);
            currentState -= 3;
        }
        else if (WithinRange(currentState, sadBound.x, sadBound.y))
        {
            var msg = "Cheer up";
            PlotingTimeline(msg);
            currentState += 2;
        }

        Debug.Log("MC mood pacified " + currentState);
    }
    

    //--------------Functions for Journals--------------------------------------

    public static void CheckForTween()
    {
        var doTweening = false;
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
        if ((com.Num > com.threshold[0] && !com.finished[0]) ||
            (com.Num > com.threshold[1] && !com.finished[1]) ||
            (com.Num > com.threshold[2] && !com.finished[2]))
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
    //------------------------------------------------------------------

    //[Obsolete]
    public static void CheckCooldown()
    {
        //if (!Mathf.Approximately(coolDownCounting, float.Epsilon))
        //{
        //    // if the action is still on cool down, skip function
        //    return;
        //}
        //else if (Mathf.Abs(currentActionCombo) == comboBound)
        //{
        //    // if the action exceeds the combo bound, skip function
        //    Debug.Log("ResetEmoControl");
        //    coolDownCounting = 5.0f;
        //    return;
        //}

        //if (!Mathf.Approximately(coolDownCounting, float.Epsilon) && coolDownCounting > 0.0f)
        //{
        //    coolDownCounting -= Time.deltaTime;
        //}
        //else
        //{
        //    coolDownCounting = 0.0f;
        //}

    }

    // [obsolete] 
    public static int tallyEmotion()
    {
        var mood = 0;

        foreach (KeyValuePair<string, int> moodEntry in moodLog)
        {
            mood += moodEntry.Value * effectWeights[moodEntry.Key];

        }

        return mood;
    }
}
