using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The selector runs until it finds any child that evaluates to SUCCESS.
//Will return FAILURE only if each child evaluates to FAILURE
public class Selector : Node
{
    //Protect list of children so that it can't be modified inadvertently
    protected List<Node> children = new List<Node>();

    //Set the list of children to one passed to the constructor
    public Selector(List<Node> childList)
    {
        children.AddRange(childList);
    }

    //We are looking for the first one to evaluate to SUCCESS otherwise report FAILURE
    public override NodeStatus Evaluate()
    {
        bool anyChildRunning = false;

        foreach(Node node in children)
        {
            switch (node.Evaluate())
            {
                case NodeStatus.SUCCESS:
                    currentStatus = NodeStatus.SUCCESS;
                    return currentStatus;
                case NodeStatus.RUNNING:
                    anyChildRunning = true;
                    continue;
                case NodeStatus.FAILURE:
                    continue;
                default:
                    Debug.Log("Bad value in Selector");
                    currentStatus = NodeStatus.FAILURE;
                    return currentStatus;
            }
        }

        currentStatus = anyChildRunning ? NodeStatus.RUNNING : NodeStatus.FAILURE;

        return currentStatus;
    }
}
