using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialProcControl : MonoBehaviour
{
    public GameObject bed;
    public GameObject mc;
    private Animator animForBed;

    public GameObject pointLight;
    public GameObject directionalLight;

    private bool mcWokeUp = false;
    private float mcAppearWaitTime = 4f; // Play around with the value for fade in/out
    // Start is called before the first frame update
    void Start()
    {
        animForBed = bed.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TutorialCharSwitch.WakeActionChosen && !mcWokeUp)
        {
            if (TutorialCharSwitch.TutCharChoice == 2)
            {
                directionalLight.SetActive(true);
                pointLight.GetComponent<Light>().intensity = 1;
            }
            animForBed.SetTrigger("wakeuplo");
            mcWokeUp = true;
            Invoke("MagicMcAppear", mcAppearWaitTime);
        }
    }

    private void MagicMcAppear()
    {
        mc.SetActive(true);
    }
}
