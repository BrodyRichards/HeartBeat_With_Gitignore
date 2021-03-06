﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialProcControl : MonoBehaviour
{
    public GameObject bed;
    public GameObject emptyBed;
    public GameObject mc;
    public GameObject writingCharlie;
    private Animator animForBed;

    public Light pointLight;
    public Light directionalLight;
    public GameObject fadeObject;

    public GameObject waypointObjs;
    public static bool writingJournal;

    private bool mcWokeUp;
    private float mcAppearWaitTime; // Play around with the value for fade in/out
    private bool mcReappear;

    //public static bool nextScene = false;
    // Start is called before the first frame update
    void Start()
    {
        animForBed = bed.GetComponent<Animator>();
        mcWokeUp = false;
        mcAppearWaitTime = 4f;
        writingJournal = false;
        mcReappear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (writingJournal)
        {
            mc.SetActive(false);
            writingCharlie.SetActive(true);
            writingJournal = false;
        }

        if (TutorialJournal.journalOpenOnce && !mcReappear)
        {
            mc.transform.position = new Vector2(4, -3);
            mc.SetActive(true);
            writingCharlie.SetActive(false);
            mcReappear = true;
        }
        if (TutorialCharSwitch.WakeActionChosen && !mcWokeUp)
        {

            animForBed.SetTrigger("wakeuplo");
            mcWokeUp = true;
            Invoke("MagicMcAppear", mcAppearWaitTime);
            Invoke("fadeOut", mcAppearWaitTime - 1.0f);             //original is 0.5
            Invoke("fadeIn", mcAppearWaitTime);
            //Invoke("showWaypoints", mcAppearWaitTime);
        }

        if (mcWokeUp && TutorialCharSwitch.TutCharChoice==1)
        {
            //pointLight.enabled = false;
            if (directionalLight.intensity <= 1.5f)
            {
                directionalLight.intensity += 0.05f;
            }
            pointLight.intensity = 5f;
        }
    }

    private void MagicMcAppear()
    {
        mc.SetActive(true);
        TutorialSpirit.goToMC = true;
        bed.SetActive(false);
        emptyBed.SetActive(true);
        
    }

    private void fadeIn()
    {
        var meh = fadeObject.GetComponent<TutorialFade>();
        meh.FadeIn();
        //Debug.Log("Fade In");
    }

    private void fadeOut()
    {
        //nextScene = true;
        var meh = fadeObject.GetComponent<TutorialFade>();
        meh.FadeOutStay();
        //Debug.Log("Fade Out");
    }
    /*
    private void showWaypoints()
    {
        waypointObjs.SetActive(true);
    }
    */

}
