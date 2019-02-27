using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInstantiator : MonoBehaviour
{
    //private int npcCount = 1;

    private int runnerCount = 3;    //0
    private int lonerCount = 3;     //1
    private int rcCount = 1;        //2
    private int bpCount = 1;        //3
    
    public GameObject[] NPCs; //contains the 3 types of NPCs
    public GameObject[] groupies;
    public GameObject groupiePrefab;
    private int groupCount = 3;
    private int num;

    public GameObject sadFace; //for reactions to avatars
    public GameObject madFace;
    public GameObject happyFace;
    public GameObject surpriseFace;

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

    // Start is called before the first frame update
    void Start()
    {
        int ranX = Random.Range((int)Playground.LeftX, (int)Playground.RightX);
        int ranY = Random.Range((int)Playground.LowerY, (int)Playground.UpperY);
        center = new Vector3(ranX, ranY, -1);
        center = checkDist(center);
        groupiePos = new Vector3[groupCount];
        createNPCs(0, runnerCount, ranX, ranY);
        createNPCs(1, lonerCount, ranX, ranY);
        createNPCs(2, rcCount, ranX, ranY);
        createNPCs(3, bpCount, ranX, ranY);

        groupies = new GameObject[groupCount];
        for (int i = 0; i < groupCount; i++)
        {
            ranX = Random.Range((int)Playground.LeftX, (int)Playground.RightX);
            ranY = Random.Range((int)Playground.LowerY, (int)Playground.UpperY);
            Vector3 pos = new Vector3(ranX, ranY, -1);
            groupies[i] = Instantiate(groupiePrefab, pos, Quaternion.identity) as GameObject;
            groupies[i].GetComponent<SpriteRenderer>().sortingLayerName = "Main";
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
        ranX = Random.Range((int)Playground.LeftX, (int)Playground.RightX);
        ranY = Random.Range((int)Playground.LowerY, (int)Playground.UpperY);
        Vector3 pos = new Vector3(ranX, ranY, -1);
        Quaternion rot = new Quaternion(0, 0, 0, 0);
        Instantiate(NPCs[choice], pos, rot);
        NPCs[choice].GetComponent<SpriteRenderer>().sortingLayerName = "Main";
    }

    private void Update()
    {
        rabbitPos = rabbit.transform.position;
        ballKidPos = ballKid.transform.position;
        if (characterSwitcher.isMusicGuyInCharge)
        {
            musicKidPos = musicKid.transform.position;
        }
        
    }


}
