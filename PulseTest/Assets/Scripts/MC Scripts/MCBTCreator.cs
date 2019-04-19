using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCBTCreator : MonoBehaviour
{
    public Animator anim;

    public List<Vector2> mcWaypoints;
    public List<Vector2> refPoints;

    public float followDist;
    public float walkSpeed;

    private bool isFlipped;
    private float lastX;
    private float timePassed;

    public static bool gotHit = false;

    Node MC_BT;

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("isWalking", false);

        isFlipped = false;
        followDist = 20f;
        timePassed = 0f;

        MC_BT = createBehaviorTree();
        refPoints = new List<Vector2>(mcWaypoints);
    }

    // Update is called once per frame
    void Update()
    {
        MC_BT.Evaluate();
    }

    /*-------------------Tree Creation--------------------*/

    Node createBehaviorTree()
    {
        Leaf Walk = new Leaf(GoToWaypoints);

        //Create Rabbit Bite Check Sequence
        //Create Ball Kid mean ball check Sequence
        //Create Ball kid nearby follow him Sequence

        return Walk;
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

    private void McRunsFromAvatar()
    {
        anim.SetBool("isWalking", true);
        anim.SetBool("wantToPlay", false);

        if (gotHit)
        {
            gotHit = !gotHit;
        }

        if(timePassed <= 4f)
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
            }
        }
        else
        {
            timePassed = 0f;
        }
    }

    /*------------------Leaf functions------------------*/

    NodeStatus GoToWaypoints()
    {
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
}
