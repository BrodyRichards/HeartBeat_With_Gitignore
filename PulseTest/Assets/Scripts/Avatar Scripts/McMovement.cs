using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McMovement : MonoBehaviour
{
    public Animator anim;
    
    

    private Vector2[] mcWaypoints;
    private Vector2 musicGuyLo;
    private Vector2 rabbitLo;
    private Vector2 ballKidLo;
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
        anim.SetBool("isWalking", true);
        currentGoal = 0;
        lastX = transform.position.x;
        // get world x bound 
        worldX = GameObject.Find("/Quad").transform.localScale.x / 2;
        // get avatars position 
        musicGuyLo = GameObject.Find("3").transform.position;
        ballKidLo = GameObject.Find("2").transform.position;
        rabbitLo = GameObject.Find("1").transform.position;
        // put into array with some random waypoints, highly customizable 
        mcWaypoints = new Vector2[] {new Vector2(-44f, 2f), new Vector2(-50f, 2f), musicGuyLo,
            new Vector2(-27, 2), new Vector2(-13f, -6f), rabbitLo, new Vector2(23f, -2f), new Vector2(37f, -6f), new Vector2(63f, -3f), new Vector2(-13f, -6f), new Vector2(-70f, -8f) };
        
        // the array storing whether a waypoint has been reached 
        arrivals = new bool[mcWaypoints.Length];
        for (var i=0; i < mcWaypoints.Length; ++i)
        {
            arrivals[i] = false;
        }
    }

    void Update()
    {
        
        float step = speed * Time.deltaTime;
        // check whether the radio guy has been activated yet 
        if (RadioControl.isMusic)
        {
            GoToWaypoints(step);
        }
        FlipAssetDirection();

        

    }
    
    
    // Move towards the assigned waypoint, if arrive, will return true 
    private bool McGoesTo(Vector2 target, float step)
    {
        
        transform.position = Vector2.MoveTowards(transform.position, target, step);
        

        if (Vector2.Distance(transform.position, target) < 1.0f)
        {
            Debug.Log("arrive at" + target);
            
            return true;
        }
        
        return false;
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
        for (var i = 0; i < arrivals.Length; i++)
        {
            if (!arrivals[i])
            {
                arrivals[i] = McGoesTo(mcWaypoints[i], step);
                
                break;
            }
            else
            {
                continue;
            }
        }
    }
    // turn the mc around when hitting world bound, not really necessary anymore but have just in case 
    private void BoundCheck()
    {
        if ((transform.position.x > (worldX - 2) && direction == Vector2.right) ||
            (transform.position.x < (-worldX + 2) && direction == Vector2.left))
        {
            direction *= -1;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}
