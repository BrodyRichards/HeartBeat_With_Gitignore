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
            CurtainsClosed.SetActive(false);
            CurtainsOpen.SetActive(true);
            //CurtainsClosed.GetComponent<Renderer>().enabled = true;
            mc.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            mc.GetComponent<McFreeMove>().enabled = true;
            if (GameObject.Find("Q") != null)
            {
                GameObject.Find("Q").SetActive(false);
            }
            
            //mc.GetComponent<McMovement>().tutorialScene = true;
            //mc.GetComponent<McMovement>().endScene = true;
        }
    }
}