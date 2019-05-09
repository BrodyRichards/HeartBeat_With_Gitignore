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

    public Color blue = new Color(0.0f, 0.1f, 0.9f);
    public Color yellow = new Color(1f, 0.2844f, 1f);
    public Color white = new Color(1f, 1f, 1f);

    public GameObject lightOn;
    public GameObject lightOff;

    private float turnToDayTimer = 3f;

    public static bool turnOffRoomLights;
    public static bool morningIsHere;

    private void Awake()
    {
        turnOffRoomLights = false;
        morningIsHere = false;
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
            }
        }

        if (morningIsHere)
        {

            LetTheSunShine();

        }



    }


    public void LetTheSunShine()
    {

        if (brightMorningPointLight.intensity < 1f)
        {
            brightMorningPointLight.intensity += 0.002f;
        }

        //if (darkNightDirLight.intensity > 0.05f)
        //{
        //    darkNightDirLight.intensity -= 0.01f;
        //}

        if (brightMorningDirLight.intensity < 0.5f)
        {
            brightMorningDirLight.intensity += 0.001f;
        }
    }

    public void DecideRoomLightColor()
    {
        int mood = MentalState.OverallResult();
        Color roomLightColor;

        if (mood < 5 && mood > -5)
        {
            roomLightColor = white;

        }
        else if (mood > 5)
        {
            roomLightColor = yellow;
        }
        else
        {
            roomLightColor = blue;
        }


        foreach (Light l in roomLights)
        {
            l.color = roomLightColor;
        }
    }
}
