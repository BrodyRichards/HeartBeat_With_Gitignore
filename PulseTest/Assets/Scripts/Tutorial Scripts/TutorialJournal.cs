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
        if (ThoughtsTutorial.alarm || ThoughtsTutorial.curtain)
        {
            setText();
            if (Input.GetKeyDown(Control.pullJournal))
            {
                if (journalOpen == false)
                {
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
