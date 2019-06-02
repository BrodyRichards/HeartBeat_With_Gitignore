using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalResultDecision : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        int friend = MentalState.DecideFriend();


        if (friend != 0)
        {
            GameObject.Find(friend.ToString()).GetComponent<SpriteRenderer>().enabled = true;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
