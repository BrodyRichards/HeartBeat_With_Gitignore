using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThoughtBubble : MonoBehaviour
{
    public GameObject mc;
    Vector3 pos;
    Vector3 offset;

    
    // Start is called before the first frame update
    void Start()
    {
        pos = mc.transform.position;
        offset = new Vector3(5, 2, 0);

        
    }

    // Update is called once per frame
    void Update()
    {
        pos = mc.transform.position;
        transform.position = pos + offset;
        
    }
}
