using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarAssets : MonoBehaviour
{
    public GameObject rabAsset;
    public GameObject ballAsset;
    public GameObject musicAsset;

    // Start is called before the first frame update
    void Start()
    {

        if (MentalState.moodLog["Held Rabbit"] > 3)
        {
            rabAsset.gameObject.SetActive(true);
        }
        if (MentalState.moodLog["Played catch"] > 3)
        {
            ballAsset.gameObject.SetActive(true);
        }
        if (MentalState.moodLog["Happy Song"] > 3)
        {
            musicAsset.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
