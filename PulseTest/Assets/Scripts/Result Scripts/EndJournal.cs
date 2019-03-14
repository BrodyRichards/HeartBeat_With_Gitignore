using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndJournal : MonoBehaviour
{
    public GameObject journalIcon;
    public GameObject sleepIcon;
    public static bool journalIsOpened = false;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = journalIcon.GetComponent<Animator>();
        journalIcon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (LightController.isNightGlowFinish && !journalIcon.activeSelf)
        {
            journalIcon.SetActive(true);
            anim.SetBool("newAccom", true);
        }

        if (journalIcon.activeSelf)
        {
            if (Input.GetKeyDown(Control.pullJournal) && !journalIsOpened)
            {
                
                journalIsOpened = true;
                
                
            }
            else if (Input.GetKeyDown(Control.pullJournal) && journalIsOpened)
            {
                journalIsOpened = false;
            }
        }

        if (FlipJournal.finishReadingJournal)
        {
            sleepIcon.SetActive(true);
            anim.SetBool("newAccom", false);
        }
    }
}
