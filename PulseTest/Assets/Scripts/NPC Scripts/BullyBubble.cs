using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullyBubble : MonoBehaviour
{
    private Vector3 pos;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(4, 5, 0);
    }

    // Update is called once per frame
    void Update()
    {
        pos = NpcInstantiator.bullyKidPos;
        transform.position = pos + offset;
    }
}
