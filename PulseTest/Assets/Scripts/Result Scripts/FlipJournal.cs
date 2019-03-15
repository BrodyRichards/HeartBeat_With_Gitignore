﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipJournal : MonoBehaviour
{
    public GameObject[] journalPages;
    public GameObject[] journalDrawings; // 0 rabbit+ 1 rabbit- 2 ballkid+ 3 ballkid- 4 music+ 5 music-
    public GameObject speechBubble1;
    public GameObject speechBubble2;
    public static bool finishReadingJournal = false;
    private int currentIndex = 0;
    private bool[] haveYouReadMeYet;
    // Start is called before the first frame update
    void Start()
    {
        speechBubble1.SetActive(false);
        speechBubble2.SetActive(false);
        foreach(var j in journalPages)
        {
            j.SetActive(false);
        }

        haveYouReadMeYet = new bool[]{ false, false, false };
    }

    // Update is called once per frame
    void Update()
    {
        if (EndJournal.journalIsOpened)
        {
            speechBubble1.SetActive(true);
            speechBubble2.SetActive(true);
            EnableThisDisableRest(currentIndex, journalPages);
            haveYouReadMeYet[currentIndex] = true;
            whichDrawingsToShow(currentIndex);
            if (Input.GetKeyDown(KeyCode.A))
            {
                speechBubble1.SetActive(false);
                speechBubble2.SetActive(false);
                currentIndex -= 1;
                if (currentIndex == -1)
                {
                    currentIndex = 0;
                    CloseJournalAndReset();
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                speechBubble1.SetActive(false);
                speechBubble2.SetActive(false);
                currentIndex += 1;
                if (currentIndex == 3)
                {
                    currentIndex = 2;
                    CloseJournalAndReset();
                }
            }

        }
        else
        {
            CloseJournalAndReset();
        }
        
        if (!finishReadingJournal)
        {
            finishReadingJournal = CheckReadingStatus();
        }
    }

    void whichDrawingsToShow(int index)
    {
        switch (index)
        {
            case 0:
                if (EventHasPositiveResult("Held Rabbit", "Bit by rabbit"))
                {
                    EnableThisDisableRest(0, journalDrawings);
                }
                else
                {
                    EnableThisDisableRest(1, journalDrawings);
                }

                break;
            case 1:
                if (EventHasPositiveResult("Played catch", "Hit by ball"))
                {
                    EnableThisDisableRest(2, journalDrawings);
                }
                else
                {
                    EnableThisDisableRest(3, journalDrawings);
                }
                break;
            case 2:
                if (EventHasPositiveResult("Happy Song", "Sad Song"))
                {
                    EnableThisDisableRest(4, journalDrawings);
                }
                else
                {
                    EnableThisDisableRest(5, journalDrawings);
                }
                break;
            default:
                break;
        }
        
    }


    void EnableThisDisableRest(int index, GameObject[] go)
    {
        var sel = go[index];

        sel.SetActive(true);
        foreach (var j in go)
        {
            if (!j.Equals(sel))
            {
                j.SetActive(false);
            }
        }
    }

    void CloseJournalAndReset()
    {
        journalPages[currentIndex].SetActive(false);
        foreach(var d in journalDrawings) { d.SetActive(false); }
        currentIndex = 0;
        EndJournal.journalIsOpened = false;
        
    }

    bool CheckReadingStatus()
    {
        var tempBool = true;
        foreach(var b in haveYouReadMeYet)
        {
            tempBool &= b;
        }
        return tempBool;
    }

    bool EventHasPositiveResult(string pos, string neg)
    {
        if (MentalState.moodLog != null)
        {
            if (MentalState.moodLog[pos] > MentalState.moodLog[neg])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;

        
    }

   
}