using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoControl : MonoBehaviour
{
    public Sprite happy;
    public Sprite sad;
    public Sprite gross;
    public Sprite angry;

    private SpriteRenderer sr;

    public static int mcBallHit = 0;
    public static bool rabbitHug = false;

    public static bool hasEmo = false;
    public static bool emoChanged = false;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
       
        ReactToMusic();
        
        if (mcBallHit > 1)
        {
            hasEmo = true;
            sr.enabled = true;
            sr.sprite = angry;
            emoChanged = true;
            Invoke("DestroyEmotion", 1f);
            Invoke("ChangeEmoBack", 1f);
        }
        else if (rabbitHug)
        {
            hasEmo = true;
            sr.enabled = true;
            sr.sprite = happy;
            emoChanged = true;
            Invoke("ChangeEmoBack", 1f);
        }
     
            
        


    }

    public void DestroyEmotion()
    {
        hasEmo = false;
        sr.enabled = false;
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
                //sr.enabled = true;
                hasEmo = true;
                sr.sprite = sad;
                break;
            case 2:
                //sr.enabled = true;
                hasEmo = true;
                sr.sprite = gross;
                break;
            case 3:
                //sr.enabled = true;
                hasEmo = true;
                sr.sprite = happy;
                break;
            default:
                //sr.enabled = false;
                hasEmo = false;
                sr.sprite = null;
                break;
        }
    }
}
