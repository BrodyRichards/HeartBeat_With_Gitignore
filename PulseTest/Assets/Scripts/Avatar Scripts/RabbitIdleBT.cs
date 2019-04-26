using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitIdleBT : MonoBehaviour
{
    Node RabbitTree;

    private float idleTimer;
    private float walkTimer;
    private bool directionChosen;
    private int ranDir;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        RabbitTree = CreateRabbitBT();
    }

    // Update is called once per frame
    void Update()
    {
        RabbitTree.Evaluate();
    }

    Node CreateRabbitBT()
    {
        Leaf CheckControl = new Leaf(checkControl);
        Leaf RunAround = new Leaf(runAround);

        Sequence rabbitBT = createSeqRoot(CheckControl, RunAround);

        return rabbitBT;
    }

    /*---------------------Helper Functions---------------------*/

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

    private void idleWalk()
    {
        if ((transform.position.x > Playground.RightX) ||
            (transform.position.x < Playground.LeftX)  ||
            (transform.position.y > Playground.UpperY) ||
            (transform.position.y < Playground.LowerY))
        {
            //Hit the bound
            if(transform.position.x > Playground.RightX)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }else if (transform.position.x < Playground.LeftX)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }else if(transform.position.y > Playground.UpperY)
            {
                transform.Translate(Vector2.down * speed * Time.deltaTime);
            }else if (transform.position.y < Playground.LowerY)
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            }
        }
        else
        {
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            else if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }

            //Pick a direction and walk in it for 1 second
            if (!directionChosen)
            {
                Debug.Log("Choosing");
                ranDir = Random.Range(1, 4);
                directionChosen = true;
            }
            else
            {
                if(ranDir == 1)
                {
                    //Walk up
                    transform.Translate(Vector2.up * speed * Time.deltaTime);
                }else if (ranDir == 2)
                {
                    //Walk right
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                }else if (ranDir == 3)
                {
                    //Walk down
                    transform.Translate(Vector2.down * speed * Time.deltaTime);
                }else if (ranDir == 4)
                {
                    //Walk left
                    transform.Translate(Vector2.left * speed * Time.deltaTime);
                }
            }
        }
    }

    NodeStatus checkControl()
    {
        if (characterSwitcher.charChoice == (int)Control.toRabbit)
        {
            return NodeStatus.FAILURE;
        }
        else
        {
            return NodeStatus.SUCCESS;
        }
    }

    NodeStatus runAround()
    {
        Debug.Log("Rabbit can run");
        //Debug.Log("Idle " + idleTimer);
        //Debug.Log("Walk " + walkTimer);
        Debug.Log("Direction " + ranDir);

        //Every five seconds pick a direction and walk for 1 second
        if (idleTimer >= 5f)
        {
            if (walkTimer <= 1f)
            {
                idleWalk();
                walkTimer += Time.deltaTime;
            }
            else
            {
                walkTimer = 0f;
                idleTimer = 0f;
                directionChosen = false;
            }
        }
        else
        {
            idleTimer += Time.deltaTime;
        }

        return NodeStatus.SUCCESS;
    }
}
