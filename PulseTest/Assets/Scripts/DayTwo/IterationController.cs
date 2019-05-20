using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IterationController : MonoBehaviour
{
    public static float ballKidTimer;
    public static float bunnyTimer;
    public static float musicKidTimer;
    public static float dayCount;
    public static int leastUsed;
    private GameObject leastPlayed;

    // Start is called before the first frame update
    void Awake()
    {
        if(dayCount < 1)
        {
            leastUsed = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrepareNextDay()
    {
        //If there is one that is least used then we need to alternate which avatar is disabled
        //if (dayCount >= 3)
        //{
        //    //Replace the avatar that is missing
        //    leastPlayed.transform.localScale = new Vector3(1, 1, 1);
        //}

        //Have to keep this array in order
        leastUsed = FindLeastPlayed(bunnyTimer, ballKidTimer, musicKidTimer);
        //Convert int to string for GameObject lookup
        string lUsed = leastUsed.ToString();
        //Find GameObject
        leastPlayed = GameObject.Find(lUsed);
        //Disable in hierarchy
        //leastPlayed.SetActive(false);
        leastPlayed.transform.localScale = new Vector3(0, 0, 0);
        Debug.Log("Disabled: " + leastUsed);
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

                if(currLowest == 0f)
                {
                    currLowest += 0.01f;
                }

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
