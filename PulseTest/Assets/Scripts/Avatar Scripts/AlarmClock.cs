using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmClock : MonoBehaviour
{
    public GameObject mc;
    private float time;
    private float timer;

    private bool alarm = false;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.fixedUnscaledTime;
        if (Input.GetKeyDown(Control.positiveAction))
        {
            timer = time + 3.0f;
            alarm = true;
            //mc.GetComponent<McMovement>().tutorialScene = true;
            //mc.GetComponent<McMovement>().endScene = true;
        }
        if (timer <= time && alarm)
        {
            alarm = false;
            mc.GetComponent<McMovement>().tutorialScene = true;
            mc.GetComponent<McMovement>().endScene = true;
        }
    }
}
