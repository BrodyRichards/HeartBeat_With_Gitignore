using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : Node
{
    //This is the definition for the type of function we are passing to the 
    //node's delegate. In this case we want it to be of type NodeStatus so
    //that it will return the NodeStatus type
    public delegate NodeStatus LeafNodeDelegate();

    //This is the function pointer that will hold the pointer to the function
    //that we want the delegate to represent. Basically, this is holding the
    //function that we want to evaluate. This allows us to pass whatever function
    //we want to the delegate.
    private LeafNodeDelegate m_LeafMethod;

    //The Leaf node constructor requires that you pass a function to it so that
    //the delegate can be set to the function that you pass to it. 
    //Example call: Leaf newLeaf = new Leaf(Action whateverMethodYouWantHere);
    public Leaf(LeafNodeDelegate PassedFunc)
    {
        m_LeafMethod = PassedFunc;
    }

    public override NodeStatus Evaluate()
    {
        //A bit redundant to use the switch to return the status of the node
        //but we want to use it so we get that default in case an unexpected
        //value is returned. The default for undefined behavior is FAILURE
        switch (m_LeafMethod())
        {
            case NodeStatus.SUCCESS:
                currentStatus = NodeStatus.SUCCESS;
                return currentStatus;
            case NodeStatus.FAILURE:
                currentStatus = NodeStatus.FAILURE;
                return currentStatus;
            case NodeStatus.RUNNING:
                currentStatus = NodeStatus.RUNNING;
                return currentStatus;
            default:
                currentStatus = NodeStatus.FAILURE;
                return currentStatus;
        }
    }
}
