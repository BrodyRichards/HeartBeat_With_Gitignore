using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallProjectile : MonoBehaviour
{
    public Vector3 targetLoc;
    public Vector3 startPos;
    public float arcHeight = 2;

    public float delayTime;

    public float speed;
    public float meanSpeed;
    public float lifetime;
    public LayerMask hittableObjects;
    //This is like its hitbox
    public float distance;
    public float McCheckDist;
    public float radius;
    public static bool meanBallThrown = false;
    //public static bool playBallPlayer = false;

    public static string NpcName = "";
    

    // Start is called before the first frame update
    void Start()
    {
        Invoke("stationaryBall", lifetime);
        targetLoc = GameObject.Find("target").transform.position;
        startPos = transform.position;
        meanSpeed = speed * 1.5f;
        delayTime = 0.5f;
        McCheckDist = 10f;
        radius = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        //This check is to see if the MC is in view of the ball kid
        RaycastHit2D[] McCheck = Physics2D.CircleCastAll(transform.position, radius, transform.right, McCheckDist, hittableObjects);
        if (McCheck != null && CheckForMC(McCheck))
        {
            //Debug.Log("MC Detected");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, distance, hittableObjects);
            if (hit.collider != null && hit.collider.gameObject.name != "2")
            {
                if (hit.collider.CompareTag("MC")) //hit.collider.CompareTag("Person") || 
                {
                    if (meanBallThrown)
                    {
                        //A mean ball was thrown
                        //Debug.Log("You threw a mean ball!");
                        //Update Mental State
                        NpcName = hit.collider.gameObject.name;
                        if (NpcName == "MC")
                        {
                            MentalState.sendMsg("Hit by ball");
                            EmoControl.mcBallHit = true;
                            McMovement.gotHit = true;
                        }

                        //MC gets hit by ball and doesn't play catch
                        stationaryBall();
                        //Reset meanBall bool

                        //meanBallThrown = false;
                    }
                    else
                    {
                        //This is stuff for normal nicely thrown balls
                        //Debug.Log("You played catch with " + hit.collider.gameObject.name);
                        meanBallThrown = false;
                        GameObject NPC = hit.collider.gameObject;
                        NpcName = NPC.name;

                        if (NPC.name == "MC")
                        {
                            MentalState.sendMsg("Played catch");
                            McMovement.playedCatch = true;
                            EmoControl.justPlayedCatch = true;
                            GameObject.Find("MC").GetComponent<Animator>().SetTrigger("playCatch");
                        }
                        //NPC.GetComponent<PlayCatch>().hitByBall();
                        PlayCatch delayCatch = NPC.GetComponent<PlayCatch>();
                        GameObject.Find("2").GetComponent<Animator>().SetBool("hasBall", false);

                        delayCatch.Invoke("hitByBall", delayTime);
                        destroyBall();
                    }
                }
            }
        }
        else
        {
            //Ball catch stuff for the NPCs
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, distance, hittableObjects);
            if (hit.collider != null && hit.collider.gameObject.name != "2")
            {
                if (meanBallThrown)
                {
                    stationaryBall();
                }
                else
                {
                    meanBallThrown = false;
                    GameObject NPC = hit.collider.gameObject;
                    PlayCatch delayCatch = NPC.GetComponent<PlayCatch>();
                    GameObject.Find("2").GetComponent<Animator>().SetBool("hasBall", false);
                    delayCatch.Invoke("hitByBall", delayTime);
                }
                destroyBall();
            }
        }

        if ( transform.position.x > Playground.RightX ||
            transform.position.x < Playground.LeftX  ||
            transform.position.y > Playground.UpperY ||
            transform.position.y < Playground.LowerY - 5f )
        {
            stationaryBall();
        }

        if (meanBallThrown)
        {
            speed = meanSpeed;
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            SimulateProjectile();
        }

    }

    private void SimulateProjectile()
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
        newBall.AddComponent<CircleCollider2D>().isTrigger = true;
    }

    static Quaternion LookAt2D(Vector2 forward)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }

    //Helper function for checking if the MC is in the Circle Cast
    private bool CheckForMC(RaycastHit2D[] results)
    {
        foreach(RaycastHit2D result in results)
        {
            if(result.collider.gameObject.name == "MC")
            {
                return true;
            }
        }

        return false;
    }

    private void getBallAnim()
    {

    }
}
