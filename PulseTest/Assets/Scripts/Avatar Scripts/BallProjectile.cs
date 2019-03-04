using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallProjectile : MonoBehaviour
{
    public Vector3 targetLoc;
    public Vector3 startPos;
    public float arcHeight = 2;

    public float speed;
    public float meanSpeed;
    public float lifetime;
    public LayerMask hittableObjects;
    //This is like its hitbox
    public float distance;
    public static bool meanBallThrown = false;
    //public static bool playBallPlayer = false;

    public static string NpcName = "";
    

    // Start is called before the first frame update
    void Start()
    {
        Invoke("stationaryBall", lifetime);
        targetLoc = GameObject.Find("target").transform.position;
        startPos = transform.position;
        meanSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, distance, hittableObjects);
        if (hit.collider != null && hit.collider.gameObject.name != "2")
        {
            if (hit.collider.CompareTag("Person") || hit.collider.CompareTag("MC"))
            {
                if (hit.collider.CompareTag("MC") && meanBallThrown)
                {
                    //A mean ball was thrown
                    //Debug.Log("You threw a mean ball!");
                    //Update Mental State
                    MentalState.sendMsg("Hit by ball");
                    //MC gets hit by ball and doesn't play catch
                    stationaryBall();
                    //Reset meanBall bool
                    meanBallThrown = false;
                }
                else
                {
                    //This is stuff for normal nicely thrown balls
                    //Debug.Log("You played catch with " + hit.collider.gameObject.name);
                    GameObject NPC = hit.collider.gameObject;
                    NpcName = NPC.name;
                    if (NPC.name == "MC")
                    {
                        MentalState.sendMsg("Played catch");
                        
                    }
                    NPC.GetComponent<PlayCatch>().hitByBall();
                    GameObject.Find("2").GetComponent<Animator>().SetBool("hasBall", false);
                }
            }
            
            destroyBall();
        }

        if( transform.position.x > Playground.RightX ||
            transform.position.x < Playground.LeftX  ||
            transform.position.y > Playground.UpperY ||
            transform.position.y < Playground.LowerY )
        {
            stationaryBall();
        }

        if (meanBallThrown)
        {
            speed = meanSpeed * 1.5f;
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            float x0 = startPos.x;
            float x1 = targetLoc.x;
            float dist = x1 - x0;
            float nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);
            float baseY = Mathf.Lerp(startPos.y, targetLoc.y, (nextX - x0) / dist);
            float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
            Vector3 nextPos = new Vector3(nextX, baseY + arc, transform.position.z);

            // Rotate to face the next position, and then move there
            transform.rotation = LookAt2D(nextPos - transform.position);
            transform.position = nextPos;
        }

    }

    private void destroyBall()
    {
        //Sound and special FX can go here
        Destroy(gameObject);
    }

    private void stationaryBall()
    {
        //Sound and special FX can go here
        Destroy(gameObject);
        GameObject newBall = Instantiate(gameObject, transform.position, Quaternion.identity);
        newBall.name = "newBall";
    }

    static Quaternion LookAt2D(Vector2 forward)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }

    private void getBallAnim()
    {

    }
}
