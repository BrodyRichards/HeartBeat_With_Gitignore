using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalResultPlaythrough : MonoBehaviour
{
    public GameObject leftPage;
    private Animator leftAnim;
    public GameObject rightPage;
    private Animator rightAnim;
    public GameObject hand;
    private Animator handClose;

    
    //public Image bubble;

    private float time;
    //private float timer;
    
    private float endTimer = 13f;
    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        //timer = 0f;

        leftAnim = leftPage.GetComponent<Animator>();
        rightAnim = rightPage.GetComponent<Animator>();
        handClose = hand.GetComponent<Animator>();

        Invoke("playAnim", 5f);
        //Invoke("dialogue", 5f);
    }

    private void playAnim()
    {
        leftAnim.Play("JournalLeftPage");
        rightAnim.Play("JournalRightPage");
        handClose.Play("newCloseJournal");
    }
    
    /*
    private void dialogue()
    {
        timer = time + 3.5f;
        bubble.GetComponent<Image>().gameObject.SetActive(true);
    }
    */
    private void Update()
    {
        time = Time.timeSinceLevelLoad;
        /*
        if (time >= timer)
        {
            bubble.GetComponent<Image>().gameObject.SetActive(false);
        }
        */
        if (time >= endTimer)
        {
            SceneManager.LoadScene("CreditScreen");
        }
    }
}
