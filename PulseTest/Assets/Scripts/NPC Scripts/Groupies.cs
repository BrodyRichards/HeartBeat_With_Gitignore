using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI; may need this in the future??

public class Groupies : MonoBehaviour
{
    public GameObject manager;
    private Vector2 compare;
    public Vector2 location = Vector2.zero;
    public Vector2 velocity;
    Vector2 goalPos = Vector2.zero;
    Vector2 currentForce;
    private Vector3 scale;
    private Vector3 scaleOpposite;
    // Start is called before the first frame update
    void Start()
    {
        location = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
        velocity = new Vector2(Random.Range(0.1f, 1f), Random.Range(0.1f, 1f));
        compare = new Vector2(2.5f, 2.5f);
        scale = transform.localScale;
        scaleOpposite = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    Vector2 seek(Vector2 target)
    {
        return (target - location);
    }

    void applyForce(Vector2 f)
    {
        Vector3 force = new Vector3(f.x, f.y, 0);
        if (this.GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            transform.localScale = scaleOpposite;
        }
        else
        {
            transform.localScale = scale;
        }
        if (force.magnitude > manager.GetComponent<NpcInstantiator>().maxForce)
        {
            force = force.normalized;
            force *= manager.GetComponent<NpcInstantiator>().maxForce;
        }
        this.GetComponent<Rigidbody2D>().AddForce(force);
        if (this.GetComponent<Rigidbody2D>().velocity.magnitude > manager.GetComponent<NpcInstantiator>().maxVelocity)
        {
            this.GetComponent<Rigidbody2D>().velocity = this.GetComponent<Rigidbody2D>().velocity.normalized;
            this.GetComponent<Rigidbody2D>().velocity *= manager.GetComponent<NpcInstantiator>().maxVelocity;
        }
        Debug.DrawRay(this.transform.position, force, Color.white);
    
    }

    Vector2 align()
    {
        float neighbordist = manager.GetComponent<NpcInstantiator>().neighbourDistance;
        Vector2 sum = Vector2.zero;
        int count = 0;
        foreach (GameObject other in manager.GetComponent<NpcInstantiator>().groupies)
        {
            if (other == this.gameObject) continue;
            float d = Vector2.Distance(location, other.GetComponent<Groupies>().location);
            if (d < neighbordist)
            {
                sum += other.GetComponent<Groupies>().velocity;
                count++;
            }
        }
        if (count > 0)
        {
            sum /= count;
            Vector2 steer = sum - velocity;
            return steer;
        }
        return Vector2.zero;
    }

    Vector2 cohesion()
    {
        float neighbordist = manager.GetComponent<NpcInstantiator>().neighbourDistance;
        Vector2 sum = Vector2.zero;
        int count = 0;
        foreach (GameObject other in manager.GetComponent<NpcInstantiator>().groupies)
        {
            if (other == this.gameObject) continue;
            float d = Vector2.Distance(location, other.GetComponent<Groupies>().location);
            if (d < neighbordist)
            {
                sum += other.GetComponent<Groupies>().location;
                count++;
            }
        }
        if (count > 0)
        {
            sum /= count;
            return seek(sum);
        }
        return Vector2.zero;
    }

    void flock()
    {
        location = this.transform.position;
        velocity = this.GetComponent<Rigidbody2D>().velocity;

        if (Random.Range(0, 50) <= 1)
        {
            Vector2 ali = align();
            Vector2 coh = cohesion();
            Vector2 gl;
            gl = seek(goalPos);
            Vector2 gl2 = new Vector2(Mathf.Abs(gl.x), Mathf.Abs(gl.y));
            if (gl2.x < compare.x && gl2.y < compare.y){
                this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            }
            currentForce = gl + ali + coh;
            currentForce = currentForce.normalized;
        }
        applyForce(currentForce);
    }

    // Update is called once per frame
    void Update()
    {
        goalPos = manager.GetComponent<NpcInstantiator>().target;
        flock();
    }
   
    
}
