using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI; may need this in the future??

public class BallPlayers : NPCs
{
    private Rigidbody2D rb;
    private Vector3 mcPos;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        int ranX = Random.Range((int)Playground.LeftX, (int)Playground.RightX);
        int ranY = Random.Range((int)Playground.LowerY, (int)Playground.UpperY);
        target = new Vector3(ranX, ranY, -1);
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        //isWalking = anim.GetBool("IsWalking");
        if (schoolBell == false)
        {
            mcPos = GameObject.Find("MC").transform.position;
            time = Time.fixedUnscaledTime;
            bool ballDist = checkDist(NpcInstantiator.ballKidPos, transform.position);
            directionCheck(target.x, transform.position.x);
            avatarChecks();
            bool mcClose = mcOverlay();
            if (mcClose == false)
            {
                checkBallBunny(ballDist, NpcInstantiator.ballKidPos);
            }
            DetectMovement();
            
     
        }
        else
        {
            toClass();
        }
    }

    protected override void playBall()
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
                timer = time + 2.0f;
                Emo = master.GetComponent<NpcInstantiator>().madFace;
                addEmo();
                addQueue(2);
                BallProjectile.meanBallThrown = false;
                anim.SetTrigger("isHit");
            }
            else
            {
                anim.SetTrigger("playCatch");
                timer = time + 2.0f;
                Emo = master.GetComponent<NpcInstantiator>().happyFace;
                addQueue(1);
                addEmo();
            }
            
        }
    }

    private bool mcOverlay()
    {
        float dist = Vector3.Distance(transform.position, mcPos);
        if (dist <= 5.0f)
        {
            return true;
        }
        return false;
    }
}
