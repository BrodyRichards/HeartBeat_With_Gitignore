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
        //Increment day count
        leastUsed = FindLeastPlayed();
        //Convert int to string for GameObject lookup
        string lUsed = leastUsed.ToString();
        //Find GameObject
        GameObject leastPlayed = GameObject.Find(lUsed);
        //Disable in hierarchy
        //leastPlayed.SetActive(false);
        leastPlayed.transform.localScale = new Vector3(0, 0, 0);
        Debug.Log("Disabled: " + leastUsed);
    }

    private int FindLeastPlayed()
    {
        int leastIndex = 0;
        float currLowest = 0f;
        float[] avArray = { bunnyTimer, ballKidTimer, musicKidTimer };

        for(int i = 0; i < 3; i++)
        {
            if (currLowest <= 0f)
            {
                currLowest = avArray[i];
            }

            if(avArray[i] < currLowest)
            {
                currLowest = avArray[i];
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
