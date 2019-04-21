using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInstantiator : MonoBehaviour
{
    //private int npcCount = 1;

    private int runnerCount = 2;    //0
    private int lonerCount = 1;     //1
    private int rcCount = 1;        //2
    private int bpCount = 1;        //3
    private string rename;
    
    public GameObject[] NPCs; //contains the 3 types of NPCs
    public GameObject[] groupies;
    public GameObject groupiePrefab;
    private int groupCount = 3;
    private int num;

    public GameObject sadFace; //for reactions to avatars
    public GameObject madFace;
    public GameObject happyFace;
    public GameObject surpriseFace;
    public GameObject hurtFace;
    public GameObject groovinFace;

    public GameObject ballKid;  //to get the position of these guys
    public static Vector3 ballKidPos;
    public GameObject rabbit;
    public static Vector3 rabbitPos;
    public GameObject musicKid;
    public static Vector3 musicKidPos;
    public Vector3 target;

    public GameObject rightBound;

    public static Vector3 center;
    public Vector3[] groupiePos;

    private List<GameObject> npcPositions;
    int origLayer;

    // Start is called before the first frame update
    void Start()
    {
        npcPositions = new List<GameObject>();
        int ranX = Random.Range((int)Playground.LeftX, (int)Playground.RightX);
        int ranY = Random.Range((int)Playground.LowerY, (int)Playground.UpperY);
        center = new Vector3(ranX, ranY, -1);
        center = checkDist(center);
        groupiePos = new Vector3[groupCount];
        createNPCs(0, runnerCount, ranX, ranY);
        createNPCs(1, lonerCount, ranX, ranY);
        createNPCs(2, rcCount, ranX, ranY);
        createNPCs(3, bpCount, ranX, ranY);
        NPCs[0].name = "Runner"; NPCs[1].name = "Loner"; NPCs[2].name = "RabbitChaser"; NPCs[3].name = "BallPlayers";
        groupies = new GameObject[groupCount];
        for (int i = 0; i < groupCount; i++)
        {
            ranX = Random.Range((int)Playground.LeftX, (int)Playground.RightX);
            ranY = Random.Range((int)Playground.LowerY, (int)Playground.UpperY);
            Vector3 pos = new Vector3(ranX, ranY, -1);
            groupies[i] = Instantiate(groupiePrefab, pos, Quaternion.identity) as GameObject;
            npcPositions.Add(groupies[i]);
            groupies[i].GetComponent<SpriteRenderer>().sortingLayerName = "Main";
            groupies[i].GetComponent<Groupies>().target = RandomCircle(center, 3f, groupCount, i);
            groupies[i].name = groupies[i].name + i;


        }
        origLayer = npcPositions[0].layer;
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

    Vector3 checkDist(Vector3 pos)  //to make it so that the groupies don't circle on top of the avatars
    {
        float dist1 = Vector3.Distance(ballKid.transform.position, pos);
        float dist2 = Vector3.Distance(rabbit.transform.position, pos);
        float dist3 = Vector3.Distance(musicKid.transform.position, pos);
        if (dist1 < 10.0f || dist2 < 10.0f || dist3 < 10.0f)
        {
            int ranX = Random.Range((int)Playground.LeftX, (int)Playground.RightX);
            int ranY = Random.Range((int)Playground.LowerY, (int)Playground.UpperY);
            Vector3 pos2 = new Vector3(ranX, ranY, -1);
            pos = checkDist(pos2);
        }
        return pos;
    }

    private void createNPCs(int choice, int count, int ranX, int ranY)
    {
        rename = NPCs[choice].name;
        for (int i = 0; i < count; i++)
        {
            //NPCs[choice].name = rename;
            ranX = Random.Range((int)Playground.LeftX, (int)Playground.RightX);
            ranY = Random.Range((int)Playground.LowerY, (int)Playground.UpperY);
            Vector3 pos = new Vector3(ranX, ranY, -1);
            Quaternion rot = new Quaternion(0, 0, 0, 0);
            GameObject temp = Instantiate(NPCs[choice], pos, rot);
            npcPositions.Add(temp);
            NPCs[choice].GetComponent<SpriteRenderer>().sortingLayerName = "Main";
            //string rename = NPCs[choice].name + i;
            NPCs[choice].name = rename + i;
        }        
    }

    private void checkPositions()
    {
        //int temp = npcPositions[0].layer;
        for (int i = 0; i < npcPositions.Count - 1; i++)
        {
            for (int j = 1; j < npcPositions.Count; j++)
            {
                //if (npcPositions[i].)
                Vector3 pos1 = npcPositions[i].transform.position;
                Vector3 pos2 = npcPositions[j].transform.position;
                bool checkXPos = checkXPosition(pos1.x, pos2.x);
                bool checkYPos = checkYPosition(pos1.y, pos2.y);
                
                if (checkXPos && checkYPos)
                {
                    npcPositions[i].layer = 31;
                    //Debug.Log("should be 31 on layer " + npcPositions[i].layer);
                }
                else
                {
                    //npcPositions[i].layer = temp;
                    npcPositions[i].layer = origLayer;
                    //Debug.Log("should be whatever on layer " + npcPositions[i].layer);
                }     
            }
        }
    }

    private bool checkXPosition(float x1, float x2)
    {
        float distance = Mathf.Abs(x1) - Mathf.Abs(x2);
        if (Mathf.Abs(distance) < 2f)
        {
            return true;
        }
        return false;
    } 

    private bool checkYPosition(float y1, float y2)
    {
        float distance = Mathf.Abs(y1) - Mathf.Abs(y2);
        if (Mathf.Abs(distance) < 2f)
        {
            return true;
        }
        return false;
    }

    private void Update()
    {
        rabbitPos = rabbit.transform.position;
        ballKidPos = ballKid.transform.position;
        if (characterSwitcher.isMusicGuyInCharge)
        {
            musicKidPos = musicKid.transform.position;
        }
        checkPositions();
    }


}
