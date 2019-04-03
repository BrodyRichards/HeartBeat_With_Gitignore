using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialProcControl : MonoBehaviour
{
    public GameObject bed;
    public GameObject emptyBed;
    public GameObject mc;
    private Animator animForBed;

    public GameObject pointLight;
    public GameObject directionalLight;
    public GameObject fadeObject;

    private bool mcWokeUp;
    private float mcAppearWaitTime; // Play around with the value for fade in/out
    // Start is called before the first frame update
    void Start()
    {
        animForBed = bed.GetComponent<Animator>();
        mcWokeUp = false;
        mcAppearWaitTime = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        if (TutorialCharSwitch.WakeActionChosen && !mcWokeUp)
        {
            if (TutorialCharSwitch.TutCharChoice == 2)
            {
                directionalLight.SetActive(true);
                pointLight.GetComponent<Light>().intensity = 1;
            }
            animForBed.SetTrigger("wakeuplo");
            mcWokeUp = true;
            Invoke("MagicMcAppear", mcAppearWaitTime);
            //var meh = fadeObject.GetComponent<TutorialFade>();
            //meh.FadeOut();
            //meh.FadeIn();
            Invoke("fadeOut", mcAppearWaitTime - 0.5f);
            Invoke("fadeIn", mcAppearWaitTime);
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
    }

    private void fadeOut()
    {
        var meh = fadeObject.GetComponent<TutorialFade>();
        meh.FadeOutStay();
    }


}
