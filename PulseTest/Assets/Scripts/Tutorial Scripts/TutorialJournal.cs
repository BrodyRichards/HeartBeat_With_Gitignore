using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialJournal : MonoBehaviour
{
    public GameObject text;
    public GameObject journal;
    public static bool journalOpen = false;
    public static bool journalOpenOnce = false;

    public Animator journalAnimator;

    private TextMeshProUGUI tmpug;
    // Start is called before the first frame update
    void Start()
    {
        tmpug = text.GetComponent<TextMeshProUGUI>();
        journal.SetActive(journalOpen);
    }

    // Update is called once per frame
    void Update()
    {
        journalAnimator.SetBool("newAccom", !journalOpenOnce);
        if (McExit.nextScene == false)
        {
            if (ThoughtsTutorial.alarm || ThoughtsTutorial.curtain)
            {
                setText();
                Debug.Log("Hello");
                if (Input.GetKeyDown(Control.pullJournal))
                {
                    if (journalOpen == false)
                    {
                        journalOpenOnce = true;
                        journalOpen = true;
                    }
                    else if (journalOpen)
                    {
                        journalOpen = false;
                    }
                    journal.SetActive(journalOpen);
                }
                
            }
        }
        else //if (McExit.nextScene)
        {
            Debug.Log("nextScene: " + McExit.nextScene);
            journal.SetActive(false);
        }
        
        
        
    }

    private void setText()
    {
        if (ThoughtsTutorial.alarm)
        {
            tmpug.text = "I hate how loud and yucky my alarm sounds. ";
        }
        else if (ThoughtsTutorial.curtain)
        {
            tmpug.text = "The sun feels so nice and warm! ";
        }
    }
}
