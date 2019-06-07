using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlipJournal : MonoBehaviour
{
    public GameObject mc;


    public Sprite[] positiveDrawings;
    public Sprite[] negativeDrawings;
    public Sprite nullDrawing;
    public Sprite[] pageSprites;
    public GameObject drawing;
    public GameObject page;
    public GameObject speechBubble1;
    public GameObject speechBubble2;
    public GameObject moversThought;
    public GameObject journalText;
    public GameObject assObj;
    private AudioSource turnPageSound;
    public static bool finishReadingJournal;
    private int currentIndex;
    private bool[] haveYouReadMeYet;

    public static int lastAvatar = 0;               //var used to determine which avatar to rule out next scene, 1 rabbit, 2 ballkid, 3 music

    private float time;// = 0f;
    private float timer;// = 0f;

    private readonly string[] positiveThoughts =
    {
        "this little guy!",
        "He is so cool~",
        "Her music is awesome!"

    };

    private readonly string[] negativeThoughts =
    {
        "Evil rabbit...",
        "He is so mean to me...",
        "Her music is ugly..."

    };

    private readonly string nothingHappenThought = "Nothing happened";


    private void Awake()
    {
        finishReadingJournal = false;
        currentIndex = 0;
    }

    void Start()
    {
        turnPageSound = assObj.GetComponent<AudioSource>();
        timer = 6.5f;

        speechBubble1.SetActive(false);
        speechBubble2.SetActive(false);
        //moversThought.transform.position = mc.transform.position + new Vector3(3, 2, 0);
        //moversThought.SetActive();
        page.SetActive(false);
        journalText.SetActive(false);
        haveYouReadMeYet = new bool[]{ false, false, false };
        //this is for the next scene
        NPCs.schoolBell = false;
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.fixedUnscaledTime;
        //moversThought.transform.position = mc.transform.position + new Vector3(3, 2, 0);
        /*
        if (time >= timer)
        {
            //Debug.Log(time);
            moversThought.SetActive(false);
        }
        */
        if (BedtimeProcedure.journalIsOpened)
        {
            //turnPageSound.Play();
            speechBubble1.SetActive(true);
            speechBubble2.SetActive(true);
            drawing.SetActive(true);
            page.SetActive(true);
            journalText.SetActive(true);
            ChangeObjectSprite(page, pageSprites[currentIndex]);
            haveYouReadMeYet[currentIndex] = true;
            whichDrawingsToShow(currentIndex);
            if (Input.GetKeyDown(KeyCode.A))
            {           
                currentIndex -= 1;
                turnPageSound.Play();
                if (currentIndex == -1)
                {
                    speechBubble1.SetActive(false);
                    speechBubble2.SetActive(false);
                    currentIndex = 0;
                    CloseJournalAndReset();
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                currentIndex += 1;
                turnPageSound.Play();
                if (currentIndex == 3)
                {
                    speechBubble1.SetActive(false);
                    speechBubble2.SetActive(false);
                    currentIndex = 2;
                    CloseJournalAndReset();
                }
            }

        }
        else
        {
            speechBubble1.SetActive(false);
            speechBubble2.SetActive(false);
            CloseJournalAndReset();
        }
        
        if (!finishReadingJournal)
        {
            finishReadingJournal = CheckReadingStatus();
        }
    }

    void whichDrawingsToShow(int index)
    {
        int affinity;
        MentalState.DecideFriend();
        if (MentalState.relationships != null)
        {
            affinity = MentalState.relationships[index + 1];
        }
        else
        {
            affinity = -2;
        }

        if (MentalState.interactions[index+1] == 0)
        {
            ChangeObjectSprite(drawing, nullDrawing);
            journalText.GetComponent<TextMeshProUGUI>().text = nothingHappenThought;
            return;

        }

        if (affinity >= 0 )
        {
            ChangeObjectSprite(drawing, positiveDrawings[index]);
            journalText.GetComponent<TextMeshProUGUI>().text = positiveThoughts[index];
        }
        else if (affinity < 0)
        {
            ChangeObjectSprite(drawing, negativeDrawings[index]);
            journalText.GetComponent<TextMeshProUGUI>().text = negativeThoughts[index];
        }
        else
        {
            Debug.LogError("IN FlipJournal.cs, function is broken!");
        }

    }


    void ChangeObjectSprite(GameObject go, Sprite s)
    {
        go.GetComponent<Image>().sprite = s;
    }



    void CloseJournalAndReset()
    {
        page.SetActive(false);
        drawing.SetActive(false);
        journalText.SetActive(false);
        currentIndex = 0;
        BedtimeProcedure.journalIsOpened = false;
        
    }

    bool CheckReadingStatus()
    {
        var tempBool = true;
        foreach(var b in haveYouReadMeYet)
        {
            tempBool &= b;
        }
        return tempBool;
    }





   
}
