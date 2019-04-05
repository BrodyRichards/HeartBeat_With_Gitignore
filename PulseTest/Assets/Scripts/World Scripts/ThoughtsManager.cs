using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThoughtsManager : MonoBehaviour
{
    public Text thoughtText;
    public Image thoughtBubble;

    private Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void DisplayThought(Thought thought)
    {
        //have different string arrays in Thought.cs for different types of thoughts?

    }

    void hideThought()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //random events?
        //what will trigger thoughts
    }
}
