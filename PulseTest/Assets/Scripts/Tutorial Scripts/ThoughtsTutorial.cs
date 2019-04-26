﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThoughtsTutorial : MonoBehaviour
{
    public GameObject text;
    public Image thoughtBubble;

    private List<string> thoughts;                                          //thoughts on deck
    //public static Dictionary<string, int> thoughtLine;                      //a dictionary that maps action to the index of a possible list of thoughts
    public static Dictionary<int, List<string>> thoughtPossibilities;       //index to possible list of thoughts
    //public static Dictionary<string, string> successiveThoughts;            //for thoughts that have a continuation

    bool thoughtOn = false;
    //bool nextThought = false;

    float time;
    float timer;

    int action = 99;

    //string next;

    private TextMeshProUGUI tmpug;
    // Start is called before the first frame update
    void Start()
    {
        tmpug = text.GetComponent<TextMeshProUGUI>();

        setThoughts();
        hideThought();

        time = 0f;
        timer = 0f;
    }

    void setThoughts()
    {
        thoughtPossibilities = new Dictionary<int, List<string>>
        {
            {0, new List<string>(new string[]{"Time for school!"}) },
            {1, new List<string>(new string[]{"I don't want to go to school"}) },
        };
        //add thoughts for avatar actions done to NPCs
    }

    void changeThought()            //0 = rabbit pos, 1 = rabbit neg, 2 = ball pos, 3 = ball neg, 4 = music pos, 5 = music neg
    {
        //thoughtText.text = thoughts[line];
        thoughts = thoughtPossibilities[action];
        int num = thoughts.Count; int ran; 
        ran = Random.Range(0, num);
        tmpug.text = thoughts[ran];
        showThought();
        setTimer();
    }

    void setTimer()
    {
        time = Time.fixedUnscaledTime;
        timer = time + 2.5f;
        thoughtOn = true;
    }

    void hideThought()
    {
        thoughtBubble.GetComponent<Image>().gameObject.SetActive(false);
    }

    void showThought()
    {
        thoughtBubble.GetComponent<Image>().gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.fixedUnscaledTime;
        if (time >= timer)
        {
            hideThought();
        }
        if (Input.GetKey(KeyCode.Q))
        {
            action = 0;
            Invoke("changeThought", 5f);
        }
        if (Input.GetKey(KeyCode.E))
        {
            action = 1;
            Invoke("changeThought", 9f);
        }
    }
}