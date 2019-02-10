using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortRender : MonoBehaviour
{
    
    
    public int offset = 0;
    [SerializeField]
    private bool destroy = false;
    private int depth = 500;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        sr.sortingOrder = (int)(depth - transform.position.y - offset);
        if (destroy)
        {
            Destroy(this);
        }
    }
}

// https://unitycodemonkey.com/video.php?v=CTf0WjhfBx8
//this save my life, thank you code monkey!!