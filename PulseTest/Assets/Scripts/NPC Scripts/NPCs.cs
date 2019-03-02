using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCs : MonoBehaviour
{
    //wahhh I need to research how to do variables and shit for inheritance shit later
    public GameObject master;
    public Animator anim;
    protected float speed;
    protected Vector3 scale;
    protected Vector3 scaleOpposite;
    protected GameObject Emo;
    protected int music;
    protected int check;
    protected bool schoolBell;
    private Vector3 target;
    private float currentPosX;
    private float lastPosX;

    private void Start()
    {
        master = GameObject.Find("GameController");
    }
    /*
    public NPCs()
    {
        master = GameObject.Find("GameController");
    }
    */

    protected virtual void directionCheck(float target, float pos)
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
