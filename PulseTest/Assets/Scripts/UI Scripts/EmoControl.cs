using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoControl : MonoBehaviour
{
    //public GameObject musicKid;
    //public static bool CRunning = false;
    //string compare explain: https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity5.html
    public Sprite happy;
    public Sprite sad;
    public Sprite startle;
    public Sprite angry;

    private SpriteRenderer sr;
    private float emoSize;
    private float sFa; // scaling factor
    private float iFa; // incrementing size value per update for emo sprite

    public static bool hasEmo;
    public static bool emoChanged;
    public static int currentEffect;

    void Awake()
    {
        
        hasEmo = false;
        emoChanged = false;
        sFa = 0.5f;
        iFa = 3f;
        emoSize = 0f;
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
        if (emoChanged)
        {
            CheckWhichEmoToShow();
        }
        
        if (!EmoResetSize())
        {
            EmoGrowInSize();
        }

        
       
    }
    private void CheckWhichEmoToShow()
    {
        Debug.Log("currentActionCombo" + MentalState.currentActionCombo);
        if (MentalState.currentActionCombo == 0)
        {
            sr.sprite = null;
        }

        if (MentalState.currentActionCombo > 0)
        {
            sr.sprite = happy;
            currentEffect = 1;
        }
        else if(MentalState.currentActionCombo < 0 && MentalState.currentActionCombo!=-999)
        {
            sr.sprite = sad;
            currentEffect = 2;
        }

        emoChanged = false;
        
    }

    private void EmoGrowInSize()
    {
        
        if (emoSize < Mathf.Abs(MentalState.currentActionCombo) - 0.1f)
        {
            
            
            emoSize += iFa * Mathf.Sin(Time.deltaTime);
            transform.localScale = new Vector2(sFa * Mathf.Sqrt(emoSize) , sFa * Mathf.Sqrt(emoSize));
            if (emoSize > 3.89f)
            {
                ExplodeEmo.emitParticleNow = true;
                transform.localScale = new Vector2(0f, 0f);
                MentalState.currentActionCombo = 0;
                emoSize = 0f;
                
            }
        }
        else if (emoSize > Mathf.Abs(MentalState.currentActionCombo) + 0.1f)
        {
            emoSize -= iFa * Mathf.Sin(Time.deltaTime);
            transform.localScale = new Vector2(sFa * Mathf.Sqrt(emoSize), sFa * Mathf.Sqrt(emoSize));
        }


        //if (Mathf.Abs(MentalState.currentActionCombo) == 1)
        //{
        //    emoSize = 1f;
        //    transform.localScale = new Vector2(sFa * emoSize, sFa * emoSize);
        //}



    }

    private bool EmoResetSize()
    {
        // if resetting, show no emoticon 
        if (MentalState.currentActionCombo == 0)
        {

            transform.localScale = new Vector2(0f, 0f);
            
            return true;
        }
        else if (MentalState.currentActionCombo == -999)
        {

            if (emoSize > 0.09f)
            {
                //hasEmo = true;
                emoSize -= iFa * Mathf.Sin(Time.deltaTime);
                transform.localScale = new Vector2(sFa * Mathf.Sqrt(emoSize), sFa * Mathf.Sqrt(emoSize));
                if (emoSize < 0.03f)
                {

                    transform.localScale = new Vector2(0,0);
                    MentalState.currentActionCombo *= 0;
                }
            }
            else
            {
                MentalState.currentActionCombo *= 0;
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    private void ResetHasEmo()
    {
        hasEmo = false;
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
