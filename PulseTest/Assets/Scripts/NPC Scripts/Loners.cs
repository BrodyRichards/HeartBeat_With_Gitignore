using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loners : NPCs
{
    protected override void Start()
    {
        base.Start();
        int ranX = Random.Range((int)Playground.LeftX, (int)Playground.RightX);
        int ranY = Random.Range((int)Playground.LowerY, (int)Playground.UpperY);
        target = new Vector3(ranX, ranY, -1);
    }

    protected override void Update()
    {
        if (schoolBell == false)
        {
            directionCheck(target.x, transform.position.x);
            avatarChecks();
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            DetectMovement();
            if (Input.GetKeyDown(Control.evacuate))
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

    private void OnTriggerEnter2D(Collider2D other)                                     //might not need this?
    {
        if (other.tag == "Person" || other.tag == "Avatars" || other.tag == "MC")
        {
            int ranX = Random.Range((int)Playground.LeftX, (int)Playground.RightX);
            int ranY = Random.Range((int)Playground.LowerY, (int)Playground.UpperY);
            target = new Vector3(ranX, ranY, -1);
        }

    }

    protected override void checkMusic()
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
}
