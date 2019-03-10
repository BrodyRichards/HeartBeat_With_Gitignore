using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterSwitcher : MonoBehaviour
{

    //This enables you to see and set the field from inspector but 
    //it is hidden from other scripts and objects. 
    //charChoice represents which named object to move
    public static int charChoice = -1;
    public static bool isMusicGuyInCharge;
    //public static bool isChar = false;

    // Use this for initialization
    void Start()
    {
        //Initially disable all but the chosen one
        disableOthers();
        GameObject.Find("3").GetComponent<Movement>().enabled = false;
        isMusicGuyInCharge = false;
        GameObject.Find("MC").GetComponent<Movement>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        //Poll for input
        switchCharacter();
    }

    //Function to handle character switching 
    private void switchCharacter()
    {
        if (!PauseUI.IsPaused)
        {
            if (Input.GetKeyDown(Control.toRabbit))
            {
                charChoice = 1;
                PerformSwitch();
            }
            else if (Input.GetKeyDown(Control.toBallKid))
            {
                charChoice = 2;
                PerformSwitch();
            }
            else if (Input.GetKeyDown(Control.toMusicKid))
            {
                charChoice = 3;
                PerformSwitch();
            }
            else if (Input.GetKeyDown(Control.pullJournal))
            {
                if (IconControl.journalActivated)
                {
                    Time.timeScale = 1.0f;
                    IconControl.journalActivated = false;
                    Debug.Log("Journal Deactivated");
                }
                else
                {
                    Time.timeScale = 0.0f;
                    IconControl.journalActivated = true;
                    Debug.Log("Journal Activated");
                }
            }

            if (Input.GetKeyDown(Control.evacuate))
            {
                charChoice = 1000;
                GameObject.Find("MC").GetComponent<Movement>().enabled = true;
                GameObject.Find("MC").GetComponent<McMovement>().enabled = false;
                Movement.timeToLeave = true;
                EnableAll();
                this.enabled = false;
            }
        }
        

            //Activate the object chosen and disable all the others
        
    }

    //This function loops through all the other ones not chosen 
    //and disables their movement script
    private void disableOthers()
    {
        for (int i = 1; i < 4; i++)
        {
            if (charChoice != i)
            {
                GameObject B = findGO(i);
                Disable(B);
            }
        }
    }

    private void PerformSwitch()
    {
        GameObject choice = findGO(charChoice);
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
        if(charChoice == 1 && RabbitJump.beingCarried)
        {

        }
        else
        {
            B.GetComponent<Movement>().enabled = true;
        }

        switch (charChoice)
        {
            case 1:
                B.GetComponent<RabbitJump>().enabled = true;
                GameObject.Find("Q1").SetActive(false);
                break;
            case 2:
                B.GetComponent<BallThrow>().enabled = true;
                GameObject.Find("Q2").SetActive(false);
                break;
            case 3:
                isMusicGuyInCharge = true;
                GameObject.Find("Q3").SetActive(false);
                break;
            default:
                break;
        }
    }

    //Disables a game object's script
    private void Disable(GameObject B)
    {
        B.GetComponent<Movement>().enabled = false;
        if (charChoice == -1)
        {
            findGO(1).GetComponent<RabbitJump>().enabled = false;
            findGO(2).GetComponent<BallThrow>().enabled = false;
            isMusicGuyInCharge = false;
        }

        switch (charChoice)
        {
            case 1:
                findGO(2).GetComponent<BallThrow>().enabled = false;
                findGO(2).GetComponent<Animator>().SetBool("isThrowing", false);
                findGO(2).GetComponent<Animator>().SetBool("isWalking", false);
                findGO(3).GetComponent<Animator>().SetBool("isWalking", false);
                isMusicGuyInCharge = false;
                break;
            case 2:
                findGO(1).GetComponent<RabbitJump>().enabled = false;
                findGO(1).GetComponent<Animator>().SetBool("isWalking", false);
                findGO(3).GetComponent<Animator>().SetBool("isWalking", false);
                isMusicGuyInCharge = false;
                break;
            case 3:
                findGO(2).GetComponent<BallThrow>().enabled = false;
                findGO(2).GetComponent<Animator>().SetBool("isThrowing", false);
                findGO(1).GetComponent<RabbitJump>().enabled = false;
                findGO(1).GetComponent<Animator>().SetBool("isWalking", false);
                findGO(2).GetComponent<Animator>().SetBool("isWalking", false);
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
        return charChoice;
    }
}
