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

    public GameObject bunny;
    public GameObject ballKid;
    public GameObject musicKid;

    public Image bunnyPos;
    public Image bunnyNeg;
    public Image ballPos;
    public Image ballNeg;
    public Image musicPos;
    public Image musicNeg;

    bool bunnyQ = false, bunnyE = false;                //for switching on and off
    bool ballQ = false, ballE = false; 
    bool musicQ = false, musicE = false;

    // Start is called before the first frame update
    void Start()
    {
        hideThought(q);
        hideThought(e);

        offsetQ = new Vector3(-9, 6, 0);
        offsetE = new Vector3(9, 6, 0);
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
            if (bunnyQ == false) { showThought(q); }
            if (bunnyE == false) { showThought(q); }
            q.transform.position = bunny.transform.position + offsetQ + new Vector3(1, -1, 0);
            e.transform.position = bunny.transform.position + offsetE + new Vector3(-1, -1, 0);
            if (Input.GetKey(KeyCode.Q)) { hideThought(q); bunnyQ = true; }
            if (Input.GetKey(KeyCode.E)) { hideThought(e); bunnyE = true; }
        }
        else if (characterSwitcher.charChoice == 2) //ball kid
        {
            q.transform.position = ballKid.transform.position + offsetQ;
            e.transform.position = ballKid.transform.position + offsetE;
        }
        else if (characterSwitcher.charChoice == 3) //music kid
        {
            q.transform.position = musicKid.transform.position + offsetQ;
            e.transform.position = musicKid.transform.position + offsetE;
        }
            

    }
}
