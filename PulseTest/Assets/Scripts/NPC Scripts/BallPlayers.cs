using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI; may need this in the future??

public class BallPlayers : NPCs
{
    private Rigidbody2D rb;
    private Vector3 mcPos;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        int ranX = Random.Range((int)Playground.LeftX, (int)Playground.RightX);
        int ranY = Random.Range((int)Playground.LowerY, (int)Playground.UpperY);
        target = new Vector3(ranX, ranY, -1);
        
    }

    // Update is called once per frame
    protected override void Update()
    {
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
            
            if (Input.GetKeyDown(Control.evacuate))
            {
                schoolBell = true;
            }
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
                BallProjectile.meanBallThrown = false;
            }
            else
            {
                timer = time + 2.0f;
                Emo = master.GetComponent<NpcInstantiator>().happyFace;
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
