using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McMovement : MonoBehaviour
{
    public Animator anim;

    private Vector2 direction;
    public float speed = 6f;
    private double worldX;

    private Vector2 musicGuyLo;
    private Vector2 rabbitLo;
    private Vector2 ballKidLo;
    private Vector2 startLo;
    float step;

    private Vector2[] mcWaypoints;
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
        mcWaypoints = new Vector2[] { musicGuyLo, rabbitLo, ballKidLo, new Vector2(0f, 0f), new Vector2(-20f, 20f), new Vector2(-10f, 10f) };
        // the array storing whether a waypoint has been reached 
        arrivals = new bool[] { false, false, false, false, false, false };

        

        

       
        
    }

    void Update()
    {
        speed = Random.Range(5f, 10f);
        float step = speed * Time.deltaTime;

        GoToWaypoints(step);

        FlipAssetDirection();

        BoundCheck();

    }

    private void BoundCheck()
    {
        if ((transform.position.x > (worldX - 2) && direction == Vector2.right) ||
            (transform.position.x < (-worldX + 2) && direction == Vector2.left))
        {
            direction *= -1;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

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
}
