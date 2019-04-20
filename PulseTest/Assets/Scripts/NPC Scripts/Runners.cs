using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI; may need this in the future??

public class Runners : NPCs
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
        //isWalking = anim.GetBool("IsWalking");
        if (schoolBell == false)
        {
            time = Time.fixedUnscaledTime;
            directionCheck(target.x, transform.position.x);
            avatarChecks();
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (transform.position == target)
            {
                int ranX = Random.Range((int)Playground.LeftX, (int)Playground.RightX);
                int ranY = Random.Range((int)Playground.LowerY, (int)Playground.UpperY);
                target = new Vector3(ranX, ranY, -1);
            }
            DetectMovement();
            //if (Input.GetKeyDown(Control.evacuate) && !MentalState.journalInProgress)
            //{
            //    schoolBell = true;
            //}
        }
        else
        {
            toClass();
        }
        
        
    }
    
    /*
    protected override void checkMusic()
    {
        if (RadioControl.currentMood == 1) //
        {
            Emo = master.GetComponent<NpcInstantiator>().sadFace;

        }
        else if (RadioControl.currentMood == 0)
        {
            Emo = master.GetComponent<NpcInstantiator>().happyFace;
        }
        addEmo();
    }
    */
}
