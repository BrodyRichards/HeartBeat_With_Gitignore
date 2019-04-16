using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Just as a general note: Composites, Decorators, and Leaves can
 * implicitly be converted to type Node, but not vice versa.
 * In other words, you can return any of the above types to an object
 * of type Node but you cannot return something of type Node to an object
 * of type Selector or Inverter
*/

public class BTCreator : MonoBehaviour
{
    Node BehaviorTree;

    // Start is called before the first frame update
    void Start()
    {
        //Sequence Test
        //BehaviorTree = testSeq();

        //Selector Test
        //BehaviorTree = testSel();

        //Inverter Test
        BehaviorTree = testInvert();
    }

    // Update is called once per frame
    void Update()
    {
        BehaviorTree.Evaluate();
        Debug.Log(BehaviorTree.currentState);
    }

    /*------------------Test functions------------------*/

    //Sequence Test
    Node testSeq()
    {
        Leaf successLeaf = new Leaf(successFunc);
        Leaf failLeaf = new Leaf(failFunc);
        Leaf runningLeaf = new Leaf(runningFunc);

        Sequence successSeq = createSeqRoot(successLeaf, successLeaf, successLeaf);
        Sequence failSeq = createSeqRoot(successLeaf, failLeaf, successLeaf);
        Sequence runningSeq = createSeqRoot(successLeaf, runningLeaf, successLeaf);

        //Testing Sequence Node with one leaf - PASSED
        Sequence testSeq = createSeqRoot(successLeaf);

        //Testing Sequence Node with two leaves - PASSED
        //Sequence testSeq = createSeqRoot(successLeaf, successLeaf);

        //Testing Sequence Node with multiple leaves - PASSED
        //Sequence testSeq = createSeqRoot(successLeaf, successLeaf, runningLeaf);

        //Testing Sequence Node with one other Sequence Nodes - PASSED
        //Sequence testSeq = createSeqRoot(successSeq);

        //Testing Sequence Node with two other Sequence Nodes - PASSED
        //Sequence testSeq = createSeqRoot(successSeq, successSeq);

        //Testing Sequence Node for SUCCESS multiple Sequence Nodes - PASSED
        //Sequence testSeq = createSeqRoot(successSeq, successSeq, successSeq, successSeq);

        //Testing Sequence Node for RUNNING multiple Sequence Nodes - PASSED
        //Sequence testSeq = createSeqRoot(successSeq, successSeq, runningSeq, successSeq);

        //Testing Sequence Node for FAILURE with multiple Sequence Nodes - PASSED
        //Sequence testSeq = createSeqRoot(successSeq, successSeq, failSeq, successSeq);

        return testSeq;
    }

    //Selector test
    Selector testSel()
    {
        Leaf successLeaf = new Leaf(successFunc);
        Leaf failLeaf = new Leaf(failFunc);
        Leaf runningLeaf = new Leaf(runningFunc);

        Selector successSel = createSelRoot(successLeaf, successLeaf, successLeaf);
        Selector failSel = createSelRoot(failLeaf, failLeaf, failLeaf);
        Selector runningSel = createSelRoot(runningLeaf, runningLeaf, runningLeaf);

        //Testing Selector Node with one leaf - PASSED
        Selector testSel = createSelRoot(successLeaf);

        //Testing Selector Node with multiple leaves - PASSED
        //Selector testSel = createSelRoot(successLeaf, successLeaf, runningLeaf);

        //Testing Selector Node with all RUNNING leaves - PASSED
        //Selector testSel = createSelRoot(runningLeaf, runningLeaf, runningLeaf);

        //Testing Selector Node with all FAILURE leaves - PASSED
        //Selector testSel = createSelRoot(failLeaf, failLeaf, failLeaf);

        //Testing Selector Node with one other Selector Nodes - PASSED
        //Selector testSel = createSelRoot(successSel);

        //Testing Selector Node with two other Sequence Nodes - PASSED
        //Selector testSel = createSelRoot(successSel, successSel);

        //Testing Selector Node for FAILURE Nodes - PASSED
        //Selector testSel = createSelRoot(failSel, failSel, failSel, failSel);

        //Testing Selector Node for RUNNING multiple Selector Nodes - PASSED
        //Selector testSel = createSelRoot(runningSel, runningSel, runningSel, runningSel);

        return testSel;
    }

    //Inverter test
    Node testInvert()
    {
        Leaf successLeaf = new Leaf(successFunc);
        Leaf failLeaf = new Leaf(failFunc);
        Leaf runningLeaf = new Leaf(runningFunc);

        Sequence successSeq = createSeqRoot(successLeaf, successLeaf, successLeaf);
        Sequence failSeq = createSeqRoot(successLeaf, failLeaf, successLeaf);
        Sequence runningSeq = createSeqRoot(successLeaf, runningLeaf, successLeaf);

        //Testing Inverter with SUCCESS inversion - PASSED
        Inverter testInv = new Inverter(successLeaf);

        //Testing Inverter with FAILURE inversion - PASSED
        //Inverter testInv = new Inverter(failLeaf);

        //Testing Inverter with FAILURE inversion - PASSED
        //Inverter testInv = new Inverter(runningLeaf);

        //Testing Inverter with SUCCESS inversion on Composite - PASSED
        //Inverter testInv = new Inverter(successSeq);

        //Testing Inverter with another Inverter - PASSED
        //Inverter childInv = new Inverter(successLeaf);
        //Inverter testInv = new Inverter(childInv);

        return testInv;
    }

    /*------------------Helper functions------------------*/

    Sequence createSeqRoot(params Node[] nodeList)
    {
        List<Node> rootOrder = new List<Node>();

        foreach(Node n in nodeList)
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

    /*------------------Leaf functions------------------*/

    NodeStatus successFunc()
    {
        Debug.Log("Succeeded");

        return NodeStatus.SUCCESS;
    }

    NodeStatus failFunc()
    {
        Debug.Log("Failed");

        return NodeStatus.FAILURE;
    }

    NodeStatus runningFunc()
    {
        Debug.Log("Running");

        return NodeStatus.RUNNING;
    }
}
