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
    public GameObject theBell;
    public AudioSource ass;

    public static Accomplish rabbit;
    public static Accomplish ball;
    public static Accomplish music;
    public static List<Accomplish> accomplishments;

    private readonly float tweenSpeed = 0.02f;
    private readonly float completedAlpha = 0.99f;

    private readonly int[] rabbitThreshold = new int[] { 0, Playground.trLv2, Playground.trLv3 };
    private readonly int[] ballThreshold = new int[] { 0, Playground.tbLv2, Playground.tbLv3 };
    private readonly int[] musicThreshold = new int[] { 0, Playground.tmLv2, Playground.tmLv3 };

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
        rabbit = Accomplish.CreateInstance(rabbitThreshold, rabbitStamp, rabbitLv2, rabbitLv3);
        ball = Accomplish.CreateInstance(ballThreshold, ballStamp, ballLv2, ballLv3);
        music = Accomplish.CreateInstance(musicThreshold, musicStamp, musicLv2, musicLv3);

        accomplishments = new List<Accomplish> { rabbit, ball, music };
        gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        IconControl.journalTweening = false;
        // update the occurences of events
        

        // check if the number has reached threshold
        CheckTheAlpha();

        if (FinishedAllStatus())
        {
            theBell.SetActive(true);
            MentalState.journalInProgress = false;
            rabbit = null;
            ball = null;
            music = null;
            accomplishments = null;
            Destroy(this);
            
            
        }
        
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

    private bool FinishedAllStatus()
    {
        var tempCount = 0;
        foreach (var com in accomplishments)
        {

            if (com.finished[0] && com.finished[1] && com.finished[2])
            {
                tempCount += 1;
            }
        }

        if (tempCount == accomplishments.Count)
        {
            Debug.Log("journal is completed");
            return true;
        }

        return false;
    }

    //Backup function 
    //private void FadeAlpha(Image from, float alphaVal)
    //{
    //    Color col = from.color;
    //    col.a = alphaVal;
    //    from.color = col;
    //}
}
