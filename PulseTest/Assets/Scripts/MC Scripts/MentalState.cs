using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentalState : MonoBehaviour
{
    public static int currentState;
    public static int currentActionCombo;
    public static float coolDownCounting = 0.0f;

    public static Dictionary<string, int> moodLog = new Dictionary<string, int>
        {
            { "Played catch", 0},
            { "Hit by ball", 0},
            { "Held Rabbit", 0 },
            { "Bit by rabbit", 0 },
            { "Happy Song", 0 },
            { "Sad Song", 0 },
            { "Bullied", 0}
        };
    public static Dictionary<string, int> effectWeights;
    public static Dictionary<int, int> npcEffectWeights;
    /// <summary>
    /// the entire timeline of events to store event type, happening time, Charlie current mood 
    /// </summary>
    public static Queue<EmoPlot> emoTimeline;
    /// <summary>
    /// the number of positive interaction minus negative for deciding friend and bedtime journal outcome
    /// </summary>
    public static Dictionary<int, int> relationships = new Dictionary<int, int>
        {
            {1, 0},
            {2, 0},
            {3, 0}
        };
    /// <summary>
    /// Keeps track of the number of interactions, regardless good or bad, that happen with each avatar
    /// </summary>
    public static Dictionary<int, int> interactions = new Dictionary<int, int>
        {
            {1, 0},
            {2, 0},
            {3, 0}
        };

    public readonly static Vector2Int happyBound = new Vector2Int(6, 30);
    public readonly static Vector2Int sadBound = new Vector2Int(-30, -6);
    public readonly static Vector2Int normalBound = new Vector2Int(-5, 5);
    public readonly static List<string> positiveAct 
        = new List<string>() {  "Held Rabbit", "Played catch", "Happy Song" };
    public readonly static List<string> negativeAct 
        = new List<string>() {  "Bit by rabbit", "Hit by ball", "Sad Song", "Bullied" };

    public static bool journalInProgress; // for the journal
    public static float noEventCounting; // for the journal 

    public static string message;          //for the thought system
    public static int firstTime = 99;           //for the thought system

    // Played catch, Hit by ball, Held Rabbit, Bit by rabbit, Happy Song, Sad Song
    private void Awake()
    {
        journalInProgress = true;
        noEventCounting = 0.0f;
        currentActionCombo = 0;

        currentState = 0;
    }
    
    void Start()
    {
        emoTimeline = new Queue<EmoPlot>() { };
        message = "";
        //Dictionary storing weights for NPC interactions
        npcEffectWeights = new Dictionary<int, int>
        {
            {1, 1},     //rabbit hold
            {2, -1},    //rabbit bite
            {3, 1},     //ball play
            {4, -1},    //ball throw
            {5, 1},     //happy music
            {6, -1}     //sad music
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
            { "Bullied", -2 }
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
            { "Bullied", 0}
        };

        relationships = new Dictionary<int, int>
        {
            {1, 0},
            {2, 0},
            {3, 0}
        };

        interactions = new Dictionary<int, int>
        {
            {1, 0},
            {2, 0},
            {3, 0}
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

        int currCount;
        message = msg;
        RingUI.fuckingString = msg;
        Debug.Log("message = " + message);
        moodLog.TryGetValue(msg, out currCount);
        firstTime = moodLog[msg];
        moodLog[msg] = currCount + 1;
        UpdateEmoWithAction(msg);
        if (journalInProgress)
        {
            EventTracking(msg);
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


        //return 0;
    }
    //============== End of TODO ==========================




    // After some thinking, I think Peter made a good point with the queue thing
    // If the player wants to redeem their previous actions, they can by doing enough actions
    // That the beginning effects that go out of bound will get dequeue and won't be considered 

    public static void PlotingTimeline(string m)
    {
        var tempEmo = EmoPlot.CreateInstance(Mathf.RoundToInt(Time.timeSinceLevelLoad), currentState, m);
        emoTimeline.Enqueue(tempEmo);
        // if the emotiontime line already contain more than 24 objects, dequeue 
        if (emoTimeline.Count > 24) // can playaround with the value 
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
        if (WithinRange(currentState, sadBound.x, happyBound.y))
        {
            currentState += npcEffectWeights[newAction];
            currentState = Mathf.Clamp(currentState, sadBound.x, happyBound.y);
        }
        message = "NPC Action";
        //currentState = Mathf.Clamp(currentState, sadBound.x, happyBound.y);
        //Debug.Log("Updating emotion");
        //Debug.Log("Current State: " + currentState);
    }

    public void PacifyMood()
    {
        noEventCounting *= 0.0f;

        if (WithinRange(currentState, normalBound.x, normalBound.y))
        {
            return;
        }
        else if (WithinRange(currentState, happyBound.x, happyBound.y))
        {


            currentState -= 3;
        }
        else if (WithinRange(currentState, sadBound.x, sadBound.y))
        {

            currentState += 2;
        }

        Debug.Log("MC mood pacified " + currentState);
    }
    /// <summary>
    /// Decides the friend of Charlie based on positive inteaction minus negative interaction, if equal bunny gets the upperhand
    /// </summary>
    /// <returns>The friend. 0=No friend 1=Rabbit 2=Ball kid 3=Music girl</returns>
    public static int DecideFriend()
    {
        relationships[1] = moodLog["Held Rabbit"] - moodLog["Bit by rabbit"];
        relationships[2] = moodLog["Played catch"] - moodLog["Hit by ball"];
        relationships[3] = moodLog["Happy Song"] - moodLog["Sad Song"];



        int friend = 1;
        if (relationships[1] <= 1 && relationships[2] <= 1 && relationships[3] <= 1)
        {
            return 0; // Charlie didn't make friend boo hoo
        }

         // secretly prioritizes bunny keke 

        else if (relationships[2] > relationships[friend])
        {
            friend = 2;
            //return 2;
        }

        else if (relationships[3] > relationships[friend])
        {
            friend = 3;
            //return 3;
        }
        Debug.Log("friend" + friend);

        return friend;
        //return 1;
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

    public static void EventTracking(string msg)
    {
        if (msg != "Bullied")
        {
            var index = positiveAct.IndexOf(msg) == -1 ? negativeAct.IndexOf(msg) : positiveAct.IndexOf(msg);
            interactions[index + 1] = moodLog[positiveAct[index]] + moodLog[negativeAct[index]];
            JournalTween.accomplishments[index].Num = interactions[index + 1];
        }
            
    }



    //------------------- Debug Print functions-----------------------------
    public static void PrintMoodLog()
    {
        foreach (KeyValuePair<string, int> element in moodLog)
        {
            Debug.LogFormat("Action={0}, Count={1}", element.Key, element.Value.ToString());
        }
    }

    public static void PrintEmoTimeline()
    {
        foreach(EmoPlot ep in emoTimeline)
        {
            Debug.LogFormat("Action={0}, Happening time={1}, currentMood={2}", ep.Event, ep.Time.ToString(), ep.Mood.ToString());
        }
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
    // [obsolete]
    public static void ByeByeOldEmoSystem(string msg)
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
    }

    public static void UpdateEmoWithAction(string act)
    {
        if (NewEmoControl.NoEmoAtm)
        {
            NewEmoControl.ReactEmo = act;
        }
    }
}
