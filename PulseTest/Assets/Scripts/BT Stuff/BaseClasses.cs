using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * REFERENCES:
 *  https://hub.packtpub.com/building-your-own-basic-behavior-tree-tutorial/
 *  https://github.com/GrymmyD/UnityBehaviourTree
 *  http://www.gamasutra.com/blogs/ChrisSimpson/20140717/221339/Behavior_trees_for_AI_How_they_work.php
*/

//Type enumeration for the different statuses of the nodes
//Each node MUST return one of these statuses
public enum NodeStatus
{
    RUNNING = -1,
    FAILURE = 0,
    SUCCESS = 1
};

//Blueprint Node class for each node type: Composite, Decorator, and Leaf nodes all derive from this base class
public abstract class Node
{
    //This virtual function may be overriden in derived classes to allow for the implementation of more specific functionality
    //This function serves as a way to return the Node's NodeStatus
    //This can be used for debugging but I didn't implement it 
    /*public virtual NodeStatus ReturnStatus()
    {

    }*/

    //This is used to hold the current status of the node
    protected NodeStatus currentStatus;

    //An accessor variable to return the current status of the node
    //Returns an enum of type NodeStatus
    //Reference like this: myNode.currentState;
    public NodeStatus currentState
    {
        get
        {
            return currentStatus;
        }
    }

    public Node()
    {
        //Default constructor
    }

    //A function that can be overriden to check the status of the current called Node
    //Override this with the specific behavior desired for the node
    public abstract NodeStatus Evaluate();
}