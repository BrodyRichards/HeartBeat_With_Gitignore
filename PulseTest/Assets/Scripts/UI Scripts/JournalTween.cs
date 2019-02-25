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

    public Sprite musicLv2;
    public Sprite musicLv3;
    public Sprite ballLv2;
    public Sprite ballLv3;
    public Sprite rabbitLv2;
    public Sprite rabbitLv3;

    private int rabbitEvents;
    private int ballEvents;
    private int musicEvents;

    private int lv2threshold = 3;
    private int lv3threshold = 10;

    private bool calledOnce;

    // Start is called before the first frame update
    void Start()
    {
        calledOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!calledOnce)
        {
            EventTracking();
            calledOnce = false;
        }
        
       
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

    private void ChangeImg(int val, Image from, Sprite toLv2, Sprite toLv3)
    {
        if (val > lv3threshold)
        {
            from.sprite = toLv3;
        }else if (val > lv2threshold)
        {
            from.sprite = toLv2;
        }
    }
}
