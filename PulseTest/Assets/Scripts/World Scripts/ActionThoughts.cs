using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionThoughts : MonoBehaviour
{
    public GameObject q;
    public GameObject e;

    Vector3 offsetQ;
    Vector3 offsetE;
    Vector3 offsetMove;

    public GameObject bunny;            //to get the position of the avatars
    public GameObject ballKid;
    public GameObject musicKid;

    public GameObject bunnyPos;         //the images for the actions
    public GameObject bunnyNeg;
    public GameObject ballPos;
    public GameObject ballNeg;
    public GameObject musicPos;
    public GameObject musicNeg;

    public GameObject moveControls;
    bool moved = false;

    bool bunnyQ = false, bunnyE = false;                //for switching on and off
    bool ballQ = false, ballE = false; 
    bool musicQ = false, musicE = false;

    // Start is called before the first frame update
    void Start()
    {
        hideThought(q);
        hideThought(e);
        moveControls.gameObject.SetActive(false);

        offsetQ = new Vector3(-7.5f, 6, 0);
        offsetE = new Vector3(8, 6, 0);
        offsetMove = new Vector3(-1, 9, 0);
    }

    void hideThought(GameObject obj)
    {
        obj.GetComponent<Image>().gameObject.SetActive(false);
    }

    void showThought(GameObject obj)
    {
        obj.GetComponent<Image>().gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (characterSwitcher.charChoice == 1) //bunny
        {
            if (moved == false)
            {
                moveControls.gameObject.SetActive(true);
            }
            showThought(bunnyPos); showThought(bunnyNeg);
            hideThought(ballPos); hideThought(ballNeg);
            hideThought(musicPos); hideThought(musicNeg);
            if (bunnyQ == false) { showThought(q); }
            else { hideThought(q); }
            if (bunnyE == false) { showThought(e); }
            else { hideThought(e); }
            q.transform.position = bunny.transform.position + offsetQ + new Vector3(1, -1, 0);
            e.transform.position = bunny.transform.position + offsetE + new Vector3(-1, -1, 0);
            moveControls.transform.position = bunny.transform.position + offsetMove;
            if (Input.GetKey(KeyCode.Q)) { hideThought(q); bunnyQ = true; }
            if (Input.GetKey(KeyCode.E)) { hideThought(e); bunnyE = true; }
        }
        else if (characterSwitcher.charChoice == 2) //ball kid
        {
            if (moved == false)
            {
                moveControls.gameObject.SetActive(true);
            }
            showThought(ballPos); showThought(ballNeg);
            hideThought(bunnyPos); hideThought(bunnyNeg);
            hideThought(musicPos); hideThought(musicNeg);
            if (ballQ == false) { showThought(q); }
            else { hideThought(q); }
            if (ballE == false) { showThought(e); }
            else { hideThought(e); }
            q.transform.position = ballKid.transform.position + offsetQ;
            e.transform.position = ballKid.transform.position + offsetE;
            moveControls.transform.position = ballKid.transform.position + offsetMove;
            if (Input.GetKey(KeyCode.Q)) { hideThought(q); ballQ = true; }
            if (Input.GetKey(KeyCode.E)) { hideThought(e); ballE = true; }
        }
        else if (characterSwitcher.charChoice == 3) //music kid
        {
            if (moved == false)
            {
                moveControls.gameObject.SetActive(true);
            }
            showThought(musicPos); showThought(musicNeg);
            hideThought(ballPos); hideThought(ballNeg);
            hideThought(bunnyPos); hideThought(bunnyNeg);
            if (musicQ == false) { showThought(q); }
            else { hideThought(q); }
            if (musicE == false) { showThought(e); }
            else { hideThought(e); }
            q.transform.position = musicKid.transform.position + offsetQ;
            e.transform.position = musicKid.transform.position + offsetE;
            moveControls.transform.position = musicKid.transform.position + offsetMove;
            if (Input.GetKey(KeyCode.Q)) { hideThought(q); musicQ = true; }
            if (Input.GetKey(KeyCode.E)) { hideThought(e); musicE = true; }
        }
        else
        {
            hideThought(q);
            hideThought(e);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            moved = true;
            moveControls.gameObject.SetActive(false);
        }



    }
}
