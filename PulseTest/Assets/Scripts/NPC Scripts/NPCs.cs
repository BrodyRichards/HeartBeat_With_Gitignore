using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCs : MonoBehaviour
{
    //wahhh I need to research how to do variables and shit for inheritance shit later
    /*
    public GameObject master;
    public Animator anim;
    protected float speed;
    protected Vector3 scale;
    protected Vector3 scaleOpposite;
    protected GameObject Emo;
    protected int music;
    protected int check;
    private SpriteRenderer sp;
    protected bool schoolBell;
    private Vector3 target;
    private float currentPosX;
    private float lastPosX;
    */
    public Animator anim;
    protected float speed = 5f;
    protected Vector3 scale;
    protected Vector3 scaleOpposite;
    private float currentPosX;
    private float lastPosX;
    protected GameObject master;
    protected GameObject Emo;
    private int music;
    private int check;
    private SpriteRenderer sr;
    protected bool holdBunny = false;
    public static bool schoolBell = false;
    public Vector3 target;

    protected virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim.SetBool("IsWalking", true);
        Playground.RandomizeNpcAssets(anim, sr);
        master = GameObject.Find("GameController");
        scale = transform.localScale;
        scaleOpposite = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        music = RadioControl.currentMood;
        check = music;
    }

    protected virtual void Update()
    {
        if (schoolBell == false)
        {
            directionCheck(target.x, transform.position.x);
            avatarChecks();
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            DetectMovement();
            if (Input.GetKeyDown(KeyCode.P))
            {
                schoolBell = true;
            }
        }
        else
        {
            target = master.GetComponent<NpcInstantiator>().rightBound.transform.position;
            directionCheck(target.x, transform.position.x);
            runOff();
            if (transform.position == target)
            {
                Destroy(gameObject);
            }
        }
    }

    protected virtual void avatarChecks()
    {
        bool emoDist = checkDist(NpcInstantiator.musicKidPos, transform.position);
        check = music;
        music = RadioControl.currentMood;
        if (music != check || (emoDist && RadioControl.isMusic))
        {
            checkMusic();
        }
        checkBools(emoDist);
        checkRabbitCarry();
        
    }

    protected virtual void checkBools(bool emoDist)
    {
        if ((characterSwitcher.isMusicGuyInCharge == false && RabbitJump.beingCarried == false) || emoDist == false)
        {
            holdBunny = false;
            int count = transform.childCount;
            for (int i = 0; i < count; i++)
            {
                if (transform.GetChild(i).gameObject.tag != "Avatars" && holdBunny == false)
                {
                    GameObject.Destroy(transform.GetChild(i).gameObject);
                }
            }
        }
    }

    protected virtual void checkRabbitCarry()
    {
        if (RabbitJump.beingCarried)
        {
            int count = transform.childCount;
            for (int i = 0; i < count; i++)
            {
                if (transform.GetChild(i).gameObject.tag == "Avatars" && holdBunny == false)
                {
                    holdBunny = true;
                    Emo = master.GetComponent<NpcInstantiator>().happyFace;
                    addEmo();
                }
            }
        }
    }

    protected virtual void checkMusic()
    {
        if (RadioControl.currentMood == 1)
        {
            Emo = master.GetComponent<NpcInstantiator>().sadFace;
        }
        else if (RadioControl.currentMood == 2)
        {
            Emo = master.GetComponent<NpcInstantiator>().madFace;
        }
        else if (RadioControl.currentMood == 3)
        {
            Emo = master.GetComponent<NpcInstantiator>().happyFace;
        }
        addEmo();
    }

    protected virtual void directionCheck(float target1, float pos)
    {
        if (target1 >= 0)
        {
            if (pos >= 0)
            {
                if (target1 >= pos) { transform.localScale = scale; }
                else if (target1 <= pos) { transform.localScale = scaleOpposite; }
            }
            else if (pos <= 0) { transform.localScale = scale; }
        }
        else if (target1 <= 0)
        {
            if (pos >= 0) { transform.localScale = scaleOpposite; }
            else if (pos <= 0)
            {
                if (target1 >= pos) { transform.localScale = scale; }
                else if (target1 < pos) { transform.localScale = scaleOpposite; }
            }
        }
    }

    protected virtual void addEmo()
    {
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            if (transform.GetChild(i).gameObject.tag != "Avatars")
            {
                GameObject.Destroy(transform.GetChild(i).gameObject);
            }
        }
        Vector3 offset = new Vector3(0, 4.5f, 0);
        //GameObject balloon = Instantiate(Emo, transform.localPosition + offset, transform.rotation);
        //balloon.GetComponent<SpriteRenderer>().sortingLayerName = "Front Props";
        //balloon.transform.parent = transform;
    }

    protected virtual bool checkDist(Vector3 pos1, Vector3 pos2)  //for AOE
    {
        float dist = Vector3.Distance(pos1, pos2);
        if (dist <= 20.0f) { return true; }
        return false;
    }

    protected virtual void DetectMovement()
    {
        currentPosX = transform.position.x;
        if (currentPosX != lastPosX)
        {
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }

        lastPosX = transform.position.x;
    }

    protected virtual void runOff()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
