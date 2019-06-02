using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThoughtsManager : MonoBehaviour
{
    public  GameObject text;
    public Image thoughtBubble;

    private List<string> thoughts;                                          //thoughts on deck
    public static Dictionary<string, int> thoughtLine;                      //a dictionary that maps action to the index of a possible list of thoughts
    public static Dictionary<int, List<string>> thoughtPossibilities;       //index to possible list of thoughts
    public static Dictionary<string, string> successiveThoughts;            //for thoughts that have a continuation

    bool thoughtOn = false;                                 //bool for if a thought is currently on or not
    bool nextThought = false;                               //used for successive thoughts

    float time;
    float timer;

    string next;                                            //used for successive thoughts

    private TextMeshProUGUI tmpug; 
    // Start is called before the first frame update
    void Start()
    {
        tmpug = text.GetComponent<TextMeshProUGUI>();

        setThoughts();
        hideThought();
      
        time = 0f;
        timer = 0f;

        thoughtLine = new Dictionary<string, int>
        {
            { "Played catch", 2},
            { "Hit by ball", 3},
            { "Held Rabbit", 0 },
            { "Bit by rabbit", 1},
            { "Happy Song", 4},
            { "Sad Song", 5},
            { "", 6 },
            {"NPC Action", 10 }
        };
    }

    void setThoughts()
    {
        thoughtPossibilities = new Dictionary<int, List<string>>
        {
            {0, new List<string>(new string[]{"It's so foofy!", "So cute!", "I want to take it home!", "I want to draw this!"}) },
            {1, new List<string>(new string[]{"Ouch!", "Go away!", "Stop it!", "Why?", "Evil rabbit"}) },
            {2, new List<string>(new string[]{"This is fun!", "Yay!", "More!", "Can we be friends?"}) },
            {3, new List<string>(new string[]{"Ow!", "What a big meanie head", "I don't like him", "Why is he doing that"}) },
            {4, new List<string>(new string[]{"Pretty music!", "I like this song", "This sounds like mommy's music"}) },
            {5, new List<string>(new string[]{"Yucky song!", "Sounds bad", "This song is ugly", "Why does she keep playing it?"}) },
            {6, new List<string>(new string[]{"Why's he so mean", "Stop it...", "I want to cry...", "What a meanie"}) }, //for the bully kid
            //Below are idle thoughts, 6 is for idle, 7 for sad, 8 for happy
            {7, new List<string>(new string[]{"Snow!", "I'm an ice dragon", "so cold brrr", "I wanna draw", "I hope daddy doesn't work too late"}) }, 
            {8, new List<string>(new string[]{"Mommy said big kids don't cry", "I didn't see daddy yesterday", "I want to go home", "*Sniffle*", "I miss mommy",
                                                "I don't like this school"}) },
            {9, new List<string>(new string[]{"I want to tell mommy about today!", "Can't wait to make new friends!", "Can't wait for class!"}) },
            //maybe I can separate some strings depending on the mood of the MC
            {10, new List<string>(new string[]{"What's going on over there?", "What happened?"}) }
        };

        successiveThoughts = new Dictionary<string, string>
        {
            {"00", "It's like a marshmallow!"},
            {"70", "I guess I'm not a big kid"},
            {"61", "Rawrrr"},
            {"80", "I wonder when I'll see her" }
        };
        
        //add thoughts for avatar actions done to NPCs
    }

    void hideThought()
    {
        thoughtBubble.GetComponent<Image>().gameObject.SetActive(false);
    }

    void showThought()
    {
        thoughtBubble.GetComponent<Image>().gameObject.SetActive(true);
    }

    void changeThought(int line)            //0 = rabbit pos, 1 = rabbit neg, 2 = ball pos, 3 = ball neg, 4 = music pos, 5 = music neg
    {
        //thoughtText.text = thoughts[line];
        thoughts = thoughtPossibilities[line];
        int successive = 999;
        if (MentalState.firstTime == 0 && line != 10)
        {
            tmpug.text = thoughts[0];
            successive = 0;
            showThought();
        }
        else
        {
            int num = thoughts.Count; int ran; int ran2;
            ran2 = Random.Range(0, 10);
            if (ran2 > 5)
            {
                if (line == 6 || line == 10) { ran = Random.Range(0, num); }
                else { ran = Random.Range(1, num); }
                tmpug.text = thoughts[ran];
                showThought();
                successive = ran;
            }
            
        }
        string ok = line.ToString() +  successive.ToString();
        if (successiveThoughts.ContainsKey(ok))
        {
            nextThought = true;
            next = successiveThoughts[ok];
        }
        else
        {
            nextThought = false;
        }
        setTimer();
    }

    void setTimer()
    {
        time = Time.fixedUnscaledTime;
        timer = time + 2.5f;
        thoughtOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.fixedUnscaledTime;
        if (NPCs.schoolBell)
        {
            hideThought();
        }
        else
        {
            if (CameraMovement.thoughtSystem)       //makes it that the MC can't get thoughts in the walking in scene
            {
                if (time >= timer)
                {
                    if (nextThought)
                    {
                        //Debug.Log("HELLOOO");
                        tmpug.text = next;
                        setTimer();
                        nextThought = false;
                    }
                    else
                    {
                        thoughtOn = false;
                        hideThought();
                    }
                }

                int ran = Random.Range(0, 1000);
                if (ran > 997 && thoughtOn == false)
                {
                    if (McMovement.speed == 4) //no mood
                    {
                        changeThought(7);
                    }
                    else if (McMovement.speed == 6) //happy
                    {
                        changeThought(9);
                    }
                    else if (McMovement.speed == 3) //sad
                    {
                        changeThought(8);
                    }
                }
                if (MentalState.message != "")
                {
                    int lineNum = thoughtLine[MentalState.message];
                    Debug.Log("message: " + MentalState.message);
                    //Debug.Log("lineNum: " + lineNum);
                    MentalState.message = "";
                    changeThought(lineNum);
                }
                int ran2 = Random.Range(0, 1000);
                if (ran2 > 995 && Runners.bullying)
                {
                    changeThought(6);
                }
            }
        }
    }
}
