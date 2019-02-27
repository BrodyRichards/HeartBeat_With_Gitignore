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
    private float alphaLv1;
    private float alphaLv2;
    private float alphaLv3;

    private int lv2threshold = 3;
    private int lv3threshold = 10;

    private bool achievedLv1Ball = false;
    private bool achievedLv2Ball = false;
    private bool achievedLv3Ball = false;

    private bool achievedLv1Rabbit = false;
    private bool achievedLv2Rabbit = false;
    private bool achievedLv3Rabbit = false;

    private bool achievedLv1Music = false;
    private bool achievedLv2Music = false;
    private bool achievedLv3Music = false;

    private bool calledOnce;

    // Start is called before the first frame update
    void Start()
    {
        
        Image[] images = new Image[] { rabbitStamp, ballStamp, musicStamp, musicLv2, musicLv3, ballLv2, ballLv3, rabbitLv2, rabbitLv3 };
        foreach (var image in images)
        {
            image.enabled = false;
        }


        alphaLv1 = 0f;
        alphaLv2 = 0f;
        alphaLv3 = 0f;

        

        
    }

    // Update is called once per frame
    void Update()
    {
       
            
        EventTracking();
        

        
       
    }


    private void EventTracking()
    {
        ballEvents = MentalState.moodLog["Played catch"] + MentalState.moodLog["Hit by ball"];
        if (ballEvents > 0) { achievedLv1Ball = true; };
        if (ballEvents > lv2threshold) { achievedLv2Ball = true; };
        if (ballEvents > lv3threshold) { achievedLv3Ball = true; };
        ChangeImg(ballEvents, ballStamp, ballLv2, ballLv3, achievedLv1Ball, achievedLv2Ball, achievedLv3Ball);

        rabbitEvents = MentalState.moodLog["Held Rabbit"] + MentalState.moodLog["Bit by rabbit"];

        if (rabbitEvents > 0) { achievedLv1Rabbit = true; };
        if (rabbitEvents > lv2threshold) { achievedLv2Rabbit = true; };
        if (rabbitEvents > lv3threshold) { achievedLv3Rabbit = true; };
        ChangeImg(rabbitEvents, rabbitStamp, rabbitLv2, rabbitLv3, achievedLv1Rabbit, achievedLv2Rabbit, achievedLv3Rabbit);

        musicEvents = MentalState.moodLog["Happy Song"] + MentalState.moodLog["Sad Song"];
        if (musicEvents > 0) { achievedLv1Music = true; };
        if (musicEvents > lv2threshold) { achievedLv2Music = true; };
        if (musicEvents > lv3threshold) { achievedLv3Music = true; };
        ChangeImg(musicEvents, musicStamp, musicLv2, musicLv3, achievedLv1Music, achievedLv2Music, achievedLv3Music);
    }

    private void ChangeImg(int val, Image from, Image toLv2, Image toLv3, bool one, bool two, bool three)
    {
        if (val > lv3threshold)
        {
            from.enabled = true;
            toLv3.enabled = true;
            FadeAlpha(toLv3, 0.1f);

            TweenAlpha(toLv3, 3);
        }
        if (val > lv2threshold) // val in 0 - 3 
        {
            from.enabled = true;
            toLv2.enabled = true;
            FadeAlpha(toLv2, 0.1f);

            TweenAlpha(toLv2, 2);
        }

        if (val > 0)
        {
            from.enabled = true;
            FadeAlpha(from, 0.1f);
            TweenAlpha(from, 1);
        }
    }

    private void TweenAlpha(Image to, int level)
    {
       

        Color temp2 = to.color;
        
        if (level == 1)
        {
            alphaLv1 += 0.008f;
            //Debug.Log("bound" + bound);
            //Debug.Log(alpha);
            //Debug.Log("temp2.a" + temp2.a);
            temp2.a += alphaLv1;
            
        }else if (level == 2)
        {
            alphaLv2 += 0.006f;
            temp2.a += alphaLv2;
            
        }else if (level == 3)
        {
            alphaLv3 += 0.005f;
            temp2.a += alphaLv3;
            
        }

        to.color = temp2;
        
        
       
    }

    private void FadeAlpha(Image from, float alphaVal)
    {
        Color col = from.color;
        col.a = alphaVal;
        from.color = col;
    }
}
