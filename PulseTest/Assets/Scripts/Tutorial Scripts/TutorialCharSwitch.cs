using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCharSwitch : MonoBehaviour
{
    //This enables you to see and set the field from inspector but 
    //it is hidden from other scripts and objects. 
    //charChoice represents which named object to move
    public static int TutCharChoice = -1;
    public static bool WakeActionChosen = false;

    public GameObject one;
    public GameObject two;
    public GameObject Q;
    public GameObject E; 
    //public static bool isChar = false;

    // Use this for initialization
    void Start()
    {
        //Initially disable all but the chosen one
        disableOthers();
    }

    // Update is called once per frame
    void Update()
    {
        //Poll for input
        if (!WakeActionChosen)
        {
            switchCharacter();
        }
        //switchCharacter();
    }

    //Function to handle character switching 
    private void switchCharacter()
    {
        if (!PauseUI.IsPaused)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("On the alarm clock");
                TutCharChoice = 1;
                PerformSwitch();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("On the curtain");
                TutCharChoice = 2;
                PerformSwitch();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("On the car");
                TutCharChoice = 3;
                PerformSwitch();
            }
            //else if (Input.GetKeyDown(Control.pullJournal))
            //{
            //    if (IconControl.journalActivated)
            //    {
            //        Time.timeScale = 1.0f;
            //        IconControl.journalActivated = false;
            //        Debug.Log("Journal Deactivated");
            //    }
            //    else
            //    {
            //        Time.timeScale = 0.0f;
            //        IconControl.journalActivated = true;
            //        Debug.Log("Journal Activated");
            //    }
            //}
        }


        //Activate the object chosen and disable all the others

    }

    //This function loops through all the other ones not chosen 
    //and disables their movement script
    private void disableOthers()
    {
        for (int i = 1; i < 4; i++)
        {
            if (TutCharChoice != i)
            {
                GameObject B = findGO(i);
                Disable(B);
            }
        }
    }

    private void PerformSwitch()
    {
        GameObject choice = findGO(TutCharChoice);
        Enable(choice);
        disableOthers();
    }

    //Helper function for finding game objects
    private GameObject findGO(int i)
    {
        string choice = i.ToString();
        GameObject someB = GameObject.Find(choice);
        return someB;
    }

    //Enables a game object's script
    private void Enable(GameObject B)
    {
        switch (TutCharChoice)
        {
            case 1:
                B.GetComponent<AlarmClock>().enabled = true;
                EnablePrompts(E, Q);
                break;
            case 2:
                B.GetComponent<Curtains>().enabled = true;
                EnablePrompts(Q, E);
                break;
            case 3:
                B.GetComponent<ToyCar>().enabled = true;
                break;
            default:
                break;
        }
    }

    //Disables a game object's script
    private void Disable(GameObject B)
    {
        switch (TutCharChoice)
        {
            case -1:
                findGO(1).GetComponent<AlarmClock>().enabled = false;
                findGO(2).GetComponent<Curtains>().enabled = false;
                break;
            case 1:
                findGO(2).GetComponent<Curtains>().enabled = false;
                //findGO(3).GetComponent<ToyCar>().enabled = false;
                //findGO(2).GetComponent<Animator>().SetBool("isThrowing", false);
                //findGO(2).GetComponent<Animator>().SetBool("isWalking", false);
                //findGO(3).GetComponent<Animator>().SetBool("isWalking", false);
                break;
            case 2:
                findGO(1).GetComponent<AlarmClock>().enabled = false;
                //findGO(3).GetComponent<ToyCar>().enabled = false;
                //findGO(1).GetComponent<Animator>().SetBool("isWalking", false);
                //findGO(3).GetComponent<Animator>().SetBool("isWalking", false);
                break;
            case 3:
                findGO(1).GetComponent<AlarmClock>().enabled = false;
                findGO(2).GetComponent<Curtains>().enabled = false;
                //findGO(2).GetComponent<Animator>().SetBool("isThrowing", false);
                //findGO(1).GetComponent<Animator>().SetBool("isWalking", false);
                //findGO(2).GetComponent<Animator>().SetBool("isWalking", false);
                break;
            default:
                break;
        }
    }

    public void EnableAll()
    {
        for (int i = 1; i < 4; i++)
        {
            Debug.Log("Enabling: " + i);
            GameObject C = findGO(i);
            Enable(C);
        }
    }

    

    public int getChar()
    {
        return TutCharChoice;
    }

    private void EnablePrompts(GameObject toEnable, GameObject toDisable)
    {
        one.SetActive(false);
        two.SetActive(false);
        toEnable.SetActive(true);
        toDisable.SetActive(false);

    }
}
