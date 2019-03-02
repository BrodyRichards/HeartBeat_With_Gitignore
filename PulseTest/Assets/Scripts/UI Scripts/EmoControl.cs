using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoControl : MonoBehaviour
{
    //public GameObject musicKid;
    public static bool CRunning = false;

    public Sprite happy;
    public Sprite sad;
    public Sprite startle;
    public Sprite angry;

    private SpriteRenderer sr;

    public static int mcBallHit = 0;
    public static bool rabbitHug = false;
    private bool rabitJustHug = false;
    private bool emoDist;

    public static bool hasEmo = false;
    public static bool emoChanged = false;
    public static bool isAffectedByMusic = false;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {

        emoDist = Playground.CheckDist(NpcInstantiator.musicKidPos, transform.position, Playground.MusicAoe);

        if (mcBallHit > 1)
        {
            hasEmo = true;
            sr.enabled = true;
            sr.sprite = angry;
            emoChanged = true;
            MentalState.mood = 4;
            Invoke("DestroyEmotion", 1f);
            Invoke("ChangeEmoBack", 1f);

        }
        else if (rabbitHug)
        {
            
            hasEmo = true;
            sr.enabled = true;
            sr.sprite = happy;
            MentalState.mood = 1;


        }
        else if (emoDist)
        {
            ReactToMusic();
        }
        else
        {
            sr.enabled = false;
            MentalState.mood = 0;
        }
    }
   
    public void DestroyEmotion()
    {
        //hasEmo = false;
        //sr.enabled = false;
        
        
        mcBallHit *= 0;
        
    }

    public void ChangeEmoBack()
    {
        emoChanged = false;
        
    }

    public void ReactToMusic()
    {
        switch (RadioControl.currentMood)
        {
            case 1:
                if (isAffectedByMusic)
                {
                    sr.enabled = true;
                    hasEmo = true;
                    sr.sprite = happy;
                    MentalState.mood = 1;
                    if (!CRunning)
                    {
                        CRunning = true;
                        StartCoroutine(IncrementMoodLog("Happy Song", 1));
                    }
                    
                }
                break;
            case 2:
                if (isAffectedByMusic)
                {
                    sr.enabled = true;
                    hasEmo = true;
                    sr.sprite = sad;
                    MentalState.mood = 2;
                    if (!CRunning)
                    {
                        CRunning = true;
                        StartCoroutine(IncrementMoodLog("Sad Song", 2));
                    }
                }
                
                break;
            case 3:
                sr.enabled = true;
                hasEmo = true;
                sr.sprite = startle;
                MentalState.mood = 3;
                if (!CRunning)
                {
                    CRunning = true;
                    StartCoroutine(IncrementMoodLog("Startled Song", 3));
                }
                break;
            default:
                sr.enabled = false;
                hasEmo = false;
                sr.sprite = null;
                MentalState.mood = 0;
                break;
        }
    }

    IEnumerator IncrementMoodLog(string msg, int mood)
    {
        while(RadioControl.currentMood == mood)
        {
            yield return new WaitForSeconds(3f);
            MentalState.sendMsg(msg);
        }
        yield break;
    }

    //bool checkDist(Vector3 pos1, Vector2 pos2)
    //{
    //    float dist = Vector3.Distance(pos1, pos2);
    //    Debug.Log(dist);
    //    if (dist <= 30.0f) { return true; }
    //    return false;
    //}
}
