using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Instantiator : MonoBehaviour
{
    private int npcCount = 20;
    
    public GameObject[] NPCs; //contains the 3 types of NPCs
    public GameObject[] groupies;
    public GameObject groupiePrefab;
    private int groupCount = 10;
    private int num;

    public GameObject area;   //quad
    private int areaX, areaY; //get the size of the quad
    public int neighbourDistance = 50;
    public float maxForce = 0.5f;
    public float maxVelocity = 1.0f;
    public Vector2 target;
    // Start is called before the first frame update
    void Start()
    {
        areaX = ((int)area.transform.localScale.x) / 2;
        areaY = ((int)area.transform.localScale.y) / 2;

        for (int i = 0; i < npcCount; i++) //create and instantiate the npcs (we can make it more complicated later)
        {
            int choice = Random.Range(0, 2);
            int ranX = Random.Range(-areaX, areaX);
            int ranY = Random.Range(-areaY, areaY);
            target = new Vector2(ranX, ranY);
            Vector3 pos = new Vector3(ranX, ranY, -1);
            Quaternion rot = new Quaternion(0, 0, 0, 0);
            Instantiate(NPCs[choice], pos, rot);
        }
        groupies = new GameObject[groupCount];
        for (int i = 0; i < groupCount; i++)
        {
            int ranX = Random.Range(-areaX, areaX);
            int ranY = Random.Range(-areaY, areaY);
            Vector3 pos = new Vector3(ranX, ranY, -1);
            groupies[i] = Instantiate(groupiePrefab, pos, Quaternion.identity) as GameObject;
            groupies[i].GetComponent<Groupies>().manager = this.gameObject;
        }
    }
}
