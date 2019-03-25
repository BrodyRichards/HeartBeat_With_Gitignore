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

    public static bool mcBallHit = false;
    public static bool rabbitHug = false;
    public static bool bitten = false;
    public static bool justPlayedCatch = false;

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
        if (mcBallHit || bitten)
        {
            hasEmo = true;
            sr.sprite = angry;
            Invoke("DestroyEmotion", 1f);

        }
        else if (rabbitHug)
        {
            
            hasEmo = true;
            sr.sprite = happy;
        }
        else if (justPlayedCatch)
        {
            hasEmo = true;
            sr.sprite = happy;
            Invoke("DestroyEmotion", 1f);
        }
        else if (RadioControl.musicListener=="MC")
        {
            ReactToMusic();
        }
        else
        {
            sr.sprite = null;
        }
    }
   
    public void DestroyEmotion()
    {
        bitten = false;
        mcBallHit = false;
        justPlayedCatch = false;
    }

    public void ReactToMusic()
    {
        if (RadioControl.currentMood == 0)
        {
            hasEmo = true;
            sr.sprite = happy;
        }else if (RadioControl.currentMood == 1)
        {
            hasEmo = true;
            sr.sprite = sad;
        }
        
    }
    //[Obsolete]
    //IEnumerator IncrementMoodLog(string msg, int mood)
    //{
    //    Debug.Log("emoDist" + emoDist);
    //    while(RadioControl.currentMood == mood && emoDist)
    //    {
    //        yield return new WaitForSeconds(3f);
    //        MentalState.sendMsg(msg);
    //    }
    //    yield break;
    //}

    
}
