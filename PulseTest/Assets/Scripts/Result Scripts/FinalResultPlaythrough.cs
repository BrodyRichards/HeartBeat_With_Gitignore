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

    public Image bubble;

    private float time;
    private float timer;

    private float endTimer = 20f;
    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        timer = 0f;

        leftAnim = leftPage.GetComponent<Animator>();
        rightAnim = rightPage.GetComponent<Animator>();

        Invoke("playAnim", 10f);
        Invoke("dialogue", 5f);
    }

    private void playAnim()
    {
        leftAnim.Play("JournalLeftPage");
        rightAnim.Play("JournalRightPage");
    }
    
    private void dialogue()
    {
        timer = time + 3.5f;
        bubble.GetComponent<Image>().gameObject.SetActive(true);
    }

    private void Update()
    {
        time = Time.timeSinceLevelLoad;
        if (time >= timer)
        {
            bubble.GetComponent<Image>().gameObject.SetActive(false);
        }

        if (time >= endTimer)
        {
            SceneManager.LoadScene("CreditScreen");
        }
    }
}
