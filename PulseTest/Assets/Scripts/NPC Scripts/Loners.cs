using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI; may need this in the future??

public class Loners : MonoBehaviour
{
    Vector3 target;
    private GameObject area;   //quad
    private int areaX, areaY; //get the size of the quad
    private float speed = 5f;
    private Vector3 scale;
    private Vector3 scaleOpposite;
    // Start is called before the first frame update
    void Start()
    {
        area = GameObject.Find("Quad");
        areaX = ((int)area.transform.localScale.x) / 2;
        areaY = ((int)area.transform.localScale.y) / 2;
        int ranX = Random.Range(-areaX, areaX);
        int ranY = Random.Range(-areaY, areaY);
        target = new Vector3(ranX, ranY, -1);
        scale = transform.localScale;
        scaleOpposite = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Person")
        {
            int ranX = Random.Range(-areaX, areaX);
            int ranY = Random.Range(-areaY, areaY);
            target = new Vector3(ranX, ranY, -1);
        }

    }

    private void FixedUpdate()
    {
        directionCheck(target.x, transform.position.x);
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    void directionCheck(float target, float pos) //WHY DOES THIS GOTTA BE SO DAMN COMPLICATED MAN 
    {
        if (target >= 0)
        {
            if (pos >= 0)
            {
                if (target >= pos) { transform.localScale = scale; }
                else if (target <= pos) { transform.localScale = scaleOpposite; }
            }
            else if (pos <= 0) { transform.localScale = scale; }
        }
        else if (target <= 0)
        {
            if (pos >= 0) { transform.localScale = scaleOpposite; }
            else if (pos <= 0)
            {
                if (target >= pos) { transform.localScale = scale; }
                else if (target < pos) { transform.localScale = scaleOpposite; }
            }
        }
    }
}
