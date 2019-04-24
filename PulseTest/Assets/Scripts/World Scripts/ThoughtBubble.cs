using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThoughtBubble : MonoBehaviour
{
    public GameObject mc;
    Vector3 pos;
    Vector3 offset;

    
    // Start is called before the first frame update
    void Start()
    {
        pos = mc.transform.position;
        if ("SampleScene" == SceneManager.GetActiveScene().name)
        {
            offset = new Vector3(-8, 5, 0);
        }
        else
        {
            offset = new Vector3(-3, 2, 0);
        }      
    }

    // Update is called once per frame
    void Update()
    {
        pos = mc.transform.position;
        transform.position = pos + offset;
        
    }
}
