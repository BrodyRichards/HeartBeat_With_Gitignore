using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Succeeder : Node
{
    //This is the child node that the succeeder wraps
    private Node child;

    //This is an accessor so that you can get the child node from the succeeder
    //If you want to access it call it by node, e.g. mySucceeder.node
    public Node node
    {
        get
        {
            return child;
        }
    }

    public Succeeder(Node n)
    {
        child = n;
    }

    //A succeeder only returns SUCCESS or FAILURE once it's child process is finished running and returns a value other than running
    //Use this for things like going to a destination where RUNNING will be returned while en route but SUCCESS or FAILURE will be
    //reported once the destination is reached or if something went wrong along the way
    public override NodeStatus Evaluate()
    {
        if (child.Evaluate() == NodeStatus.RUNNING)
        {
            currentStatus = NodeStatus.RUNNING;
            return currentStatus;
        }

        currentStatus = NodeStatus.SUCCESS;
        return currentStatus;
    }
}
