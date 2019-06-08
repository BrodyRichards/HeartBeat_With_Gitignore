using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BedtimeProcedure : MonoBehaviour
{
    // Game objects
    public GameObject journalIcon;
    public GameObject sleepIcon;
    public GameObject tabIcon;
    public GameObject bed;
    public GameObject fadeObject;
    public GameObject dream;
    public GameObject rabAsset;
    public GameObject ballAsset;
    public GameObject musicAsset;
    public GameObject charlie;
    public GameObject charlieWriting;
    public GameObject moversThought;
    public GameObject journal;


    // Animators
    private Animator anim;
    private Animator bedAnim;
    private bool goToBedProcedureCalled;

    public static bool journalIsOpened;
    public static bool writingJournal;
    public static bool charlieInBed;
    private bool thought = false;

    private float time;// = 0f;
    private float timer;// = 0f;
    // Start is called before the first frame update
    private void Awake()
    {
        journalIsOpened = false;
        charlieInBed = false;
        goToBedProcedureCalled = false;
        writingJournal = false;
        rabAsset.gameObject.SetActive(false);
        ballAsset.gameObject.SetActive(false);
        musicAsset.gameObject.SetActive(false);
        time = 0f;
        timer = 0f;

    }
    void Start()
    {
        anim = journalIcon.GetComponent<Animator>();
        bedAnim = bed.GetComponent<Animator>();

        journalIcon.SetActive(false);
        tabIcon.SetActive(false);

        MentalState.PrintMoodLog();
        MentalState.PrintEmoTimeline();
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.fixedUnscaledTime; 
        Debug.Log("night is here = " + LightController.nightIsHere);
        if (time >= timer)
        {
            //Debug.Log(time);
            moversThought.SetActive(false);
            
        }
        if (writingJournal && !charlieInBed)
        {
            if (thought == false)
            {
                moversThought.SetActive(true);
                timer = time + 3f;
                thought = true;
            }
            journal.SetActive(true);
            charlie.SetActive(false);
            charlieWriting.SetActive(true);
        }
        if (LightController.nightIsHere)
        {
            ReadJournal();
            FinishJournal();
        }



    }

    private void FinishJournal()
    {
        if (FlipJournal.finishReadingJournal && !journalIsOpened && !AutoToSchool.mcWokeUp)
        {
            
            if (!charlieInBed)
            {
                sleepIcon.SetActive(true);
            }
            journalIcon.SetActive(false);


            tabIcon.SetActive(false);
            if (Input.GetKeyDown(Control.evacuate) && !goToBedProcedureCalled)
            {
                goToBedProcedureCalled = true;
                GoToBedProcedure();

                
            }
        }
    }

    private void GoToBedProcedure()
    {
        if (charlieWriting.activeSelf)
        {
            //Invoke("fadeOut", 0.5f);
            
            Invoke("CharlieGoAway", 1.7f);
            Invoke("fadeIn", 1.7f);
            Invoke("GoToBedPlsKid", 2f);
            Invoke("Dreaming", 8f);
            Invoke("TurnToMorn", 15f);

        }
    }

    void TurnToMorn()
    {
        LightController.morningIsHere = true;
    }

    void CharlieGoAway()
    {
        journal.SetActive(false);
        charlieWriting.SetActive(false);
        LightController.turnOffRoomLights = true;
        charlieInBed = true;
        sleepIcon.SetActive(false);


    }

    void GoToBedPlsKid()
    {
        bedAnim.SetBool("goToBed", true);
    }



    void Dreaming()
    {
        dream.gameObject.SetActive(true);
        int avatar = MentalState.DecideFriend();
        Debug.Log("Friend: " + avatar);
        /*
        if (avatar == 0)
        {
            //what should happen?
            
        }
        */
        if (avatar == 1)
        {
            rabAsset.gameObject.SetActive(true);
        }
        else if (avatar == 2)
        {
            ballAsset.gameObject.SetActive(true);
        }
        else if (avatar == 3)
        {
            musicAsset.gameObject.SetActive(true);
        }
        else
        {
            dream.gameObject.SetActive(false);
        }
    }

    private void fadeOut()
    {
        Debug.Log("fadeOut called");
        var meh = fadeObject.GetComponent<ResultFade>();
        meh.FadeOutStay();
    }

    private void fadeIn()
    {
        Debug.Log("fadeIn called");

        var meh = fadeObject.GetComponent<ResultFade>();
        meh.FadeIn();
    }

    private void ReadJournal()
    {
        if (!journalIcon.activeSelf && !charlieInBed) 
        {
            journalIcon.SetActive(true);
            anim.SetBool("newAccom", true);
            tabIcon.SetActive(true);
        }
        else
        {
            if (Input.GetKeyDown(Control.pullJournal) && !journalIsOpened && !charlieInBed)
            {

                journalIsOpened = true;
                tabIcon.SetActive(false);

            }
            else if (Input.GetKeyDown(Control.pullJournal) && journalIsOpened && !charlieInBed)
            {
                journalIsOpened = false;
            }
        }
    }

}
