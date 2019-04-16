using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeater : Node
{
    //This is the child node that the inverter wraps
    private Node child;
    private int numRepeat;
    private int counter = 0;

    //This is an accessor so that you can get the child node from the inverter
    //If you want to access it call it by node, e.g. myInverter.node
    public Node node
    {
        get
        {
            return child;
        }
    }

    /*
     * The first parameter is required and will be the child node to wrap
     * The second parameter is optional and will be the number of times to
     * run the repeater. If nothing is passed will default to infinite
     *
     * Example usage:    Repeater myRepeater = new Repeater(childNode, numTimesToRun);
     *                                          OR
     *                   Repeater myRepeater = new Repeater(childNode);
    */
    public Repeater(Node n, int num = -1)
    {
        numRepeat = num;
        child = n;
    }

    //Everytime the Repeater is evaluated we want to reevaluate it's child node
    //which basically reruns the node's functionality infinitely or a set number of times
    public override NodeStatus Evaluate()
    {
        if (numRepeat >= 0)
        {
            child.Evaluate();
            currentStatus = NodeStatus.RUNNING;
            return currentStatus;
        }
        else if(counter > numRepeat)
        {
            counter++;
            child.Evaluate();
        }

        currentStatus = NodeStatus.SUCCESS;
        return currentStatus;
    }
}
