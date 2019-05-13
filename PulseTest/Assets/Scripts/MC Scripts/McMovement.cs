using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McMovement : MonoBehaviour
{
    public Animator anim;
    //public static int mcCurrentMood = 0;

    public List<Vector2> mcWaypoints;
    public List<Vector2> refPoints;
    private int checkArrivals;
    private Vector2 direction;
    private enum Mood { happy, sad, idle };
    private string[] controllersName = { "MC_happy", "MC_sad", "MC_controller"};
    private float step;
    public static float speed;
    private double worldX;

    private int moodBound = 10;
    private bool[] arrivals;
    private bool walkedIn;
    private int currentGoal;
    private float lastX;
    private bool isFlipped;

    public float followDist;
    public float timeElapsed = 0f;
    public bool CRunning = false;
    public static bool playedCatch = false;
    public static bool gotHit = false;
    public bool stillInterested = true;
    public bool endScene;
    public bool tutorialScene;
    public static float leftBoundDist;

    void Start()
    {
        // some initializations
        walkedIn = false;
        isFlipped = false;
        anim.SetBool("isWalking", false);
        currentGoal = 0;
        // put into array with some random waypoints, highly customizable 
        //mcWaypoints = new List<Vector2> { new Vector2(-86f, 2f),
        //    new Vector2(-50f, -15f), new Vector2(-10f, -15f), new Vector2(-50f, -15f), new Vector2(51f, -15f), new Vector2(23f, -2f), new Vector2(8f, -8f), new Vector2(58f, 6f), new Vector2(87f, 6f), new Vector2(58f, 6f), new Vector2(121f, -6f) };

        // the array storing whether a waypoint has been reached 

        refPoints = new List<Vector2>(mcWaypoints);

        followDist = 20.0f;
    }

    void Update()
    {
        
        // check whether the radio guy has been activated yet 
        //if (MusicKidBT.isMusic && !walkedIn)
        /*
        if (characterSwitcher.charChoice != -1 && !walkedIn)
        {
            /*
            if (Playground.CheckDist(NpcInstantiator.musicKidPos, transform.position, Playground.MusicAoe))
            {
            
            walkedIn = true;
            //  anim.SetInteger("mood", MentalState.mood);

            //}
        }
    */
        walkedIn = true;
        leftBoundDist = Vector3.Distance(transform.position, GameObject.Find("LeftBound").transform.position);
        float step = speed * Time.deltaTime;
        if (walkedIn || endScene || tutorialScene)
        {
            FlipAssetDirection();
            AnimationMoodCheck();

            if (RabbitJump.bittenMC)
            {
                if (!CRunning)
                {
                    CRunning = true;
                    StartCoroutine(McRunsFromAvatar(NpcInstantiator.rabbitPos, step));
                }
            }
            else if (CheckDist(transform.position, NpcInstantiator.ballKidPos) && BallProjectile.meanBallThrown && gotHit)
            {
                if (!CRunning)
                {
                    CRunning = true;
                    StartCoroutine(McRunsFromAvatar(NpcInstantiator.ballKidPos, step));
                }
            }
            else if (CheckDist(transform.position, NpcInstantiator.ballKidPos) && !endScene && stillInterested && !RabbitJump.beingCarried)
            {
                if (!CRunning)
                {
                    McGoesToAvatar(NpcInstantiator.ballKidPos, step);
                }
            }
            else if (MusicKidBT.musicListener=="MC" && MusicKidBT.currentMood == 0)
            {
                anim.SetBool("isWalking", false);
            }
            else
            {
                if (!CRunning)
                {
                    GoToWaypoints(step);
                    anim.SetBool("wantToPlay", false);
                    anim.SetBool("isWalking", true);
                }
            }
        }
    }

    IEnumerator McRunsFromAvatar(Vector2 target, float step)
    {
        float timePassed = 0f;

        if (gotHit)
        {
            gotHit = !gotHit;
        }

        anim.SetBool("isWalking", true);
        anim.SetBool("wantToPlay", false);
        while (timePassed < 4f)
        {
            if (transform.position.x > Playground.RightX ||
                transform.position.x < Playground.LeftX ||
                transform.position.y > Playground.UpperY ||
                transform.position.y < Playground.LowerY)
            {
                //Trying to exit bound
                timePassed += Time.deltaTime;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, target, (-1) * step);
                timePassed += Time.deltaTime;

                yield return null;
            }
        }

        CRunning = false;
        yield break;
    }

    private void McGoesToAvatar(Vector2 target, float step)
    {
        //When standing still, if not played catch then walk away after 5 secs
        //Otherwise, increment time when not playing catch until 5 secs
        if (timeElapsed < 7f)
        {
            if (Vector2.Distance(transform.position, target) < 10.0f)
            {
                anim.SetBool("wantToPlay", true);
                anim.SetBool("isWalking", false);
                FlipAssetForBallKid();
                if (playedCatch)
                {
                    timeElapsed = 0;
                    playedCatch = false;
                }
                else
                {
                    timeElapsed += Time.deltaTime;
                    //Debug.Log(timeElapsed);
                }
            }
            else
            {
                anim.SetBool("wantToPlay", false);
                anim.SetBool("isWalking", true);
                transform.position = Vector2.MoveTowards(transform.position, target, step);
            }
        }
        else
        {
            stillInterested = false;
            //Still need to figure out when to set playedCatch to false
            //and need to break out of if condition
            //Walk away
        }
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
            mcWaypoints = new List<Vector2>(refPoints);
        }
    }

    // Move towards the assigned waypoint, if arrive, will return true 
    private void McGoesTo(Vector2 target, float step)
    {

        transform.position = Vector2.MoveTowards(transform.position, target, step);

        if (Vector2.Distance(transform.position, target) < 1.0f)
        {
            //Debug.Log("arrive at" + target);
            stillInterested = true;
            timeElapsed = 0f;
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

    private void FlipAssetForBallKid()
    {
        if (NpcInstantiator.ballKidPos.x < transform.position.x && transform.localScale.x > 0 && !isFlipped)
        {
            isFlipped = true;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            //Debug.LogError("why the fuck is this not called?");
        }
        else if (NpcInstantiator.ballKidPos.x > transform.position.x && transform.localScale.x < 0 && isFlipped)
        {
            isFlipped = false;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    private void AnimationMoodCheck()
    {
        
        
        if (MentalState.WithinRange(MentalState.currentState, -5, 5)) // no mood
        {
            //Debug.Log("currentMood is calm" + MentalState.mood);
            //var scaling = !isFlipped ? new Vector2(1.0f, 1.0f) : new Vector2(-1.0f, 1.0f);
            //transform.localScale = scaling;
            speed = 4;
            //anim.SetInteger("mood", (int)Mood.idle );
            SwitchAnimController((int)Mood.idle);
            anim.SetFloat("speed", 1f);
        }
        else if (MentalState.WithinRange(MentalState.currentState, 6, 30)) // happy
        {
            //anim.SetInteger("mood", (int)Mood.happy);
            SwitchAnimController((int)Mood.happy);
            anim.SetFloat("speed", 0.25f);
            speed = 6;
        }
        else if (MentalState.WithinRange(MentalState.currentState, -30, -6)) // sad 
        {
            var scaling = isFlipped ? new Vector2(-1.1f, 1.1f) : new Vector2(1.1f, 1.1f);
            transform.localScale = scaling;
            
            speed = 3;
            //anim.SetInteger("mood", (int)Mood.sad);
            SwitchAnimController((int)Mood.sad);
            anim.SetFloat("speed", 0.1f);
        }
        else
        {
            //Debug.Log("mood overfloww");
        }
    }

    private bool CheckDist(Vector3 pos1, Vector3 pos2)
    {
        float dist = Vector3.Distance(pos1, pos2);
        if (dist <= followDist) { return true; }
        return false;
    }

    private void SwitchAnimController(int i)
    {
        anim.runtimeAnimatorController = Resources.Load(controllersName[i]) as RuntimeAnimatorController;
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

