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
    public static Dictionary<int[], string> successiveThoughts;

    bool thoughtOn = false;
    bool nextThought = false;

    float time;
    float timer;

    string next;
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
        thoughtPossibilities = new Dictionary<int, List<string>>
        {
            {0, new List<string>(new string[]{"It's so foofy!", "So cute!", "I want to take it home!"}) },
            {1, new List<string>(new string[]{"Ouch!", "Go away!", "Stop it!"}) },
            {2, new List<string>(new string[]{"This is fun!", "Yay!", "More!", "Can we be friends?"}) },
            {3, new List<string>(new string[]{"Ow!", "What a big meanie head", "I don't like him"}) }, //mommy said big kids don't cry, I guess I'm not a big kid
            {4, new List<string>(new string[]{"Pretty music!", "I like this song", "This sounds like mommy's music"}) },
            {5, new List<string>(new string[]{"Yucky song!", "Sounds bad", "This song is ugly"}) },
            {6, new List<string>(new string[]{ "Snow!", "I'm an ice dragon", "so cold brrr", "i wanna draw", "I didn't see daddy yesterday", "I miss mommy"}) },
            {7, new List<string>(new string[]{"Mommy said big kids don't cry"}) }
            //maybe I can separate some strings depending on the mood of the MC
        };

        successiveThoughts = new Dictionary<int[], string>
        {
            {new int[2] {0, 0}, "It's like a marshmallow!"},
            {new int[2] {7, 0}, "I guess I'm not a big kid"}
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
        int successive = 999;
        if (MentalState.firstTime == 0)
        {
            thoughtText.text = thoughts[0];
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
                thoughtText.text = thoughts[ran];
                showThought();
                //int[] check = new int[2] { line, ran };
                //Debug.Log("check: " + check);
                successive = ran;
                /*
                if (successiveThoughts[check] != null)
                {
                    Debug.Log()
                    nextThought = true;
                    next = successiveThoughts[check];
                }
                */
                
            }
            
            
        }
        //successive = 0;
        int[] check = new int[2] { line, successive };
        Debug.Log("check: " + check[0] + check[1]);
        Debug.Log("is it in? : " + successiveThoughts.ContainsKey(check));
        if (successiveThoughts.ContainsKey(check))
        {
            Debug.Log("yay");
            nextThought = true;
            next = successiveThoughts[check];
        }
        setTimer();
    }

    void setTimer()
    {
        time = Time.fixedUnscaledTime;
        timer = time + 3.0f;
        thoughtOn = true;
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
            if (nextThought)
            {
                Debug.Log("HELLOOO");
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
        /*
        else //if (time <= timer && thoughtOn == false)//if (timer >= time)
        {
            if (thoughtOn)
            {
                showThought();
            }
            
        }
        */
           
        int ran = Random.Range(0, 1000);
        if (ran > 997 && thoughtOn == false)
        {
                //int lineNum = 6;
            changeThought(6);

        }
        if (MentalState.message != "")
        {     
            int lineNum = thoughtLine[MentalState.message];
            //string followUpThought = lineNum.ToString();
            //followUpThought.
            //Debug.Log(followUpThought);
            //Debug.Log("line = " + lineNum);
            Debug.Log("lineNum: " + lineNum);
            MentalState.message = "";
            changeThought(lineNum);
        }
        if (Input.GetKey(KeyCode.T))
        {
            changeThought(7);
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
