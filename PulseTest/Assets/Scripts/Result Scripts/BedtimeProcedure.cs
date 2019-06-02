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

    public GameObject dayIndicator;

    // Animators
    private Animator anim;
    private Animator bedAnim;
    private bool goToBedProcedureCalled;

    public static bool journalIsOpened;

    public static bool charlieInBed;
    // Start is called before the first frame update
    private void Awake()
    {
        journalIsOpened = false;
        charlieInBed = false;
        goToBedProcedureCalled = false;
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
        Debug.Log("night is here = " + LightController.nightIsHere);
        if (LightController.nightIsHere)
        {
            ReadJournal();
            FinishJournal();
        }

        dayIndicator.GetComponent<TextMeshProUGUI>().text = "Day " + (IterationController.dayCount + 1);

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
        if (GameObject.Find("MC") != null)
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
        GameObject.Find("MC").SetActive(false);
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
        if (avatar == 0)
        {
            //what should happen?
            dream.gameObject.SetActive(false);
        }
        else if (avatar == 1)
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
