using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//A sequence node evaluates all children in sequence and will return true
//if all children evaluate to true. If any children are still running will
//return RUNNING
public class Sequence : Node
{
    //Protect list of children so that it can't be modified inadvertently
    protected List<Node> children = new List<Node>();

    //Set the list of children to one passed to the constructor
    public Sequence(List<Node> childList)
    {
        children.AddRange(childList);
    }

    //Overriding Evaluate() to check each child for its status then 
    //determing status of the sequence overall based on children's
    //return statuses.
    public override NodeStatus Evaluate()
    {
        bool anyChildRunning = false;

        foreach(Node node in children)
        {
            switch (node.Evaluate())
            {
                case NodeStatus.FAILURE:
                    currentStatus = NodeStatus.FAILURE;
                    return currentStatus;
                case NodeStatus.SUCCESS:
                    continue;
                case NodeStatus.RUNNING:
                    anyChildRunning = true;
                    continue;
                default:
                    Debug.Log("Bad value in Sequence");
                    currentStatus = NodeStatus.FAILURE;
                    return currentStatus;
            }
        }

        //If any are still running then return RUNNING else return SUCCESS
        currentStatus = anyChildRunning ? NodeStatus.RUNNING : NodeStatus.SUCCESS;

        return currentStatus;
    }
}
