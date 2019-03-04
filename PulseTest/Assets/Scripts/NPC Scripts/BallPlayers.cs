using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI; may need this in the future??

public class BallPlayers : NPCs
{
    private Rigidbody2D rb;
    private bool nameChange = false;
    float time;
    float timer;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        int ranX = Random.Range((int)Playground.LeftX, (int)Playground.RightX);
        int ranY = Random.Range((int)Playground.LowerY, (int)Playground.UpperY);
        target = new Vector3(ranX, ranY, -1);
        time = Time.fixedUnscaledTime;
        timer = time;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (schoolBell == false)
        {
            time = Time.fixedUnscaledTime;
            bool ballDist = checkDist(NpcInstantiator.ballKidPos, transform.position);
            directionCheck(target.x, transform.position.x);
            avatarChecks();
            checkBall(ballDist);
            if (BallProjectile.NpcName == this.gameObject.name)
            {
                BallProjectile.NpcName = "";
                nameChange = true;
                playBall();
            }

            if (timer <= time)
            {
                nameChange = false;
            }
            
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

    protected override void checkBools(bool emoDist)
    {
        if (characterSwitcher.isMusicGuyInCharge == false && RabbitJump.beingCarried == false || emoDist == false)
        {
            if (nameChange == false)
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
        }
    }

    private void playBall()
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
            timer = time + 2.0f;
            Emo = master.GetComponent<NpcInstantiator>().happyFace;
            addEmo();
        }
    }

    private void checkBall(bool ballDist)
    {
        if (ballDist)
        {
            float dist = Vector3.Distance(NpcInstantiator.ballKidPos, transform.position);

            if (dist > 10.0f)
            {
                target = NpcInstantiator.ballKidPos;
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            }
        }
        Vector3 offset = new Vector3(0, 4.5f, 0);
        //GameObject balloon = Instantiate(Emo, transform.localPosition + offset, transform.rotation);
        //balloon.GetComponent<SpriteRenderer>().sortingLayerName = "Front Props";
        //balloon.transform.parent = transform;
    }
}
