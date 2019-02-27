using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JournalTween : MonoBehaviour
{
    [SerializeField]
    private Image rabbitStamp;
    [SerializeField]
    private Image ballStamp;
    [SerializeField]
    private Image musicStamp;

    public Image musicLv2;
    public Image musicLv3;
    public Image ballLv2;
    public Image ballLv3;
    public Image rabbitLv2;
    public Image rabbitLv3;

    private int rabbitEvents;
    private int ballEvents;
    private int musicEvents;
    private float alpha;

    private int lv2threshold = 3;
    private int lv3threshold = 10;

    private bool calledOnce;

    // Start is called before the first frame update
    void Start()
    {
        calledOnce = false;
        Image[] images = new Image[] { musicLv2, musicLv3, ballLv2, ballLv3, rabbitLv2, rabbitLv3 };
        foreach (var image in images)
        {
            image.enabled = false;
        }

        alpha = 0f;

        
    }

    // Update is called once per frame
    void Update()
    {
       
            
        EventTracking();
        calledOnce = false;

        
       
    }


    private void EventTracking()
    {
        ballEvents = MentalState.moodLog["Played catch"] + MentalState.moodLog["Hit by ball"];
        ChangeImg(ballEvents, ballStamp, ballLv2, ballLv3);

        rabbitEvents = MentalState.moodLog["Held Rabbit"] + MentalState.moodLog["Bit by rabbit"];
        ChangeImg(rabbitEvents, rabbitStamp, rabbitLv2, rabbitLv3);

        musicEvents = MentalState.moodLog["Happy Song"] + MentalState.moodLog["Sad Song"];
        ChangeImg(musicEvents, musicStamp, musicLv2, musicLv3);
    }

    private void ChangeImg(int val, Image from, Image toLv2, Image toLv3)
    {
        if (val > lv2threshold)
        {
            toLv3.enabled = true;
            FadeAlpha(toLv3, 0.1f);

            TweenAlpha(toLv3, Mathf.Clamp(0.15f * (val - 2) , 0.0f, 1.0f));
        }
        else // val in 0 - 3 
        {
            toLv2.enabled = true;
            FadeAlpha(toLv2, 0.1f);

            TweenAlpha(toLv2, Mathf.Clamp(0.25f * val, 0.0f, 1.0f));
        }
    }

    private void TweenAlpha(Image to, float bound)
    {
       
        //Color temp = from.color;
        //temp.a -= alpha ;
        //from.color = temp;

        Color temp2 = to.color;
        if (temp2.a < bound)
        {
            alpha += 0.001f;
            Debug.Log("bound" + bound);
            Debug.Log(alpha);
            Debug.Log("temp2.a" + temp2.a);
            temp2.a += alpha;
            to.color = temp2;
        }
        
        
       
    }

    private void FadeAlpha(Image from, float alphaVal)
    {
        Color col = from.color;
        col.a = alphaVal;
        from.color = col;
    }
}
