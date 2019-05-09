using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light[] roomLights;
    public Light brightMorningPointLight;
    public Light darkNightDirLight;
    public Light brightMorningDirLight;

    public Color darkNightColor = new Color(0f, 0.001166861f, 0.2075472f);

    public Color happyColor;
    public Color neutralColor;
    public Color sadColor;

    public GameObject lightOn;
    public GameObject lightOff;

    private float turnToDayTimer = 3f;

    public static bool turnOffRoomLights;
    public static bool morningIsHere;
    public static bool timeToGetOutOfBed;

    private void Awake()
    {
        turnOffRoomLights = false;
        morningIsHere = false;
        timeToGetOutOfBed = false;
    }
    void Start()
    {

        DecideRoomLightColor();

    }

    // Update is called once per frame
    void Update()
    {
        
        
        
        if (turnOffRoomLights)
        {
            foreach(Light l in roomLights)
            {
                l.enabled = false;
                lightOn.SetActive(false);
                lightOff.SetActive(true);
            }
        }

        if (morningIsHere && !timeToGetOutOfBed)
        {

            LetTheSunShine();

        }



    }


    public void LetTheSunShine()
    {

        if (brightMorningPointLight.intensity < 0.5f)
        {
            brightMorningPointLight.intensity += 0.002f;
        }


        //if (darkNightDirLight.intensity > 0.05f)
        //{
        //    darkNightDirLight.intensity -= 0.01f;
        //}

        if (brightMorningDirLight.intensity < 0.6f)
        {
            brightMorningDirLight.intensity += 0.001f;
        }
        else
        {
            timeToGetOutOfBed = true;
        }

    }

    public void DecideRoomLightColor()
    {
        int mood = MentalState.OverallResult();
        Color roomLightColor;
        lightOn.SetActive(true);
        lightOff.SetActive(false);

        if (mood < 5 && mood > -5)
        {
            roomLightColor = neutralColor;

        }
        else if (mood > 5)
        {
            roomLightColor = happyColor;
        }
        else
        {
            roomLightColor = sadColor;
        }


        foreach (Light l in roomLights)
        {
            l.color = roomLightColor;
        }
    }
}
