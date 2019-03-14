using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipJournal : MonoBehaviour
{
    public GameObject[] journalPages;
    public static bool finishReadingJournal = false;
    private int currentIndex = 0;
    private bool[] haveYouReadMeYet;
    // Start is called before the first frame update
    void Start()
    {
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
            EnableThisDisableRest(currentIndex);
            haveYouReadMeYet[currentIndex] = true;
            if (Input.GetKeyDown(KeyCode.A))
            {
                currentIndex -= 1;
                if (currentIndex == -1)
                {
                    currentIndex = 0;
                    CloseJournalAndReset();
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
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

    void EnableThisDisableRest(int index)
    {
        var sel = journalPages[index];

        sel.SetActive(true);
        foreach (var j in journalPages)
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
}
