using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterSwitcher : MonoBehaviour
{

    //This enables you to see and set the field from inspector but 
    //it is hidden from other scripts and objects. 
    //charChoice represents which named object to move
    public static int charChoice = 3;
    public static bool isMusicGuyInCharge;
    //public static bool isChar = false;

    // Use this for initialization
    void Start()
    {
        //Initially disable all but the chosen one
        disableOthers();
        GameObject.Find("3").GetComponent<Movement>().enabled = false;
        isMusicGuyInCharge = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Poll for input
        switchCharacter();
    }

    public int getChar()
    {
        return charChoice;
    }

    //Function to handle character switching 
    private void switchCharacter()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            charChoice = 1;
        }else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            charChoice = 2;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            charChoice = 3;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Journal Activated");
        }

            //Activate the object chosen and disable all the others
        if (charChoice != -1)
        {
            GameObject choice = findGO(charChoice);
            Enable(choice);
            disableOthers();
        }
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
                GameObject.Find("Q1").GetComponent<SpriteRenderer>().enabled = false;
                break;
            case 2:
                B.GetComponent<BallThrow>().enabled = true;
                GameObject.Find("Q2").GetComponent<SpriteRenderer>().enabled = false;
                break;
            case 3:
                isMusicGuyInCharge = true;
                GameObject.Find("Q3").GetComponent<SpriteRenderer>().enabled = false;
                break;
            default:
                break;
        }
    }

    //Disables a game object's script
    private void Disable(GameObject B)
    {
        B.GetComponent<Movement>().enabled = false;

        switch (charChoice)
        {
            case 1:
                findGO(2).GetComponent<BallThrow>().enabled = false;
                findGO(2).GetComponent<Animator>().SetBool("isThrowing", false);
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

                break;
            default:
                break;
        }
    }
}
