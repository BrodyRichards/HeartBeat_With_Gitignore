using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI; may need this in the future??

public class Runners : MonoBehaviour
{
    Vector3 target;
    private GameObject area;   //quad
    private int areaX, areaY; //get the size of the quad
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
        area = GameObject.Find("Quad");
        areaX = ((int)area.transform.localScale.x) / 2 - 1;
        areaY = ((int)area.transform.localScale.y) / 2 - 1;
        int ranX = Random.Range((int)Playground.LeftX, (int)Playground.RightX);
        int ranY = Random.Range((int)Playground.LowerY, (int)Playground.UpperY);
        target = new Vector3(ranX, ranY, -1);
        scale = transform.localScale;
        scaleOpposite = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        music = RadioControl.currentMood;
        check = music;
    }

    // Update is called once per frame
    void Update()
    {
        directionCheck(target.x, transform.position.x);
        check = music;
        music = RadioControl.currentMood;
        if (music != check) { checkMusic(); }
        if (characterSwitcher.isMusicGuyInCharge == false && RabbitJump.beingCarried == false)
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
        if (transform.position == target)
        {
            int ranX = Random.Range((int)Playground.LeftX, (int)Playground.RightX);
            int ranY = Random.Range((int)Playground.LowerY, (int)Playground.UpperY);
            target = new Vector3(ranX, ranY, -1);    
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
        Vector3 offset = new Vector3(0, 3.5f, 0);
        GameObject balloon = Instantiate(Emo, transform.localPosition + offset, transform.rotation);
        balloon.transform.parent = transform;
    }
}
