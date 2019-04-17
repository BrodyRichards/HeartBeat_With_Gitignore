using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThoughtsManager : MonoBehaviour
{
    public Text thoughtText;
    public Image thoughtBubble;

    private List<string> thoughts;
    public static Dictionary<string, int> thoughtLine;
    public static Dictionary<int, List<string>> thoughtPossibilities;

    float time;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
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

    /*
    public void DisplayThought(Thought thought)
    {
        //have different string arrays in Thought.cs for different types of thoughts?

    }
    */

    void setThoughts()
    {
        /*
        thoughts = new List<string>(new string[]{
            "First time bunny",    
            "So adorable",
            "Ouch!",
            "First time ball",
            "Awesome",
            "What a big meanie head",
            "First time music",
            "Sounds great",
            "Sounds terrible"
        });
        */
        thoughtPossibilities = new Dictionary<int, List<string>>
        {
            {0, new List<string>(new string[]{"First time bunny hold", "So adorable", "Bunny hold"}) },
            {1, new List<string>(new string[]{"First time bunny bite", "Ouch!", "Bunny bite"}) },
            {2, new List<string>(new string[]{"First time ball play", "Awesome", "Ball play"}) },
            {3, new List<string>(new string[]{"First time ball hit", "What a big meanie head", "Ball hit"}) },
            {4, new List<string>(new string[]{"First time music pos", "Sounds great", "Music pos"}) },
            {5, new List<string>(new string[]{"First time music neg", "Sounds terrible", "Music neg"}) },
            {6, new List<string>(new string[]{ "The snow is beautiful", "I'm an ice dragon", "so cold brrr", "i wanna draw "}) }
        };
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
        if (MentalState.firstTime == 0)
        {
            thoughtText.text = thoughts[0];
        }
        else
        {
            int num = thoughts.Count; int ran;
            if (line == 6) { ran = Random.Range(0, num); }
            else { ran = Random.Range(1, num); }
            
            thoughtText.text = thoughts[ran];
        }
        setTimer();
    }

    void setTimer()
    {
        time = Time.fixedUnscaledTime;
        timer = time + 3.0f;
    }

    /*
    public void TriggerDialogue()
    {
        
    }
    */

    // Update is called once per frame
    void Update()
    {
        time = Time.fixedUnscaledTime;
        //random events?
        //what will trigger thoughts
        
        if (time >= timer)
        {
            hideThought();
        }
        else //if (timer >= time)
        {
            showThought();
        }
        if (MentalState.message != "")
        {     
            int lineNum = thoughtLine[MentalState.message];
            //Debug.Log("line = " + lineNum);
            MentalState.message = "";
            changeThought(lineNum);
        }
        /*
        else if (MentalState.message == "")
        {
            
            int ran = Random.Range(0, 10);
            if (ran >= 7)
            {
                //int lineNum = 6;
                changeThought(6);
            }
            
        }
        */
    }
}
