using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBallProjectile : MonoBehaviour
{
    public float speed = 9f;
    public float lifetime = 1.5f;
    public LayerMask hittableObjects;
    //This is like its hitbox
    public float distance = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("stationaryBall", lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance, hittableObjects);
        if (hit.collider != null && hit.collider.gameObject.tag != "Person")
        {
            if (hit.collider.gameObject.name == "2")
            {
                Debug.Log("Ouch! You hit " + hit.collider.gameObject.name);
                hit.collider.gameObject.GetComponent<BallThrow>().thrownBall = false;
            }

            destroyBall();
        }

        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void destroyBall()
    {
        Destroy(gameObject);
    }

    private void stationaryBall()
    {
        //Sound and special FX can go here
        Destroy(gameObject);
        GameObject newBall = Instantiate(gameObject, transform.position, Quaternion.identity);
        newBall.name = "newBall";
    }
}
