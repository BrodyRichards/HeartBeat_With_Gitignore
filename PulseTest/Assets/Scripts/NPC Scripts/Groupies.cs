using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI; may need this in the future??

public class Groupies : MonoBehaviour
{
    Vector3 target;
    private GameObject area;   //quad
    private int areaX, areaY; //get the size of the quad
    private float speed = 5f;

    public GameObject manager;
    public Vector2 location = Vector2.zero;
    public Vector2 velocity;
    Vector2 goalPos = Vector2.zero;
    Vector2 currentForce;

    // Start is called before the first frame update
    void Start()
    {
        area = GameObject.Find("Quad");
        areaX = ((int)area.transform.localScale.x) / 2;
        areaY = ((int)area.transform.localScale.y) / 2;
        int ranX = Random.Range(-areaX, areaX);
        int ranY = Random.Range(-areaY, areaY);
        //target = new Vector3(ranX, ranY, -1);
        location = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
        velocity = new Vector2(Random.Range(0.01f, 0.1f), Random.Range(0.01f, 0.1f));
    }

    Vector2 seek(Vector2 target)
    {
        return (target - location);
    }

    void applyForce(Vector2 f)
    {
        Vector3 force = new Vector3(f.x, f.y, 0);
        this.GetComponent<Rigidbody2D>().AddForce(force);
        Debug.DrawRay(this.transform.position, force, Color.white);
    }

    void flock()
    {
        location = this.transform.position;
        velocity = this.GetComponent<Rigidbody2D>().velocity;

        Vector2 gl;
        gl = seek(goalPos);
        currentForce = gl;
        currentForce = currentForce.normalized;

        applyForce(currentForce);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position == target)
        {
            int ranX = Random.Range(-areaX, areaX);
            int ranY = Random.Range(-areaY, areaY);
            target = new Vector3(ranX, ranY, -1);
        }
        */
        /*
        if (this.gameObject.transform.position == goalPos)
        {

        }
        */
        flock();
        int ranX = Random.Range(-areaX, areaX);
        int ranY = Random.Range(-areaY, areaY);
        target = new Vector3(ranX, ranY, -1);
        //goalPos = new Vector2(ranX, ranY);
        goalPos = area.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        //Debug.Log(goalPos);
    }
}
