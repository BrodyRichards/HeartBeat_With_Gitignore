using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullyBT : MonoBehaviour
{
    Node NPC_BT;

    // Start is called before the first frame update
    void Start()
    {
        NPC_BT = createBullyBT();
    }

    // Update is called once per frame
    void Update()
    {
        NPC_BT.Evaluate();
    }

    Node createBullyBT()
    {
        Leaf BasicLeaf = new Leaf(basicLeaf);

        return BasicLeaf;
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

    NodeStatus basicLeaf()
    {
        return NodeStatus.SUCCESS;
    }
}
