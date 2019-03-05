using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI; may need this in the future??

public class Groupies : NPCs
{
    protected override void Update()
    {
        if (schoolBell == false)
        {
            time = Time.fixedUnscaledTime;
            directionCheck(target.x, transform.position.x);
            avatarChecks();
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (anim.GetBool("IsWalking") == true) { directionCheck(target.x, transform.position.x); }
            else { directionCheck(NpcInstantiator.center.x, transform.position.x); }
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

}
