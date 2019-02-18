using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallProjectile : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public LayerMask hittableObjects;
    //This is like its hitbox
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("stationaryBall", lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance, hittableObjects);
        if (hit.collider != null && hit.collider.gameObject.name != "2")
        {
            if (hit.collider.CompareTag("Person"))
            {
                Debug.Log("Ouch! You hit " + hit.collider.gameObject.name);
                GameObject NPC = hit.collider.gameObject;
                NPC.GetComponent<PlayCatch>().hitByBall();
                GameObject.Find("2").GetComponent<Animator>().SetBool("hasBall", false);
            }
            else if (hit.collider.CompareTag("MC"))
            {
                GameObject MC = hit.collider.gameObject;
                MC.GetComponent<PlayCatch>().hitByBall();
                EmoControl.mcBallHit += 1;
                GameObject.Find("2").GetComponent<Animator>().SetBool("hasBall", false);
            }

            destroyBall();
            
        }

        if( transform.position.x > Playground.RightX ||
            transform.position.x < Playground.LeftX  ||
            transform.position.y > Playground.UpperY ||
            transform.position.y < Playground.LowerY)
        {
            stationaryBall();
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

    private void getBallAnim()
    {

    }
}
