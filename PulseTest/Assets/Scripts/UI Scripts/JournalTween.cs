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

    private Accomplish rabbit;
    private Accomplish ball;
    private Accomplish music;
    private List<Accomplish> accomplishments;

    private readonly float tweenSpeed = 0.008f;
    private readonly float completedAlpha = 0.99f;

    private readonly int[] rabbitThreshold = new int[] { 0, 1, 2 };
    private readonly int[] ballThreshold = new int[] { 0, 1, 2 };
    private readonly int[] musicThreshold = new int[] { 0, 1, 2 };

    // Start is called before the first frame update
    void Start()
    {
        // disabled all the images so they don't show up at first 
        Image[] images = new Image[] { rabbitStamp, ballStamp, musicStamp, musicLv2, musicLv3, ballLv2, ballLv3, rabbitLv2, rabbitLv3 };
        foreach (var image in images)
        {
            image.enabled = false;
        }

        // instantiate class objects for the three avatars stamps 
        rabbit = new Accomplish(rabbitThreshold, rabbitStamp, rabbitLv2, rabbitLv3);
        ball = new Accomplish(ballThreshold, ballStamp, ballLv2, ballLv3);
        music = new Accomplish(musicThreshold, musicStamp, musicLv2, musicLv3);

        accomplishments = new List<Accomplish> { rabbit, ball, music };

    }

    // Update is called once per frame
    void Update()
    {
       
        // update the occurences of events
        EventTracking();

        // check if the number has reached threshold
        CheckTheAlpha();

        
    }


    private void EventTracking()
    {
        
        ball.Num = MentalState.moodLog["Played catch"] + MentalState.moodLog["Hit by ball"];
        rabbit.Num = MentalState.moodLog["Held Rabbit"] + MentalState.moodLog["Bit by rabbit"];
        music.Num = MentalState.moodLog["Happy Song"] + MentalState.moodLog["Sad Song"] + MentalState.moodLog["Startled Song"];
    }



    private void DoTheAlphaShit(Accomplish accom, int index)
    {
        // gradually increase alpha of the corresponding stamp to max 
        accom.images[index].enabled = true;
        accom.alpha[index] += tweenSpeed;
        Color col = accom.images[index].color;
        col.a = accom.alpha[index];
        accom.images[index].color = col;

        if (accom.alpha[index] > completedAlpha) { accom.finished[index] = true; }
    }

    private void CheckTheAlpha()
    {
        // check for all the accomplishments, if their occurence has reach the threshold
        // and if the alpha is not max yet 
        foreach (var com in accomplishments)
        {

            if (com.Num > com.threshold[0] && !com.finished[0])
            {
                DoTheAlphaShit(com, 0);
            }
            else if (com.Num > com.threshold[1] && !com.finished[1])
            {
                DoTheAlphaShit(com, 1);
            }
            else if (com.Num > com.threshold[2] && !com.finished[2])
            {
                DoTheAlphaShit(com, 2);
            }

        }
    }

    //Backup function 
    //private void FadeAlpha(Image from, float alphaVal)
    //{
    //    Color col = from.color;
    //    col.a = alphaVal;
    //    from.color = col;
    //}
}
