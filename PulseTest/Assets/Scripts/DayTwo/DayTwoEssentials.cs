using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTwoEssentials : MonoBehaviour
{
    public GameObject rabbit;
    public GameObject ballKid;
    public GameObject musicKid;
    // Start is called before the first frame update
    void Start()
    {
        if (FlipJournal.lastAvatar == 1) { rabbit.SetActive(false); }
        else if (FlipJournal.lastAvatar == 2) { ballKid.SetActive(false); }
        else if (FlipJournal.lastAvatar == 3) { musicKid.SetActive(false); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
