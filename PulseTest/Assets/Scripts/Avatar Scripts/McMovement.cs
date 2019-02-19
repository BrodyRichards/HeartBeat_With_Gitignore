using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McMovement : MonoBehaviour
{
    public Animator anim;
    public static int mcCurrentMood = 0;

    private List<Vector2> mcWaypoints;
    private int checkArrivals;
    private Vector2 direction;

    private float step;
    public float speed;
    private double worldX;

    private bool[] arrivals;
    private bool walkedIn;
    private int currentGoal;
    private float lastX;
    private bool isFlipped;
    void Start()
    {
        // some initializations
        walkedIn = false;
        isFlipped = false;
        anim.SetBool("isWalking", false);
        currentGoal = 0;
        // put into array with some random waypoints, highly customizable 
        mcWaypoints = new List<Vector2> { new Vector2(-86f, 2f),
            new Vector2(-50f, -15f), new Vector2(-10f, -15f), new Vector2(-50f, -15f), new Vector2(51f, -15f), new Vector2(23f, -2f), new Vector2(8f, -8f), new Vector2(58f, 6f), new Vector2(87f, 6f), new Vector2(58f, 6f), new Vector2(121f, -6f) };

        // the array storing whether a waypoint has been reached 



    }

    void Update()
    {

        float step = speed * Time.deltaTime;
        // check whether the radio guy has been activated yet 
        if (RadioControl.isMusic)
        {
            if (!EmoControl.emoChanged)
            {
                anim.SetBool("isWalking", true);
                GoToWaypoints(step);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }

        }

        FlipAssetDirection();

        AnimationMoodCheck();
        

    }


    // Move towards the assigned waypoint, if arrive, will return true 
    private void McGoesTo(Vector2 target, float step)
    {

        transform.position = Vector2.MoveTowards(transform.position, target, step);


        if (Vector2.Distance(transform.position, target) < 1.0f)
        {
            Debug.Log("arrive at" + target);

            mcWaypoints.RemoveAt(0);

        }


    }

    // Flip asset when MC switch direction 
    private void FlipAssetDirection()
    {
        if (lastX > transform.position.x && !isFlipped)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            isFlipped = true;


        }
        else if (lastX < transform.position.x && isFlipped)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            isFlipped = false;
        }
        lastX = transform.position.x;
    }
    // Go to the assigned waypoints that haven't been reached yet
    private void GoToWaypoints(float step)
    {
        if (mcWaypoints.Count != 0)
        {
            McGoesTo(mcWaypoints[0], step);
        }
        else
        {
            mcWaypoints = new List<Vector2> { new Vector2(-86f, 2f),
            new Vector2(-50f, -15f), new Vector2(-10f, -15f), new Vector2(-50f, -15f), new Vector2(51f, -15f), new Vector2(23f, -2f), new Vector2(8f, -8f), new Vector2(58f, 6f), new Vector2(87f, 6f), new Vector2(58f, 6f), new Vector2(121f, -6f) };
        }


    }

    private void AnimationMoodCheck()
    {
        anim.SetInteger("mood", mcCurrentMood);
    }
}
    


//// turn the mc around when hitting world bound, not really necessary anymore but have just in case 
    //private void BoundCheck()
    //{
    //    if ((transform.position.x > Playground.RightX && direction == Vector2.right) ||
    //        (transform.position.x < Playground.LeftX && direction == Vector2.left))
    //    {
    //        direction *= -1;
    //        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    //    }
    //}

