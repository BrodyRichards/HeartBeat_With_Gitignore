using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LightController : MonoBehaviour
{
    public Light[] roomLights;
    public Light brightMorningPointLight;
    public Light sunsetDirLight;
    public Light sunsetPointLight;
    public Light darkNightDirLight;
    public Light brightMorningDirLight;

    public Color sunsetColor;
    public Color darkNightColor = new Color(0f, 0.001166861f, 0.2075472f);

    public Color happyColor;
    public Color neutralColor;
    public Color sadColor;

    public GameObject lightOn;
    public GameObject lightOff;
    public GameObject skyCycle;

    [Range(-360f, 120f)]
    public float rotateAngle;

    [Range(-360f, 120f)] public float sunsetAngle;
    [Range(-360f, 120f)] public float nightAngle;
    [Range(-360f, 120f)] public float morningAngle;

    private float turnToDayTimer = 5f;
    private float turnToNightTimer = 5f;

    public static bool nightIsHere;
    public static bool turnOffRoomLights;
    public static bool morningIsHere;
    public static bool timeToGetOutOfBed;

    public bool isReset;

    private void Awake()
    {
        nightIsHere = false;
        turnOffRoomLights = false;
        morningIsHere = false;
        timeToGetOutOfBed = false;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (!nightIsHere)
        {

            turnToNightTimer -= Time.deltaTime;
            RotateSkyTo(sunsetAngle, 0.1f);
            if (turnToNightTimer < 0)
            {

                nightIsHere = ShiftToNightLight();


            }
        }
        else
        {
            DecideRoomLightColor();
        }

        if (turnOffRoomLights)
        {
            foreach(Light l in roomLights)
            {
                l.enabled = false;
                lightOn.SetActive(false);
                lightOff.SetActive(true);
            }

        }

        if (BedtimeProcedure.charlieInBed && !timeToGetOutOfBed)
        {
            RotateSkyTo(morningAngle, 0.17f);
        }

        if (morningIsHere && !timeToGetOutOfBed)
        {

            LetTheSunShine();
        }




        if (isReset)
        {
            darkNightDirLight.intensity = 0f;
            sunsetDirLight.intensity = 1f;
            sunsetPointLight.intensity = 0.5f;
        }


    }

    private bool ShiftToNightLight()
    {
        bool lightIsReady = false;
        if (darkNightDirLight.intensity < 2.2f)
        {
            darkNightDirLight.intensity += 0.002f;
            //darkNightDirLight.intensity += 0.01f / turnToNightTimer;
        }
        else
        {
            lightIsReady = true;
        }

        if (sunsetDirLight.intensity > 0f)
        {
            sunsetDirLight.intensity -= 0.001f;
            //sunsetDirLight.intensity -= 0.005f / turnToNightTimer;
        }
        else
        {
            lightIsReady = lightIsReady && true;
        }



        if (sunsetPointLight.intensity > 0f)
        {
            sunsetPointLight.intensity -= 0.001f;
            //sunsetPointLight.intensity -= 0.005f / turnToNightTimer;
        }
        RotateSkyTo(nightAngle, 0.2f);

        return lightIsReady;

    }

    public void LetTheSunShine()
    {

        if (brightMorningPointLight.intensity < 0.5f)
        {
            brightMorningPointLight.intensity += 0.01f;
        }


        //if (darkNightDirLight.intensity > 0.05f)
        //{
        //    darkNightDirLight.intensity -= 0.01f;
        //}

        if (brightMorningDirLight.intensity < 0.6f)
        {
            brightMorningDirLight.intensity += 0.005f;
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
            l.enabled = true;
            l.color = roomLightColor;
        }
    }

    public void RotateSkyTo(float angle, float rotateSpeed)
    {
        if (rotateAngle > angle)
        {
            rotateAngle -= rotateSpeed;
        }

        skyCycle.transform.rotation = Quaternion.Euler(0f, 0f, rotateAngle);
        Debug.Log("rotate angle" + rotateAngle);
    }
}
