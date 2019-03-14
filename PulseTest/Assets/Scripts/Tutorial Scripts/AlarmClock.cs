using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmClock : MonoBehaviour
{
    public GameObject mc;
    private float time;
    private float timer;
    private AudioSource ass;

    private bool alarm = false;
    // Start is called before the first frame update
    void Start()
    {
        ass = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.fixedUnscaledTime;
        if (Input.GetKeyDown(Control.negativeAction))
        {
            if (GameObject.Find("E") != null)
            {
                GameObject.Find("E").SetActive(false);
            }
            
            Debug.Log("Alarm went off");
            timer = time + 3.0f;
            if (!ass.isPlaying) { ass.Play(); }
            alarm = true;
            //mc.GetComponent<McMovement>().tutorialScene = true;
            //mc.GetComponent<McMovement>().endScene = true;
        }

        if (timer <= time && alarm)
        {
            alarm = false;
            ass.Stop();
            TutorialCharSwitch.WakeActionChosen = true;
            mc.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            mc.GetComponent<McFreeMove>().enabled = true;
            //mc.GetComponent<McMovement>().tutorialScene = true;
            //mc.GetComponent<McMovement>().endScene = true;
        }
    }
}
