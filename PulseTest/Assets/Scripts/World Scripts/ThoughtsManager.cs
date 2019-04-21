using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThoughtsManager : MonoBehaviour
{
    public  GameObject text;
    public Image thoughtBubble;

    private List<string> thoughts;
    public static Dictionary<string, int> thoughtLine;
    public static Dictionary<int, List<string>> thoughtPossibilities;
    public static Dictionary<string, string> successiveThoughts;

    bool thoughtOn = false;
    bool nextThought = false;

    float time;
    float timer;

    string next;

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
            { "", 6 }
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
            //Below are idle thoughts, 6 is for idle, 7 for sad, 8 for happy
            {6, new List<string>(new string[]{"Snow!", "I'm an ice dragon", "so cold brrr", "I wanna draw", "I hope daddy doesn't work too late"}) }, 
            {7, new List<string>(new string[]{"Mommy said big kids don't cry", "I didn't see daddy yesterday", "I want to go home", "*Sniffle*", "I miss mommy",
                                                "I don't like this school"}) },
            {8, new List<string>(new string[]{"I want to tell mommy about today!", "Can't wait to make new friends!", "Can't wait for class!"}) }
            //maybe I can separate some strings depending on the mood of the MC
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
        if (MentalState.firstTime == 0)
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
                if (line == 6) { ran = Random.Range(0, num); }
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

        if (CameraMovement.thoughtSystem)
        {
            if (time >= timer)
            {
                if (nextThought)
                {
                    //Debug.Log("HELLOOO");
                    thoughtText.text = next;
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
                    changeThought(6);
                }
                else if (McMovement.speed == 6) //happy
                {
                    changeThought(8);
                }
                else if (McMovement.speed == 3) //sad
                {
                    changeThought(7);
                }
            }
            if (MentalState.message != "")
            {
                int lineNum = thoughtLine[MentalState.message];

                Debug.Log("lineNum: " + lineNum);
                MentalState.message = "";
                changeThought(lineNum);
            }
        }
        
        
        
        /*
        if (Input.GetKey(KeyCode.T))
        {
            changeThought(7);
        }
        */
    }
}
