using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Instantiator : MonoBehaviour
{
    private int npcCount = 20;

    public GameObject npcObj;
    public GameObject loner;
    public GameObject area;   //quad
    private int areaX, areaY; //get the size of the quad
    // Start is called before the first frame update
    void Start()
    {
        areaX = ((int)area.transform.localScale.x) / 2;
        areaY = ((int)area.transform.localScale.y) / 2;

        for (int i = 0; i < npcCount; i++) //create and instantiate the npcs (we can make it more complicated later)
        {
            int ranX = Random.Range(-areaX, areaX);
            int ranY = Random.Range(-areaY, areaY);
            Vector3 pos = new Vector3(ranX, ranY, -1);
            Quaternion rot = new Quaternion(0, 0, 0, 0);
            if (i % 2 == 0)
            {
                Instantiate(npcObj, pos, rot);
            }
            else
            {
                Instantiate(loner, pos, rot);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
