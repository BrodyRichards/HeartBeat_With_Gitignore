using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoToSchool : MonoBehaviour
{
    public GameObject bed;
    public GameObject mc;
    public GameObject emptyBed;
    public GameObject busIcon;
    private Animator animForBed;
    public RuntimeAnimatorController wakeUpBed;
    public static bool mcWokeUp;
    private float mcAppearWaitTime;

    public GameObject fadeObject;
    public GameObject thoughtBubble;
    public Vector2 mcWakeUpPosition;
    public Sprite bus;
    // Play around with the value for fade in/out
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
        if (LightController.timeToGetOutOfBed && !mcWokeUp)
        {
            thoughtBubble.SetActive(false);
            animForBed.runtimeAnimatorController = wakeUpBed;
            animForBed.SetTrigger("wakeuplo");
            mcWokeUp = true;
            Invoke("MagicMcAppear", mcAppearWaitTime);
            Invoke("fadeOut", mcAppearWaitTime - 1.0f);             //original is 0.5
            Invoke("fadeIn", mcAppearWaitTime);
            Invoke("ChangeBedToBus", mcAppearWaitTime + 1.0f);

        }

        if (Input.GetKeyDown(Control.evacuate) && LightController.timeToGetOutOfBed && mcWokeUp)
        {
            FadeToNextScene();
        }



    }

    private void MagicMcAppear()
    {
        mc.transform.position = mcWakeUpPosition;
        mc.SetActive(true);
        bed.SetActive(false);
        emptyBed.SetActive(true);

    }

    private void fadeIn()
    {
        var meh = fadeObject.GetComponent<ResultFade>();
        meh.FadeIn();
        
        //Debug.Log("Fade In");
    }

    private void fadeOut()
    {
        var meh = fadeObject.GetComponent<ResultFade>();
        meh.FadeOutStay();


        //Debug.Log("Fade Out");
    }
    void FadeToNextScene()
    {
        var meh = fadeObject.GetComponent<ResultFade>();
        meh.FadeOut();

    }

    void ChangeBedToBus()
    {
        busIcon.SetActive(true);
    }

}
