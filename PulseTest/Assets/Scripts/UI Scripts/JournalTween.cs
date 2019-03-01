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

    private float alphaLv1Ball = 0f;
    private float alphaLv2Ball = 0f;
    private float alphaLv3Ball = 0f;

    private float alphaLv1Music = 0f;
    private float alphaLv2Music = 0f;
    private float alphaLv3Music = 0f;

    private float alphaLv1Rabbit = 0f;
    private float alphaLv2Rabbit = 0f;
    private float alphaLv3Rabbit = 0f;

    private bool once = false;
    private bool once2 = false;
    private bool once3 = false;

    private int achievedLv1Ball = 0;
    private int achievedLv2Ball = 2;
    private int achievedLv3Ball = 4;

    private int achievedLv1Rabbit = 0;
    private int achievedLv2Rabbit = 1;
    private int achievedLv3Rabbit = 3;

    private int achievedLv1Music = 0;
    private int achievedLv2Music = 1;
    private int achievedLv3Music = 2;

    private bool calledOnce;

    // Start is called before the first frame update
    void Start()
    {
        
        Image[] images = new Image[] { rabbitStamp, ballStamp, musicStamp, musicLv2, musicLv3, ballLv2, ballLv3, rabbitLv2, rabbitLv3 };
        foreach (var image in images)
        {
            image.enabled = false;
        }


        

        

        
    }

    // Update is called once per frame
    void Update()
    {
       
            
        EventTracking();

        // for event in events:
        //    if ( val > eventLv1 && !eventLv1Completed){
        //        tweenAlpha1;
        //        if event.alpha1 == 1.0 eventLv1Completed = true;
        //    }
        //    if ( val > eventLv2 && !eventLv2Completed){
        //        tweenAlpha2;
        //        if event.alpha2 == 1.0 eventLv2Completed = true;
        //    }
        //    if ( val > eventLv3 && !eventLv3Completed){
        //        tweenAlpha3;
        //        if event.alpha3 == 1.0 eventLv3Completed = true;
        //    }
        // 
        

        
       
    }


    private void EventTracking()
    {
        ballEvents = MentalState.moodLog["Played catch"] + MentalState.moodLog["Hit by ball"];
        
        ChangeImg(ballEvents, ballStamp, ballLv2, ballLv3, achievedLv1Ball, achievedLv2Ball, achievedLv3Ball,
            alphaLv1Ball, alphaLv2Ball, alphaLv3Ball);

        rabbitEvents = MentalState.moodLog["Held Rabbit"] + MentalState.moodLog["Bit by rabbit"];

        
        ChangeImg(rabbitEvents, rabbitStamp, rabbitLv2, rabbitLv3, achievedLv1Rabbit, achievedLv2Rabbit, achievedLv3Rabbit,
            alphaLv1Rabbit, alphaLv2Rabbit, alphaLv3Rabbit);

        musicEvents = MentalState.moodLog["Happy Song"] + MentalState.moodLog["Sad Song"];
      
        ChangeImg(musicEvents, musicStamp, musicLv2, musicLv3, achievedLv1Music, achievedLv2Music, achievedLv3Music,
            alphaLv1Music, alphaLv2Music, alphaLv3Music);
    }

    private void ChangeImg(int val, Image from, Image toLv2, Image toLv3, int one, int two, int three,
        float alpha1, float alpha2, float alpha3)
    {
        
        if (val > three)
        {
            from.enabled = true;
            toLv3.enabled = true;
            if (!once3)
            {
                FadeAlpha(toLv3, 0.1f);
                once3 = true;
            }
            TweenAlpha(toLv3, alpha3);
        }
        if (val > two) // val in 0 - 3 
        {
            from.enabled = true;
            toLv2.enabled = true;
            if (!once2)
            {
                FadeAlpha(toLv2, 0.1f);
                once2 = true;
            }
            

            TweenAlpha(toLv2, alpha2);
        }

        if (val > one)
        {
            from.enabled = true;
            if (!once)
            {
                FadeAlpha(from, 0.1f);
                once = true;
            }
                
            TweenAlpha(from, alpha1);
        }
    }

    private void TweenAlpha(Image to, float a)
    {
       

        Color temp2 = to.color;
        
        //if (level == 1)
        //{
            a += 0.008f;
            //Debug.Log("bound" + bound);
            //Debug.Log(alpha);
            //Debug.Log("temp2.a" + temp2.a);
            temp2.a += a;
            
        //}else if (level == 2)
        //{
        //    alphaLv2 += 0.006f;
        //    temp2.a += alphaLv2;
            
        //}else if (level == 3)
        //{
        //    alphaLv3 += 0.005f;
        //    temp2.a += alphaLv3;
            
        //}

        to.color = temp2;
        
        
       
    }

    private void FadeAlpha(Image from, float alphaVal)
    {
        Color col = from.color;
        col.a = alphaVal;
        from.color = col;
    }
}
