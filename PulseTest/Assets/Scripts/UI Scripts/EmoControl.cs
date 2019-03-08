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

        //emoDist = Playground.CheckDist(NpcInstantiator.musicKidPos, transform.position, Playground.MusicAoe);

        

        if (mcBallHit || bitten)
        {
            hasEmo = true;
            sr.enabled = true;
            sr.sprite = angry;
            Invoke("DestroyEmotion", 1f);

        }
        else if (rabbitHug)
        {
            
            hasEmo = true;
            sr.enabled = true;
            sr.sprite = happy;
        }
        else if (justPlayedCatch)
        {
            hasEmo = true;
            sr.enabled = true;
            sr.sprite = happy;
            Invoke("DestroyEmotion", 1f);
        }
        else if (RadioControl.mcIsAffected)
        {
            ReactToMusic();
        }
        else
        {
            sr.enabled = false;
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
        switch (RadioControl.currentMood)
        {
            case 0:
                
                sr.enabled = true;
                hasEmo = true;
                sr.sprite = happy;
                gameObject.transform.localScale = new Vector2(0.5f, 0.5f);
                CRunning = true;
                break;
            case 1:
                sr.enabled = true;
                hasEmo = true;
                sr.sprite = sad;
                gameObject.transform.localScale = new Vector2(2.1f, 2.1f);
                CRunning = true;
                break;
            default:
                sr.enabled = false;
                hasEmo = false;
                sr.sprite = null;
                
                break;
        }
    }

    IEnumerator IncrementMoodLog(string msg, int mood)
    {
        Debug.Log("emoDist" + emoDist);
        while(RadioControl.currentMood == mood && emoDist)
        {
            yield return new WaitForSeconds(3f);
            MentalState.sendMsg(msg);
        }
        yield break;
    }

    
}
