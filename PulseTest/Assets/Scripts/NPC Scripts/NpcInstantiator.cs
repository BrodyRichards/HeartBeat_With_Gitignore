using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInstantiator : MonoBehaviour
{
    private int npcCount = 8;
    
    public GameObject[] NPCs; //contains the 3 types of NPCs
    public GameObject[] groupies;
    public GameObject groupiePrefab;
    private int groupCount = 8;
    private int num;

    public GameObject sadFace;
    public GameObject madFace;
    public GameObject happyFace;

    public GameObject area;   //quad
    private int areaX, areaY; //get the size of the quad
    public int neighbourDistance = 100;
    public float maxForce = 0.5f;
    public float maxVelocity = 2.0f;
    public Vector3 target;

    private Vector3 center;
    public Vector3[] groupiePos;
    // Start is called before the first frame update
    void Start()
    {
        areaX = ((int)area.transform.localScale.x) / 2 - 1;
        areaY = ((int)area.transform.localScale.y) / 2 - 1;
        int ranX = Random.Range(-areaX, areaX);
        int ranY = Random.Range(-areaY, areaY);
        center = new Vector3(ranX, ranY, -1);
        groupiePos = new Vector3[groupCount];
        for (int i = 0; i < npcCount; i++) //create and instantiate the npcs (we can make it more complicated later)
        {
            int choice = Random.Range(0, 2);
            ranX = Random.Range(-areaX, areaX);
            ranY = Random.Range(-areaY, areaY);
            Vector3 pos = new Vector3(ranX, ranY, -1);
            Quaternion rot = new Quaternion(0, 0, 0, 0);
            Instantiate(NPCs[choice], pos, rot);
        }
        groupies = new GameObject[groupCount];
        for (int i = 0; i < groupCount; i++)
        {
            ranX = Random.Range(-areaX, areaX);
            ranY = Random.Range(-areaY, areaY);
            Vector3 pos = new Vector3(ranX, ranY, -1);
            groupies[i] = Instantiate(groupiePrefab, pos, Quaternion.identity) as GameObject;

            groupies[i].GetComponent<Groupies>().manager = this.gameObject;
            groupies[i].GetComponent<Groupies>().target = RandomCircle(center, 3f, groupCount, i);

        }
    }

    Vector3 RandomCircle(Vector3 center, float radius, int ranNum, int num)
    {
        float deg = (float)(num + 1)/ranNum;
        float ang = deg * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }
}
