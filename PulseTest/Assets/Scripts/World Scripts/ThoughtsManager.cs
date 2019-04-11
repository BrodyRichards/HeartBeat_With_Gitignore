using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThoughtsManager : MonoBehaviour
{
    public Text thoughtText;
    public Image thoughtBubble;
    public Thought thought;

    private List<string> thoughts;

    float time;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        hideThought();
        thoughts = new List<string>();
        time = 0f;
        timer = 0f;

        foreach (string thought in thought.thoughts)
        {
            thoughts.Add(thought);
        }
        thoughtText.text = thoughts[1];
    }

    /*
    public void DisplayThought(Thought thought)
    {
        //have different string arrays in Thought.cs for different types of thoughts?

    }
    */

    void hideThought()
    {
        thoughtBubble.GetComponent<Image>().gameObject.SetActive(false);
    }

    void showThought()
    {
        thoughtBubble.GetComponent<Image>().gameObject.SetActive(true);
    }

    void changeThought(int line)
    {

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
    }
}
