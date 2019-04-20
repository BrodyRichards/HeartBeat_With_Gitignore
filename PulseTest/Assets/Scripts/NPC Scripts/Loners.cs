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
        //isWalking = anim.GetBool("IsWalking");
        if (schoolBell == false)
        {
            time = Time.fixedUnscaledTime;
            directionCheck(target.x, transform.position.x);
            avatarChecks();
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
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

    private void OnTriggerEnter2D(Collider2D other)                                     //might not need this?
    {
        if (other.tag == "Person" || other.tag == "Avatars" || other.tag == "MC")
        {
            int ranX = Random.Range((int)Playground.LeftX, (int)Playground.RightX);
            int ranY = Random.Range((int)Playground.LowerY, (int)Playground.UpperY);
            target = new Vector3(ranX, ranY, -1);
        }

    }


}
