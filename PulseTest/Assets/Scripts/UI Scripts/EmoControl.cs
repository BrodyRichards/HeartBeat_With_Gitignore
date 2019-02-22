using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoControl : MonoBehaviour
{
    //public GameObject musicKid;

    public Sprite happy;
    public Sprite sad;
    public Sprite gross;
    public Sprite angry;

    private SpriteRenderer sr;

    public static int mcBallHit = 0;
    public static bool rabbitHug = false;
    private bool rabitJustHug = false;
    private bool emoDist;

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

        emoDist = Playground.CheckDist(NpcInstantiator.musicKidPos, transform.position, Playground.MusicAoe);

        if (mcBallHit > 1)
        {
            hasEmo = true;
            sr.enabled = true;
            sr.sprite = angry;
            emoChanged = true;
            McMovement.mcCurrentMood = 2;
            Invoke("DestroyEmotion", 1f);
            Invoke("ChangeEmoBack", 1f);

        }
        else if (rabbitHug)
        {
            rabitJustHug = true;
            hasEmo = true;
            sr.enabled = true;
            sr.sprite = happy;
            McMovement.mcCurrentMood = 1;


        }
        else if (emoDist)
        {
            ReactToMusic();
        }
        else
        {
            sr.enabled = false;
            McMovement.mcCurrentMood = 0;
        }
    }
   
    public void DestroyEmotion()
    {
        hasEmo = false;
        sr.enabled = false;
        McMovement.mcCurrentMood = 0;
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
                sr.enabled = true;
                hasEmo = true;
                sr.sprite = sad;
                McMovement.mcCurrentMood = 2;
                break;
            case 2:
                sr.enabled = true;
                hasEmo = true;
                sr.sprite = gross;
                McMovement.mcCurrentMood = 0;
                break;
            case 3:
                sr.enabled = true;
                hasEmo = true;
                sr.sprite = happy;
                McMovement.mcCurrentMood = 1;
                break;
            default:
                sr.enabled = false;
                hasEmo = false;
                sr.sprite = null;
                McMovement.mcCurrentMood = 0;
                break;
        }
    }

    //bool checkDist(Vector3 pos1, Vector2 pos2)
    //{
    //    float dist = Vector3.Distance(pos1, pos2);
    //    Debug.Log(dist);
    //    if (dist <= 30.0f) { return true; }
    //    return false;
    //}
}
