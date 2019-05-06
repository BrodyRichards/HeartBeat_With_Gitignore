using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndJournal : MonoBehaviour
{
    public GameObject journalIcon;
    public GameObject sleepIcon;
    public GameObject tabIcon;
    public GameObject bed;
    public GameObject emptyBed;
    public GameObject fadeObject;
    public static bool journalIsOpened;
    public static bool deemLight;
    private Animator anim;
    private Animator bedAnim;

    public GameObject dream;
    public GameObject rabAsset;
    public GameObject ballAsset;
    public GameObject musicAsset;
    // Start is called before the first frame update
    private void Awake()
    {
        journalIsOpened = false;
        deemLight = false;
    }
    void Start()
    {
        anim = journalIcon.GetComponent<Animator>();
        bedAnim = bed.GetComponent<Animator>();
        bed.GetComponent<SpriteRenderer>().enabled = false;
        journalIcon.SetActive(false);
        tabIcon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (LightController.isNightGlowFinish && !journalIcon.activeSelf)
        {
            journalIcon.SetActive(true);
            anim.SetBool("newAccom", true);
            tabIcon.SetActive(true);
        }

        if (journalIcon.activeSelf)
        {
            if (Input.GetKeyDown(Control.pullJournal) && !journalIsOpened)
            {
                
                journalIsOpened = true;
                tabIcon.SetActive(false);

            }
            else if (Input.GetKeyDown(Control.pullJournal) && journalIsOpened)
            {
                journalIsOpened = false;
            }
        }

        if (FlipJournal.finishReadingJournal && !journalIsOpened)
        {
            sleepIcon.SetActive(true);
            anim.SetBool("newAccom", false);
            tabIcon.SetActive(false);
            if (Input.GetKeyDown(Control.evacuate))
            {
                if (GameObject.Find("MC") != null)
                {
                    bed.GetComponent<SpriteRenderer>().enabled = true;
                    emptyBed.GetComponent<SpriteRenderer>().enabled = false;

                    //Invoke("fadeOut", 0.1f);
                    Invoke("fadeOut", 0.5f);
                    Invoke("CharlieGoAway", 1f);
                    Invoke("fadeIn", 1.25f);
                    Invoke("GoToBedPlsKid", 2f);
                    Invoke("Dreaming", 9f);
                    Invoke("DeemTheLight", 12f);

                }
                
            }
        }
    }

    void CharlieGoAway()
    {
        GameObject.Find("MC").SetActive(false);
    }

    void GoToBedPlsKid()
    {
        bedAnim.SetBool("goToBed", true);
        
    }

    void DeemTheLight()
    {
        var meh = fadeObject.GetComponent<ResultFade>();
        meh.FadeOut();
    }

    void Dreaming()
    {
        dream.gameObject.SetActive(true);
        int avatar = 1;
        if (MentalState.moodLog["Played catch"] > avatar) { avatar = 2; }
        else if (MentalState.moodLog["Happy Song"] > 3) { avatar = 3; }
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
        //dream.gameObject.SetActive(true);
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

}
