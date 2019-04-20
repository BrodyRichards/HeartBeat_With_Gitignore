using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI; may need this in the future??

public class RabbitChasers : NPCs
{
    protected override void Start()
    {       
        base.Start();
        int ranX = Random.Range((int)Playground.LeftX, (int)Playground.RightX);
        int ranY = Random.Range((int)Playground.LowerY, (int)Playground.UpperY);
        /*
        scale = transform.localScale;
        scaleOpposite = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        */
        scale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        scaleOpposite = transform.localScale;
        target = new Vector3(ranX, ranY, -1);
    }

    protected override void Update()
    {
        //isWalking = anim.GetBool("IsWalking");
        if (schoolBell == false)
        {
            time = Time.fixedUnscaledTime;
            bool rabbitDist = checkDist(NpcInstantiator.rabbitPos, transform.position);
            directionCheck(target.x, transform.position.x);
            avatarChecks();
            checkBallBunny(rabbitDist, NpcInstantiator.rabbitPos);
            DetectMovement();
            if (Input.GetKeyDown(Control.evacuate) && !MentalState.journalInProgress)
            {
                schoolBell = true;
            }
        }
        else
        {
            toClass();
        }
    }

    protected override void checkRabbitCarry()
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
                    addQueue(5);
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (transform.position == target)
            {
                int ranX = Random.Range((int)Playground.LeftX, (int)Playground.RightX);
                int ranY = Random.Range((int)Playground.LowerY, (int)Playground.UpperY);
                target = new Vector3(ranX, ranY, -1);
            }
            /*
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
                Emo = master.GetComponent<NpcInstantiator>().sadFace;
                addEmo();
            }
            */

        }
    }
   
}
