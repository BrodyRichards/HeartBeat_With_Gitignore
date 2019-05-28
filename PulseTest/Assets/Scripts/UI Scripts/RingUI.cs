using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RingUI : MonoBehaviour
{
    public GameObject[] emoSegments;

    public GameObject ring;
    public GameObject face;
    public GameObject buttonPrompt;
    public GameObject ringBg;
    public GameObject dayIndicator;

    public static bool isCompleted;

    public Sprite happySeg;
    public Sprite sadSeg;
    public Sprite angrySeg;

    public Sprite neutralFace;
    public Sprite happyFace;
    public Sprite sadFace;

    public Sprite bell;

    public Animator bell_anim;
    public static string fuckingString;

    private int emoCurrentLength;
    private readonly int segNum = 12;

    private GameObject pgs;

    // Start is called before the first frame update

    private void Awake()
    {
        emoCurrentLength = 0;
        isCompleted = false;
        fuckingString = "";

    }
    void Start()
    {
        foreach(var es in emoSegments)
        {
            es.SetActive(false);
        }

        bell_anim.enabled = false;
        pgs = GameObject.FindGameObjectWithTag("ProgSpirit");
    }

    // Update is called once per frame
    void Update()
    {
        if (emoCurrentLength < segNum && !isCompleted)
        {
            AddSegToRing();
            IsRingFinished();
        }
        SwitchFace();


        if (NPCs.schoolBell)
        {
            bell_anim.enabled = false;
            ring.SetActive(false);
            buttonPrompt.SetActive(false);  
        }

        dayIndicator.GetComponent<TextMeshProUGUI>().text = "Day " + (IterationController.dayCount + 1.0f);
    }

    void SwitchFace()
    {
        if (MentalState.WithinRange(MentalState.currentState, MentalState.normalBound.x, MentalState.normalBound.y))
        {
            face.GetComponent<Image>().sprite = neutralFace;
        }
        else if (MentalState.WithinRange(MentalState.currentState, MentalState.happyBound.x, MentalState.happyBound.y))
        {

            face.GetComponent<Image>().sprite = happyFace;
        }
        else if (MentalState.WithinRange(MentalState.currentState, MentalState.sadBound.x, MentalState.sadBound.y))
        {
            face.GetComponent<Image>().sprite = sadFace;
        }
    }

    void AddSegToRing()
    {
        if (MentalState.emoTimeline != null)
        {

            if (MentalState.emoTimeline.Count > emoCurrentLength)
            {

                emoCurrentLength = MentalState.emoTimeline.Count;
                

                var thisObj = emoSegments[emoCurrentLength - 1];
                if (MentalState.positiveAct.Contains(fuckingString))
                {

                    thisObj.SetActive(true);
                    thisObj.GetComponent<Image>().sprite = happySeg;
                    pgs.GetComponent<ProgSpiritScript>().Emit(1);
                }
                else if (fuckingString == "Bit by rabbit" || fuckingString == "Hit by ball")
                {
                    thisObj.SetActive(true);
                    thisObj.GetComponent<Image>().sprite = angrySeg;
                    pgs.GetComponent<ProgSpiritScript>().Emit(3);
                }
                else if (fuckingString == "Sad Song")
                {
                    thisObj.SetActive(true);
                    thisObj.GetComponent<Image>().sprite = sadSeg;
                    pgs.GetComponent<ProgSpiritScript>().Emit(2);
                }
                else
                {
                    Debug.Log("message" + MentalState.message);
                    thisObj.SetActive(true);
                    thisObj.GetComponent<Image>().sprite = sadSeg;
                }
                
            }
        }

    }

    void IsRingFinished()
    {
        if (emoCurrentLength == segNum)    
        {
            bell_anim.enabled = true;

        }
    }

    public void AnimCompleted()
    {
        isCompleted = true;
        ring.GetComponent<Image>().sprite = bell;
        foreach (var es in emoSegments)
        {
            es.SetActive(false);
        }
        ringBg.SetActive(false);
        face.SetActive(false);
        buttonPrompt.SetActive(true);
    }
}
