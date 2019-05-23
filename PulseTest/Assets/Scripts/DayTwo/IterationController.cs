using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IterationController : MonoBehaviour
{
    public static float ballKidTimer;
    public static float bunnyTimer;
    public static float musicKidTimer;
    public static int dayCount;
    public static int leastUsed;
    private GameObject leastPlayed;
    private Vector3 ballKidScale;
    private Vector3 bunnyScale;
    private Vector3 musicKidScale; 

    // Start is called before the first frame update
    void Awake()
    {
        if(dayCount < 1)
        {
            leastUsed = -1;
            bunnyScale = GameObject.Find("1").transform.localScale;
            ballKidScale = GameObject.Find("2").transform.localScale;
            musicKidScale = GameObject.Find("3").transform.localScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Function for prepping the second day
    public void PrepareNextDay()
    {
        //Have to keep this array in order
        leastUsed = FindLeastPlayed(bunnyTimer, ballKidTimer, musicKidTimer);
        //Convert int to string for GameObject lookup
        string lUsed = leastUsed.ToString();
        //Debug.Log(lUsed);
        //Find GameObject
        leastPlayed = GameObject.Find(lUsed);
        //Disable in hierarchy
        //leastPlayed.SetActive(false);
        leastPlayed.transform.localScale = new Vector3(0, 0, 0);
        Debug.Log("Disabled: " + leastUsed);
    }

    //Function for prepping the final day
    public void PrepareFinalDay()
    {
        string lUsed;
        GameObject lastDropped;
        Vector3 lastDroppedScale;
        string lastLeast = leastUsed.ToString();

        switch (leastUsed)
        {
            case 1:
                lastDroppedScale = bunnyScale;
                leastUsed = FindLeastPlayed(ballKidTimer, musicKidTimer);
                break;
            case 2:
                lastDroppedScale = ballKidScale;
                leastUsed = FindLeastPlayed(bunnyTimer, musicKidTimer);
                break;
            case 3:
                lastDroppedScale = musicKidScale;
                leastUsed = FindLeastPlayed(bunnyTimer, ballKidTimer);
                break;
            default:
                lastDroppedScale = new Vector3(0, 0, 0);
                leastUsed = -1;
                break;
        }

        if(leastUsed > 0)
        {
            lUsed = leastUsed.ToString();
        }
        else
        {
            lUsed = null;
        }

        //Find the last avatar that was dropped out
        //and find the one that's going to be dropped next

        //Last one dropped that's coming back in
        lastDropped = GameObject.Find(lastLeast);
        //Next one getting dropped
        leastPlayed = GameObject.Find(lUsed);

        //Bring last one dropped back in
        lastDropped.transform.localScale = lastDroppedScale;

        //Drop the next one
        leastPlayed.transform.localScale = new Vector3(0, 0, 0);
    }

    private int FindLeastPlayed(params float[] avTimes)
    {
        int leastIndex = 0;
        float currLowest = 0f;
        //float[] avArray = { bunnyTimer, ballKidTimer, musicKidTimer };

        for(int i = 0; i < avTimes.Length; i++)
        {
            if (currLowest <= 0f)
            {
                currLowest = avTimes[i];
                leastIndex = i + 1;
            }

            if(avTimes[i] < currLowest)
            {
                currLowest = avTimes[i];
                leastIndex = i + 1;
            }
        }

        if(leastIndex != 0)
        {
            return leastIndex;
        }
        else
        {
            return -1;
        }
    }
}
