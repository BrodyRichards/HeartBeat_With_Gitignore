using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterSwitcher : MonoBehaviour
{

    //This enables you to see and set the field from inspector but 
    //it is hidden from other scripts and objects. 
    //charChoice represents which named object to move
    [HideInInspector]public static int charChoice = 3;
    public static bool isMusicGuyInCharge;

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
        //Poll for mouse click
        switchCharacter();
    }

    public int getChar()
    {
        return charChoice;
    }

    //Function to handle character switching 
    private void switchCharacter()
    {
        //Looking for 'Left Mouse Button' to be pressed
        if (Input.GetMouseButtonDown(0))
        {
            //Vector for Raycast, takes mouse position
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Decompose to 2D vector
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            //Raycast hit register for mouse position
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            //If a hit is registered, find which object was hit
            if (hit.collider != null && hit.collider.gameObject.tag == "Avatars")
            {
                //Take the name of the object and convert to int for charChoice
                string name = hit.collider.gameObject.name;
                Debug.Log(name);
                int.TryParse(name, out charChoice);
            }

            //Activate the object chosen and disable all the others
            if (charChoice!= -1)
            {
                GameObject choice = findGO(charChoice);
                Enable(choice);
                disableOthers();
            }
            
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
            //case 0:
            //    //Finding GameObject 1 which is the rabbit
            //    findGO(1).GetComponent<RabbitJump>().enabled = false;
            //    //Finding GameObject 2 which is the ball thrower
            //    findGO(2).GetComponent<BallThrow>().enabled = false;
            //    findGO(2).GetComponent<Animator>().SetBool("isThrowing", false);
            //    isMusicGuyInCharge = false;
            //    break;
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
