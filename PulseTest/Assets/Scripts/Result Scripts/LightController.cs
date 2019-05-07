using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light light;
    public GameObject go;

    public Color blue = new Color(0.0f, 0.1f, 0.9f);
    public Color yellow = new Color(1f, 0.2844f, 1f);
    public Color white = new Color(1f, 1f, 1f);

    public GameObject lightOn;
    public GameObject lightOff;
    private int mood;
    private readonly float turnToNightTimer = 2f;
    private bool isRadiateFinish;
    public static bool isNightGlowFinish;
    // Start is called before the first frame update
    private void Awake()
    {
        isRadiateFinish = false;
        isNightGlowFinish = false;
    }
    void Start()
    {
        mood = MentalState.OverallResult();
        

        //light.transform.position = Vector2()
        if (mood < 5 && mood > -5)
        {
            light.color = white;
            light.range = 20f;
            light.intensity = 2f;

        }
        else if (mood > 5)
        {
            light.color = yellow;
            light.range = 30f;
            light.intensity = 2.5f;
        }
        else
        {
            light.color = blue;
            light.range = 20f;
            light.intensity = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
        
        if (Time.timeSinceLevelLoad > turnToNightTimer)
        {
            NightGlow();
            
            
        }

        if (isNightGlowFinish)
        {
            
            lightOn.SetActive(true);
            lightOff.SetActive(false);

        }

        
        
    }

    private void NightGlow()
    {

        if (light.intensity > 0.01f)
        {
            light.intensity -= 0.01f;
            light.range -= 0.01f;
        }
        else
        {
            isNightGlowFinish = true;
        }
    }


    public void Deem()
    {
        
        if (light.intensity > 0)
        {
            light.intensity -= 0.01f;
            Debug.Log(light.intensity);
            
        }
        
    }
}
