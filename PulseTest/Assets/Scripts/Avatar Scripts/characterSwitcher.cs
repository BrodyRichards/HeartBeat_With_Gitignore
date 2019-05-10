using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterSwitcher : MonoBehaviour
{

    //This enables you to see and set the field from inspector but 
    //it is hidden from other scripts and objects. 
    //charChoice represents which named object to move
    public static int charChoice;
    public static bool isMusicGuyInCharge;
    //public static bool isChar = false;
    private Animator anim_keyPrompt;
    private float doNothingTimer; 
    

    // Use this for initialization
    void Awake()
    {
        //Initially disable all but the chosen one
        charChoice = -1;
        disableOthers();
        //EnableAll();
        GameObject.Find("3").GetComponent<Movement>().enabled = false;
        isMusicGuyInCharge = false;
        GameObject.Find("MC").GetComponent<Movement>().enabled = false;


        anim_keyPrompt = GameObject.Find("KeyPrompt1").GetComponent<Animator>();
        anim_keyPrompt.enabled = false;

        doNothingTimer = 0f;

        /*
        if (FlipJournal.lastAvatar == 1) { findGO(1).SetActive(false); }
        else if (FlipJournal.lastAvatar == 2) { findGO(2).SetActive(false); }
        else if (FlipJournal.lastAvatar == 3) { findGO(3).SetActive(false); }
        */
    }

    // Update is called once per frame
    void Update()
    {

        //if (charChoice == -1)
        //{
        //    PlayerIdleCheck();
        //}else if (anim_keyPrompt.enabled)
        //{
        //    anim_keyPrompt.enabled = false;
        //}
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

            if (Input.GetKeyDown(Control.evacuate) && RingUI.isCompleted && !IconControl.journalActivated)
            {
                charChoice = 1000;
                GameObject.Find("MC").GetComponent<Movement>().enabled = true;
                GameObject.Find("MC").GetComponent<Animator>().SetBool("wantToPlay", false);
                GameObject.Find("MC").GetComponent<Animator>().SetBool("isWalking", true);
                GameObject.Find("MC").GetComponent<MCBTCreator>().enabled = false;
                Movement.timeToLeave = true;
                disableOthers();
                EnableAll();
                this.enabled = false;
                NPCs.schoolBell = true;
                DisablePrompt("Q1");
                DisablePrompt("Q2");
                DisablePrompt("Q3");
                GameObject.Find("BellRing").GetComponent<AudioSource>().Play();
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
        disableOthers();
        Enable(choice);
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

                DisablePrompt("Q1");
                break;
            case 2:
                B.GetComponent<BallThrow>().enabled = true;
                DisablePrompt("Q2");
                break;
            case 3:
                isMusicGuyInCharge = true;
                DisablePrompt("Q3");
                break;
            default:
                break;
        }
    }

    //Disables a game object's script
    private void Disable(GameObject B)
    {
        if (charChoice != 1 && RabbitJump.beingCarried)
        {
            findGO(1).GetComponent<RabbitJump>().PutRabbitDown();
        }

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

    private void DisablePrompt(string target)
    {
        var go = GameObject.Find(target);
        if (go!=null) { go.SetActive(false); }
    }

    private void PlayerIdleCheck()
    {
        doNothingTimer += Time.deltaTime;

        if (doNothingTimer > 20f)
        {
            GiveSomePromptIfPlayerDoesntKnowWhatsGoingOn();
            doNothingTimer *= 0f;
        }
    }

    private void GiveSomePromptIfPlayerDoesntKnowWhatsGoingOn()
    {
        //var ran = Random.Range(1, 4);
        anim_keyPrompt.enabled = true; 
    }
}
