using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCBTCreator : MonoBehaviour
{
    public Animator anim;

    public List<Vector2> mcWaypoints;
    public List<Vector2> refPoints;
    private string[] controllersName = { "MC_happy", "MC_sad", "MC_controller" };
    private enum Mood { happy, sad, idle };

    public float followDist;
    public static float walkSpeed;
    public bool stillInterested;

    private bool isFlipped;
    private bool biteStatus;
    private bool ballStatus;
    private float lastX;
    private float rabbitTimePassed;
    private float ballTimePassed;
    private float interestTimer;
    private float interestTime;
    private float interestResetTimer;

    public static bool gotHit = false;
    public static bool playedCatch = false;

    private Vector3 target;

    Node MC_BT;

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("isWalking", false);

        isFlipped = false;
        stillInterested = true;
        followDist = 20f;
        rabbitTimePassed = 0f;
        ballTimePassed = 0f;
        interestTimer = 0f;
        interestTime = 7f;
        interestResetTimer = 0f;

        MC_BT = createBehaviorTree();
        refPoints = new List<Vector2>(mcWaypoints);

        target = GameObject.Find("GameController").GetComponent<NpcInstantiator>().rightBound.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MC_BT.Evaluate();
    }

    /*-------------------Tree Creation--------------------*/

    Node createBehaviorTree()
    {
        //Default child for MC walking
        Leaf Walk = new Leaf(GoToWaypoints);

        //Create Rabbit Bite Check Sequence
        //Leaf CheckBite = new Leaf(checkBite);
        //Leaf RunFromRabbit = new Leaf(runFromRabbit);
        //Sequence RabbitSeq = createSeqRoot(CheckBite, RunFromRabbit);

        //Create Ball Kid mean ball check Sequence
        //Leaf CheckMeanThrow = new Leaf(checkMeanThrow);
        //Leaf RunFromBallKid = new Leaf(runFromBallKid);
        //Sequence MeanBallSeq = createSeqRoot(CheckMeanThrow, RunFromBallKid);

        //Update MC
        Leaf UpdateMC = new Leaf(updateMC);

        //Create Play Catch with Ball Kid Sequence
        Leaf CheckExist = new Leaf(checkExist);
        Leaf CheckCatch = new Leaf(checkCatch);
        Leaf CheckInterest = new Leaf(checkInterest);
        Leaf MoveToBallKid = new Leaf(moveToBallKid);
        Sequence PlayCatch = createSeqRoot(CheckExist, CheckCatch, CheckInterest, MoveToBallKid);

        //Music Kid Sequence
        Leaf CheckMusic = new Leaf(checkMusic);

        Selector root = createSelRoot(UpdateMC, PlayCatch, CheckMusic, Walk);

        return root;
    }

    /*------------------Helper functions------------------*/

    Sequence createSeqRoot(params Node[] nodeList)
    {
        List<Node> rootOrder = new List<Node>();

        foreach (Node n in nodeList)
        {
            rootOrder.Add(n);
        }

        Sequence newSeq = new Sequence(rootOrder);

        return newSeq;
    }

    Selector createSelRoot(params Node[] nodeList)
    {
        List<Node> rootOrder = new List<Node>();

        foreach (Node n in nodeList)
        {
            rootOrder.Add(n);
        }

        Selector newSel = new Selector(rootOrder);

        return newSel;
    }

    private void ExitCheck()
    {
        RaycastHit2D wallCheck = Physics2D.Raycast(transform.position, transform.right, 0.25f);
        Debug.Log("Checking...");
        //Check for MC exit scene
        if (wallCheck.collider != null && wallCheck.collider.gameObject.name == "RightBound")
        {
            Debug.Log("Leaving now");
            LevelFade.readyToLeave = true;
            //Debug.Log("Loading ResultScreen...");
            //SceneManager.LoadScene("ResultScreen");
        }
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

    private void McGoesToAvatar(Vector2 target)
    {
        FlipAssetDirection();

        //When standing still, if not played catch then walk away after 5 secs
        //Otherwise, increment time when not playing catch until 5 secs
        if (Vector2.Distance(transform.position, target) < 10.0f)
        {
            anim.SetBool("wantToPlay", true);
            anim.SetBool("isWalking", false);
            FlipAssetForBallKid();
            if (playedCatch)
            {
                playedCatch = false;
            }
        }
        else
        {
            anim.SetBool("wantToPlay", false);
            anim.SetBool("isWalking", true);
            transform.position = Vector2.MoveTowards(transform.position, target, walkSpeed * Time.deltaTime);
        }
        //stillInterested = false;
        //Still need to figure out when to set playedCatch to false
        //and need to break out of if condition
        //Walk away
    }

    private void McRunsFromAvatar(Vector2 target)
    {
        AnimationMoodCheck();
        FlipAssetDirection();

        anim.SetBool("isWalking", true);
        anim.SetBool("wantToPlay", false);

        if (gotHit)
        {
            gotHit = !gotHit;
        }

        if (transform.position.x > Playground.RightX ||
            transform.position.x < Playground.LeftX ||
            transform.position.y > Playground.UpperY ||
            transform.position.y < Playground.LowerY)
        {
            //Trying to exit bound
            //timePassed += Time.deltaTime;
            anim.SetBool("isWalking", false);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, target, (-1) * walkSpeed * Time.deltaTime);
            //timePassed += Time.deltaTime;
        }
    }

    private bool CheckDist(Vector3 pos1, Vector3 pos2)
    {
        float dist = Vector3.Distance(pos1, pos2);
        if (dist <= followDist) { return true; }
        return false;
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
            walkSpeed = 4;
            //anim.SetInteger("mood", (int)Mood.idle );
            SwitchAnimController((int)Mood.idle);
            anim.SetFloat("speed", 1f);
        }
        else if (MentalState.WithinRange(MentalState.currentState, 6, 30)) // happy
        {
            //anim.SetInteger("mood", (int)Mood.happy);
            SwitchAnimController((int)Mood.happy);
            anim.SetFloat("speed", 0.25f);
            walkSpeed = 6;
        }
        else if (MentalState.WithinRange(MentalState.currentState, -30, -6)) // sad 
        {
            var scaling = isFlipped ? new Vector2(-1.1f, 1.1f) : new Vector2(1.1f, 1.1f);
            transform.localScale = scaling;

            walkSpeed = 3;
            //anim.SetInteger("mood", (int)Mood.sad);
            SwitchAnimController((int)Mood.sad);
            anim.SetFloat("speed", 0.1f);
        }
        else
        {
            //Debug.Log("mood overfloww");
        }
    }

    private void updateInterest()
    {
        //If the interest timer is still under 7 seconds then the MC is still interested
        //Else if not interested then start counting for 5 seconds to reset interest
        if (interestTimer <= interestTime)
        {
            interestResetTimer = 0f;
            stillInterested = true;
        }
        else
        {
            //If interest timer hasn't counted to 5 then keep counting and report not interested
            //Otherwise, update interest and report interested again
            if (interestResetTimer <= 10f)
            {
                stillInterested = false;
                interestResetTimer += Time.deltaTime;
            }
            else
            {
                interestTimer = 0f;
                stillInterested = true;
            }
        }
    }

    private void SwitchAnimController(int i)
    {
        anim.runtimeAnimatorController = Resources.Load(controllersName[i]) as RuntimeAnimatorController;
    }

    /*------------------Leaf functions------------------*/

    NodeStatus GoToWaypoints()
    {
        AnimationMoodCheck();
        FlipAssetDirection();

        if (mcWaypoints.Count != 0)
        {
            //McGoesTo(mcWaypoints[0], step);
            transform.position = Vector2.MoveTowards(transform.position, mcWaypoints[0], walkSpeed * Time.deltaTime);
            anim.SetBool("isWalking", true);

            if (Vector2.Distance(transform.position, mcWaypoints[0]) < 1.0f)
            {
                //Debug.Log("arrive at" + target);
                mcWaypoints.RemoveAt(0);
            }
        }
        else
        {
            mcWaypoints = new List<Vector2>(refPoints);
        }

        return NodeStatus.SUCCESS;
    }

    NodeStatus checkBite()
    {
        if (RabbitJump.bittenMC)
        {
            biteStatus = RabbitJump.bittenMC;
        }

        if (rabbitTimePassed <= 4f && biteStatus)
        {
            rabbitTimePassed += Time.deltaTime;
            return NodeStatus.SUCCESS;
        }
        else
        {
            rabbitTimePassed = 0f;
            biteStatus = false;
            return NodeStatus.FAILURE;
        }
    }

    NodeStatus runFromRabbit()
    {
        McRunsFromAvatar(NpcInstantiator.rabbitPos);

        return NodeStatus.SUCCESS;
    }

    NodeStatus checkMeanThrow()
    {
        if (CheckDist(transform.position, NpcInstantiator.ballKidPos) && BallProjectile.meanBallThrown && gotHit)
        {
            //Debug.Log("got hit yo");
            ballStatus = gotHit;
        }

        if (ballTimePassed <= 4f && ballStatus)
        {
            ballTimePassed += Time.deltaTime;
            return NodeStatus.SUCCESS;
        }
        else
        {
            ballTimePassed = 0f;
            ballStatus = false;
            return NodeStatus.FAILURE;
        }
    }

    NodeStatus runFromBallKid()
    {
        McRunsFromAvatar(NpcInstantiator.ballKidPos);

        return NodeStatus.SUCCESS;
    }

    NodeStatus checkExist()
    {
        if(IterationController.leastUsed == 2)
        {
            return NodeStatus.FAILURE;
        }

        return NodeStatus.SUCCESS;
    }

    NodeStatus checkCatch()
    {
        if (playedCatch && !stillInterested)
        {
            interestTimer = 0f;
            updateInterest();
        }

        return NodeStatus.SUCCESS;
    }

    NodeStatus checkInterest()
    {
        //Check if MC is still interested
        updateInterest();

        if(interestTimer <= interestTime && stillInterested && !RabbitJump.beingCarried && CheckDist(transform.position, NpcInstantiator.ballKidPos))
        {
            Debug.Log("Am interested");
            if (playedCatch)
            {
                interestTimer = 0f;
                playedCatch = false;
            }
            else
            {
                interestTimer += Time.deltaTime;
            }

            return NodeStatus.SUCCESS;
        }
        else
        {
            return NodeStatus.FAILURE;
        }
    }

    NodeStatus moveToBallKid()
    {
        McGoesToAvatar(NpcInstantiator.ballKidPos);

        return NodeStatus.SUCCESS;
    }

    NodeStatus checkMusic()
    {
        if (MusicKidBT.musicListener == "MC" && MusicKidBT.currentMood == 0)
        {
            Debug.Log("Listening to music");
            anim.SetBool("isWalking", false);
            anim.SetBool("wantToPlay", false);
            anim.SetBool("isHappySong", true);
            return NodeStatus.SUCCESS;
        }
        else
        {
            anim.SetBool("isHappySong", false);
            return NodeStatus.FAILURE;
        }
    }

    NodeStatus updateMC()
    {
        //FlipAssetDirection();
        AnimationMoodCheck();

        return NodeStatus.FAILURE;
    }
}
