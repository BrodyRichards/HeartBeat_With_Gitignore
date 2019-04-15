using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : Node
{
    //This is the child node that the inverter wraps
    private Node child;

    //This is an accessor so that you can get the child node from the inverter
    //If you want to access it call it by node, e.g. myInverter.node
    public Node node
    {
        get
        {
            return child;
        }
    }

    //Requires that the child node for inversion be passed to constructor
    public Inverter(Node n)
    {
        child = n;
    }

    //On SUCCESS return FAILURE
    //On FAILURE return SUCCESS
    //On RUNNING return RUNNING
    public override NodeStatus Evaluate()
    {
        switch (child.Evaluate())
        {
            case NodeStatus.SUCCESS:
                currentStatus = NodeStatus.FAILURE;
                return currentStatus;
            case NodeStatus.FAILURE:
                currentStatus = NodeStatus.SUCCESS;
                return currentStatus;
            case NodeStatus.RUNNING:
                currentStatus = NodeStatus.RUNNING;
                return currentStatus;
            default:
                Debug.Log("Bad value in Inverter");
                return NodeStatus.FAILURE;
        }
    }
}
