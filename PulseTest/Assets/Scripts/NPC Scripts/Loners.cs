using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI; may need this in the future??

public class Loners : MonoBehaviour
{
    public Animator anim;
    Vector3 target;
    private float speed = 5f;
    private Vector3 scale;
    private Vector3 scaleOpposite;

    private GameObject master;
    GameObject Emo;
    private int music;
    private int check;

    private bool holdBunny = false;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Person" || other.tag == "Avatars" || other.tag == "MC")
        {
            int ranX = Random.Range((int)Playground.LeftX, (int)Playground.RightX);
            int ranY = Random.Range((int)Playground.LowerY, (int)Playground.UpperY);
            target = new Vector3(ranX, ranY, -1);
        }

    }

    private void FixedUpdate()
    {
        bool emoDist = checkDist(NpcInstantiator.musicKidPos, transform.position);
        directionCheck(target.x, transform.position.x);
        check = music;
        music = RadioControl.currentMood;      
        if (music != check || (emoDist && RadioControl.isMusic))
        {
            checkMusic();
        }
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
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
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
        if (RadioControl.currentMood == 1) //sad music
        {
            Emo = master.GetComponent<NpcInstantiator>().happyFace;
        }
        else if (RadioControl.currentMood == 2) //scary music
        {
            Emo = master.GetComponent<NpcInstantiator>().sadFace;
        }
        else if (RadioControl.currentMood == 3)
        {
            Emo = master.GetComponent<NpcInstantiator>().madFace; //happy music
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
        balloon.GetComponent<SpriteRenderer>().sortingLayerName = "Main";
        balloon.transform.parent = transform;
    }

    bool checkDist(Vector3 pos1, Vector3 pos2)  //for AOE of music kid
    {
        float dist = Vector3.Distance(pos1, pos2);
        if (dist <= 30.0f) { return true; }
        return false;
    }
}
