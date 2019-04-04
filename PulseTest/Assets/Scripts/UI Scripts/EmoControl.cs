using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoControl : MonoBehaviour
{
    //public GameObject musicKid;
    //public static bool CRunning = false;

    public Sprite happy;
    public Sprite sad;
    public Sprite startle;
    public Sprite angry;

    private SpriteRenderer sr;
    private float emoSize;
    private float sFa; // scaling factor
    private float iFa; // incrementing size value per update for emo sprite

    public static bool mcBallHit;
    public static bool rabbitHug;
    public static bool bitten;
    public static bool justPlayedCatch;
    public static bool hasEmo;
    public static bool emoChanged = false;
    public static bool isAffectedByMusic;
    public static int currentEffect;

    void Awake()
    {
        mcBallHit = false;
        rabbitHug = false;
        bitten = false;
        justPlayedCatch = false;
        hasEmo = false;
        isAffectedByMusic = false;
        sFa = 0.5f;
        iFa = 0.5f;
        emoSize = 1f;
        currentEffect = 0; // None
    }
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (!EmoResetSize())
        {
            EmoGrowInSize();
        }
        
        if (mcBallHit || bitten)
        {
            sr.sprite = angry;
            Invoke("DestroyEmotion", 1f);
            currentEffect = 3;

        }
        else if (rabbitHug)
        {
            sr.sprite = happy;
            currentEffect = 1;
        }
        else if (justPlayedCatch)
        {
            sr.sprite = happy;
            Invoke("DestroyEmotion", 1f);
            currentEffect = 1;
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
   
    private void DestroyEmotion()
    {
        bitten = false;
        mcBallHit = false;
        justPlayedCatch = false;
        //sr.sprite = null;
    }

    private void ReactToMusic()
    {
        if (RadioControl.currentMood == 0)
        {
            hasEmo = true;
            sr.sprite = happy;
            currentEffect = 1;
        }
        else if (RadioControl.currentMood == 1)
        {
            hasEmo = true;
            sr.sprite = sad;
            currentEffect = 2;
        }
        
    }

    private void EmoGrowInSize()
    {
       
        if (emoSize < MentalState.currentActionCombo)
        {
            emoSize += iFa * 2f * Mathf.Sin(Time.deltaTime);
            transform.localScale = new Vector2(sFa * Mathf.Sqrt(emoSize) , sFa * Mathf.Sqrt(emoSize));
            if (emoSize > 3.99f)
            {
                ExplodeEmo.emitParticleNow = true;
                transform.localScale = new Vector2(0f, 0f);
                
            }
        }

        if (MentalState.currentActionCombo == 1)
        {
            emoSize = 1f;
            transform.localScale = new Vector2(sFa * emoSize, sFa * emoSize);
        }

    }

    private bool EmoResetSize()
    {
        // if resetting, show no emoticon 
        if (MentalState.currentActionCombo == 0)
        {

            transform.localScale = new Vector2(0f, 0f);
            
            return true;
        }
        else
        {
            return false;
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
