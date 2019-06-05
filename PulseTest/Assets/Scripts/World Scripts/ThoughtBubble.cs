using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ThoughtBubble : MonoBehaviour
{
    public GameObject mc;
    public GameObject text;
    Vector3 pos;
    Vector3 offset, offset2, scale, scaleOpp, scaleText, scaleTextOpp;

    
    // Start is called before the first frame update
    void Start()
    {
        //pos = mc.transform.position;
        if ("SampleScene" == SceneManager.GetActiveScene().name)
        {
            pos = mc.transform.position;
            offset = new Vector3(-8, 5, 0);
            offset2 = new Vector3(8, 5, 0);
            scale = transform.localScale;
            scaleOpp = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            scaleText = text.transform.localScale;
            scaleTextOpp = new Vector3(-text.transform.localScale.x, text.transform.localScale.y, text.transform.localScale.z);
        }
        else if ("TutorialScreen" == SceneManager.GetActiveScene().name)//for the tutorial scene
        {
            pos = new Vector3(4.27f, -2.58f, 0);
            offset = new Vector3(0, 3, 0);
        }
        else
        {
            pos = mc.transform.position;
            offset = new Vector3(3, 2, 0);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if ("TutorialScreen" != SceneManager.GetActiveScene().name)
        {
            pos = mc.transform.position;
        }
        transform.position = pos + offset;
        
        if ("SampleScene" == SceneManager.GetActiveScene().name)
        {
            
            if (Vector3.Distance(transform.position, GameObject.Find("LeftBound").transform.position) <= 30)
            {
                transform.localScale = scaleOpp;            //make it flip
                text.transform.localScale = scaleTextOpp;
                transform.position = pos + offset2;
            }
            else if (Vector3.Distance(transform.position, GameObject.Find("LeftBound").transform.position) > 30)    //normal thought bubble
            {
                transform.localScale = scale;
                text.transform.localScale = scaleText;
                transform.position = pos + offset;
            }
        }    
    }
}
