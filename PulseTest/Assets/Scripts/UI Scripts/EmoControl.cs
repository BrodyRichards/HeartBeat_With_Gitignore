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

    public ParticleSystem ps;
    private ParticleSystem.EmissionModule em;

    private SpriteRenderer sr;
    private float emoSize = 1f;
    private float sFa = 0.5f; // scaling factor
    private float iFa = 0.5f; // incrementing size value per update for emo sprite

    public static bool mcBallHit = false;
    public static bool rabbitHug = false;
    public static bool bitten = false;
    public static bool justPlayedCatch = false;

    public static bool hasEmo = false;
    public static bool emoChanged = false;
    public static bool isAffectedByMusic = false;
    public static bool emitParticleNow = false;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = null;
        
        em = ps.emission;
        em.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!EmoResetSize())
        {
            EmoGrowInSize();
        }

        if (emitParticleNow)
        {
            
            em.enabled = true;
            Invoke("PauseEmoPs", 0.5f);
        }
        
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
        //sr.sprite = null;
    }

    public void ReactToMusic()
    {
        if (RadioControl.currentMood == 0)
        {
            hasEmo = true;
            sr.sprite = happy;
        }
        else if (RadioControl.currentMood == 1)
        {
            hasEmo = true;
            sr.sprite = sad;
        }
        
    }

    public void EmoGrowInSize()
    {
       
        if (emoSize < MentalState.currentActionCombo)
        {
            emoSize += iFa * 2f * Mathf.Sin(Time.deltaTime);
            transform.localScale = new Vector2(sFa * Mathf.Sqrt(emoSize) , sFa * Mathf.Sqrt(emoSize));
            if (emoSize > 3.99f)
            {
                emitParticleNow = true;
                transform.localScale = new Vector2(0f, 0f);
                
            }
        }

        if (MentalState.currentActionCombo == 1)
        {
            emoSize = 1f;
            transform.localScale = new Vector2(sFa * emoSize, sFa * emoSize);
        }

    }

    public bool EmoResetSize()
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

    private void PauseEmoPs()
    {
        
        em.enabled = false;
        emitParticleNow = false;
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
