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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EventTracking();
        if (ballEvents > 5)
        {
            ballStamp.sprite = ballLv3;
        }
        else if (ballEvents > 2)
        {
            ballStamp.sprite = ballLv2;
        }

        if (rabbitEvents > 5)
        {
            rabbitStamp.sprite = rabbitLv3;
        }
        else if (rabbitEvents > 2)
        {
            ballStamp.sprite = rabbitLv2;
        }

        if (musicEvents > 5)
        {
            musicStamp.sprite = musicLv3;
        }
        else if (rabbitEvents > 2)
        {
            ballStamp.sprite = musicLv2;
        }
    }


    private void EventTracking()
    {
        ballEvents = MentalState.moodLog["Played catch"] + MentalState.moodLog["Hit by ball"];

        rabbitEvents = MentalState.moodLog["Held Rabbit"] + MentalState.moodLog["Bit by rabbit"];

        musicEvents = MentalState.moodLog["Happy Song"] + MentalState.moodLog["Sad Song"];
    }
}
