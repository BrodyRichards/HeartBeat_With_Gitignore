using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThoughtsManager : MonoBehaviour
{
    public Text thoughtText;
    public Image thoughtBubble;
    //public Thought thought;

    private List<string> thoughts;
    //private string[] thoughts;
    public static Dictionary<string, int> thoughtLine;

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
        thoughts = new List<string>(new string[]{
            "So adorable",
            "Ouch!",
            "Awesome",
            "What a big meanie head",
            "Sounds great",
            "Sounds terrible"
        });
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
        thoughtText.text = thoughts[line];
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
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            showThought();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            hideThought();
        }
        
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
            Debug.Log("line = " + lineNum);
            MentalState.message = "";
            changeThought(lineNum);
        }
    }
}
