using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI; may need this in the future??

public class RabbitChasers : MonoBehaviour
{
    //public Animator anim;
    public Animator anim;
    Vector3 target;
    private float speed = 5f;
    private Vector3 scale;
    private Vector3 scaleOpposite;
    private float currentPosX;
    private float lastPosX;
    private GameObject master;
    GameObject Emo;
    private int music;
    private int check;

    private bool holdBunny = false;
    private bool schoolBell = false;

    // Start is called before the first frame update
    void Start()
    {
        master = GameObject.Find("GameController");
        int ranX = Random.Range((int)Playground.LeftX, (int)Playground.RightX);
        int ranY = Random.Range((int)Playground.LowerY, (int)Playground.UpperY);
        target = new Vector3(ranX, ranY, -1);
        scale = transform.localScale;
        scaleOpposite = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        music = RadioControl.currentMood;
        check = music;
        anim.SetBool("IsWalking", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (schoolBell == false)
        {
            bool emoDist = checkDist(NpcInstantiator.musicKidPos, transform.position);
            bool rabbitDist = checkDist(NpcInstantiator.rabbitPos, transform.position);
            directionCheck(target.x, transform.position.x);
            check = music;
            music = RadioControl.currentMood;
            if (music != check || (emoDist && RadioControl.isMusic)) { checkMusic(); }
            if (characterSwitcher.isMusicGuyInCharge == false && RabbitJump.beingCarried == false || emoDist == false)
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

            if (rabbitDist)
            {
                /*
                if (RabbitJump.beingCarried == false)
                {
                    Emo = master.GetComponent<NpcInstantiator>().surpriseFace;
                    addEmo();
                }
                */
                float dist = Vector3.Distance(NpcInstantiator.rabbitPos, transform.position);

                if (dist > 5.0f)
                {
                    target = NpcInstantiator.rabbitPos;
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

    void directionCheck(float target, float pos) //WHY DOES THIS GOTTA BE SO DAMN COMPLICATED MAN 
    {
        if (target >= 0)
        {
            if (pos >= 0)
            {
                if (target >= pos) { transform.localScale = scale; }
                else if (target <= pos) { transform.localScale = scaleOpposite; }
            }
            else if (pos <= 0) { transform.localScale = scale; }
        }
        else if (target <= 0)
        {
            if (pos >= 0) { transform.localScale = scaleOpposite; }
            else if (pos <= 0)
            {
                if (target >= pos) { transform.localScale = scale; }
                else if (target < pos) { transform.localScale = scaleOpposite; }
            }
        }
    }

    private void checkMusic()
    {
        if (RadioControl.currentMood == 1)
        {
            Emo = master.GetComponent<NpcInstantiator>().sadFace;
        }
        else if (RadioControl.currentMood == 2)
        {
            Emo = master.GetComponent<NpcInstantiator>().happyFace;
        }
        else if (RadioControl.currentMood == 3)
        {
            Emo = master.GetComponent<NpcInstantiator>().madFace;
        }
        addEmo();
    }
    private void addEmo()
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

    bool checkDist(Vector3 pos1, Vector3 pos2)  //for AOE of music kid
    {
        float dist = Vector3.Distance(pos1, pos2);
        if (dist <= 20.0f) { return true; }
        return false;
    }

    private void DetectMovement()
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

    private void runOff()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
