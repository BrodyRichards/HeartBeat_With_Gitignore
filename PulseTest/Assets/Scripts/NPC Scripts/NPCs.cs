using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCs : MonoBehaviour
{
    public Animator anim;
    protected float speed;// = Random.Range(3f, 5f);

    protected Vector3 scale;
    protected Vector3 scaleOpposite;

    private float currentPosX;
    private float lastPosX;

    private float musicCoolDown;
    private float curTime;
    protected float rabbitCoolDown;
    protected float rabbitTime;

    protected GameObject master;
    protected GameObject Emo;
    public static Queue<int> actions; //a queue of integers as Ball: 1, 2; Music: 3, 4; Rabbit: 5, 6 (bigger number is the negative)

    private int music;
    private int check;

    private SpriteRenderer sr;

    protected bool holdBunny = false;
    protected bool nameChange = false;
    protected bool rabNameChange = false;
    //public bool isWalking = false;
    public static bool schoolBell = false;
    public Vector3 target;

    protected float time;
    protected float timer;



    protected virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim.SetBool("IsWalking", true);
        //Playground.RandomizeNpcAssets(anim, sr, gameObject);
        master = GameObject.Find("GameController");
        scale = transform.localScale;
        scaleOpposite = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        music = RadioControl.currentMood;
        check = music;
        time = Time.fixedUnscaledTime;
        timer = time;
        speed = Random.Range(3f, 6f);
        Debug.Log("speed: " + speed);
        actions = new Queue<int> { };
        musicCoolDown = 4f;
        curTime = 0f;
        rabbitCoolDown = 4f;
        rabbitTime = 0f;
    }

    protected virtual void Update()
    {
        //isWalking = anim.GetBool("IsWalking");
        if (schoolBell == false)
        {
            time = Time.fixedUnscaledTime;
            directionCheck(target.x, transform.position.x);
            avatarChecks();
            DetectMovement();
            //if (Input.GetKeyDown(Control.evacuate) && !MentalState.journalInProgress) 
            //{ 
            //    schoolBell = true;
            //}
        }
        else
        {
            target = master.GetComponent<NpcInstantiator>().rightBound.transform.position;
            directionCheck(target.x, transform.position.x);
            runOff();
            DetectMovement();
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
        //if (music != check || (emoDist && RadioControl.isMusic))
        if (this.gameObject.name == RadioControl.musicListener)
        {
            checkMusic();
        }
        if (this.gameObject.name == BallProjectile.NpcName && BallProjectile.meanBallThrown)
        {
            Emo = master.GetComponent<NpcInstantiator>().madFace;
            addEmo();
            //addQueue(2);
        }
        if (BallProjectile.NpcName == this.gameObject.name)
        {
            BallProjectile.NpcName = "";
            nameChange = true;
            playBall();
            //addQueue(1);
        }
        if (RabbitJump.bitNpcName == this.gameObject.name)
        {
            Debug.Log("this guy got bit lmaooo: " + RabbitJump.bitNpcName);
            RabbitJump.bitNpcName = "";
            rabNameChange = true;
            checkRabbitBit();
            addQueue(6);
        }
        checkBools(emoDist);
        checkRabbitCarry();      
    }

    protected virtual void checkBools(bool emoDist)
    {
        //if ((characterSwitcher.isMusicGuyInCharge == false && RabbitJump.beingCarried == false) || emoDist == false)
        if ((RadioControl.musicListener != this.gameObject.name && RabbitJump.beingCarried == false) || emoDist == false)
        {
            if (nameChange == false && rabNameChange == false)
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
        if (timer <= time)
        {
            nameChange = false;
            rabNameChange = false;
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
                    Emo = master.GetComponent<NpcInstantiator>().surpriseFace;
                    addEmo();
                    if (Time.time >= rabbitTime)
                    {
                        rabbitTime = Time.time + rabbitCoolDown;
                        addQueue(5);
                    }
                }
            }
        }
    }

    protected virtual void checkRabbitBit()
    {
        if (rabNameChange)
        {
            int count = transform.childCount;
            for (int i = 0; i < count; i++)
            {
                if (transform.GetChild(i).gameObject.tag != "Avatars" && holdBunny == false)
                {
                    GameObject.Destroy(transform.GetChild(i).gameObject);
                }
            }
            timer = time + 2.0f;
            Emo = master.GetComponent<NpcInstantiator>().hurtFace;
            addEmo();
        }
    }

    protected virtual void checkMusic()
    {
        if (RadioControl.currentMood == 1)                      //sad song
        {
            Emo = master.GetComponent<NpcInstantiator>().sadFace;
            if (Time.time >= curTime)
            {
                addQueue(4);
                curTime = Time.time + musicCoolDown;
            }
        }
        else if (RadioControl.currentMood == 0)                 //happy song
        {
            Emo = master.GetComponent<NpcInstantiator>().groovinFace;
            if (Time.time >= curTime)
            {
                Debug.Log("I'm happy");
                addQueue(3);
                curTime = Time.time + musicCoolDown;
            }
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
        GameObject balloon = Instantiate(Emo, transform.localPosition + offset, transform.rotation);
        balloon.GetComponent<SpriteRenderer>().sortingLayerName = "Front Props";
        balloon.transform.parent = transform;
    }

    protected virtual bool checkDist(Vector3 pos1, Vector3 pos2)  //for AOE
    {
        float dist = Vector3.Distance(pos1, pos2);
        if (dist <= RadioControl.actionDist && characterSwitcher.isMusicGuyInCharge) { return true; }
        else if (dist <= 20.0f) { return true; }
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

    protected virtual void playBall()
    {
        if (nameChange)
        {
            int count = transform.childCount;
            for (int i = 0; i < count; i++)
            {
                if (transform.GetChild(i).gameObject.tag != "Avatars" && holdBunny == false)
                {
                    GameObject.Destroy(transform.GetChild(i).gameObject);
                }
            }
            if (BallProjectile.meanBallThrown)
            {
                Debug.Log("ouch");
                timer = time + 2.0f;
                Emo = master.GetComponent<NpcInstantiator>().madFace;
                addQueue(2);
                addEmo();
                BallProjectile.meanBallThrown = false;
            }
            else { addQueue(1); }
        }
    }

    protected virtual void checkBallBunny(bool inDist, Vector3 avatarPos)
    {
        if (inDist)
        {
            float dist = Vector3.Distance(avatarPos, transform.position);

            if (dist > 10.0f)
            {
                target = avatarPos;
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (transform.position == target)
            {
                int ranX = Random.Range((int)Playground.LeftX, (int)Playground.RightX);
                int ranY = Random.Range((int)Playground.LowerY, (int)Playground.UpperY);
                target = new Vector3(ranX, ranY, -1);
            }
        }
    }

    protected virtual void runOff()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    protected virtual void toClass()
    {
        target = master.GetComponent<NpcInstantiator>().rightBound.transform.position;
        directionCheck(target.x, transform.position.x);
        runOff();
        DetectMovement();
        if (transform.position == target)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void addQueue(int num)
    {
        actions.Enqueue(num);
        Debug.Log(num);
        if (actions.Count > 5)
        {
            actions.Dequeue();
        }

        MentalState.UpdateNPCMood(num);
    }
}
