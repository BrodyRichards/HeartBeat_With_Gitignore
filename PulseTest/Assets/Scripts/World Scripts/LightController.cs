using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light light;


    public Color blue = new Color(0.0f, 0.1f, 0.9f);
    public Color yellow = new Color(1f, 0.2844f, 1f);
    public Color white = new Color(1f, 1f, 1f);
    public int mood = 4;
    // Start is called before the first frame update
    void Start()
    {
        light.color = new Color(1f, 1f, 1f);
        light.range = 10f;
        light.intensity = 1f;
        //light.transform.position = Vector2()
    }

    // Update is called once per frame
    void Update()
    {
        if (mood < 3 && mood > -3)
        {
            light.color = white;
            Radiate();
        }
        else if (mood > 3)
        {
            
            light.color = blue;
            Radiate();
            
        }
        else
        {
            light.color = yellow;
            Radiate();
        }
        
        
    }

    public void Radiate()
    {
        light.range += 0.5f;
        if (light.intensity < 2f)
        {
            light.intensity += 0.1f;
        }
    }
}
