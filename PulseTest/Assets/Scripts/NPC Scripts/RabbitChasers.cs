using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI; may need this in the future??

public class RabbitChasers : NPCs
{

    protected override void Awake()
    {       
        base.Awake();

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
            //if (Input.GetKeyDown(Control.evacuate) && !MentalState.journalInProgress)
            //{
            //    schoolBell = true;
            //}
        }
        else
        {
            toClass();
            anim.SetBool("wantToFeed", false);
        }
    }
    protected override void checkBallBunny(bool inDist, Vector3 avatarPos)
    {
        if (inDist)
        {
            float dist = Vector3.Distance(avatarPos, transform.position);

            if (dist > 10.0f)
            {
                target = avatarPos;
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                anim.SetBool("isFeeding", false);
                anim.SetBool("wantToFeed", false);
            }
            else
            {
                //if (anim.GetBool("isWalking") == false)
                //{
                    anim.SetBool("wantToFeed", true);
                //}
                //anim.SetBool("wantToFeed", true);
                /*
                 * YOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
                 * 
                 * 
                 *  PUT THE ANIMATION FOR THE BUNNY CARROT HEREEEEEEE
                 * 
                 * 
                 * YOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
                 * 
                 * */
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

    //UNCOMMENT THIS WHEN YOU PUT THE ANIM IN

    protected override void checkRabbitBit()                   
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
            Emo = master.GetComponent<NpcInstantiator>().happyFace;
            addEmo();
            anim.SetBool("isFeeding", true);
            /*
             * 
             * 
             * 
             * PUT THE FRAME OF HER SMILING IN HERE
             * 
             * 
             * 
             * 
             * */
             /*
            anim.SetTrigger("isBit");
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
                    if (Time.time >= rabbitTime)
                    {
                        rabbitTime = Time.time + rabbitCoolDown;
                        addQueue(5);
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
