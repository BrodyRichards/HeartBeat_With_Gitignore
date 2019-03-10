using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtains : MonoBehaviour
{
    public GameObject CurtainsOpen;
    public GameObject CurtainsClosed;
    public GameObject mc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OpenCurtains();
    }

    public void OpenCurtains()
    {
        if (Input.GetKeyDown(Control.positiveAction))
        {
            Debug.Log("Activating Curtains");
            TutorialCharSwitch.WakeActionChosen = true;
            CurtainsOpen.GetComponent<Renderer>().enabled = false;
            //CurtainsClosed.GetComponent<Renderer>().enabled = true;
            mc.GetComponent<McMovement>().tutorialScene = true;
            mc.GetComponent<McMovement>().endScene = true;
        }
    }
}