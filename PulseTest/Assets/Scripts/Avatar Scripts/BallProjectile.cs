using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallProjectile : MonoBehaviour
{
    public GameObject ballHitParticle;
    private GameObject tempSys;
    private int numBounces;
    private bool firstBounce;
    public Vector3 niceTargetLoc;
    public Vector3 niceTargetLoc1;
    public Vector3 niceTargetLoc2;
    public Vector3 meanTargetLoc;
    public Vector3 meanTargetLoc1;
    public Vector3 meanTargetLoc2;
    public Vector3 startPos;
    public float arcHeight = 2;

    public float delayTime;

    public float speed;
    public float meanSpeed;
    private float lifeCounter;
    public float meanLifetime;
    public float lifetime;
    public LayerMask hittableObjects;
    public LayerMask avatars;
    //This is like its hitbox
    public float distance;
    public float McCheckDist;
    public float radius;
    public static bool meanBallThrown = false;
    public static bool mcInView;
    public static bool musicKidTalk = false;
    //public static bool playBallPlayer = false;

    public static string NpcName = "";

    // Start is called before the first frame update
    void Start()
    {
        //Invoke("stationaryBall", lifetime);
        niceTargetLoc = GameObject.Find("target").transform.position;
        niceTargetLoc1 = GameObject.Find("target1").transform.position;
        niceTargetLoc2 = GameObject.Find("target2").transform.position;
        meanTargetLoc = GameObject.Find("meanTarget").transform.position;
        meanTargetLoc1 = GameObject.Find("meanTarget1").transform.position;
        meanTargetLoc2 = GameObject.Find("meanTarget2").transform.position;
        startPos = transform.position;
        meanSpeed = speed * 1.5f;
        firstBounce = true;
        delayTime = 0.5f;
        McCheckDist = 2f;
        radius = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //This check is to see if the MC is in view of the ball kid
        if (mcInView)
        {
            Debug.Log("MC Detected");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, distance, hittableObjects);
            if (hit.collider != null && hit.collider.gameObject.name != "2")
            {
                this.GetComponent<AudioSource>().Play();
                Debug.Log("please play");
                if (hit.collider.CompareTag("MC")) //hit.collider.CompareTag("Person") || 
                {

                    if (meanBallThrown)
                    {
                        //A mean ball was thrown
                        //Update Mental State
                        TriggerHitAnim();
                        PlayHitParticles();
                        MentalState.sendMsg("Hit by ball");
                        McMovement.gotHit = true;

                        //MC gets hit by ball and doesn't play catch
                        //stationaryBall();
                        ballStops();
                    }
                    else
                    {
                        //This is stuff for normal nicely thrown balls
                        //Debug.Log("You played catch with " + hit.collider.gameObject.name);
                        meanBallThrown = false;
                        Debug.Log("BallProjectile Else: " + meanBallThrown);
                        GameObject MC = hit.collider.gameObject;
                        MentalState.sendMsg("Played catch");
                        MCBTCreator.playedCatch = true;
                        GameObject.Find("MC").GetComponent<Animator>().SetTrigger("playCatch");

                        PlayCatch delayCatch = MC.GetComponent<PlayCatch>();
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
                    NpcName = hit.collider.gameObject.name;
                    PlayHitParticles();
                    ballStops();
                    destroyBall();
                }
                else if (hit.collider.gameObject.name != "Runner(Clone)")
                {
                    meanBallThrown = false;
                    Debug.Log("BallProjectile NPC Else: " + meanBallThrown);
                    PlayCatch.playingCatch = true;
                    GameObject NPC = hit.collider.gameObject;
                    NpcName = NPC.name;
                    PlayCatch.npcName = NpcName;
                    PlayCatch delayCatch = NPC.GetComponent<PlayCatch>();
                    GameObject.Find("2").GetComponent<Animator>().SetBool("hasBall", false);
                    delayCatch.Invoke("hitByBall", delayTime);
                    destroyBall();
                }
            }
        }

        RaycastHit2D avHit = Physics2D.Raycast(transform.position, transform.right, distance, avatars);
        if (avHit.collider != null && avHit.collider.gameObject.name == "3")
        {
            Debug.Log("Hit the music kid");
            musicKidTalk = true;
        }

        if ( transform.position.x > Playground.RightX ||
             transform.position.x < Playground.LeftX  ||
             transform.position.y > Playground.UpperY + 10f ||
             transform.position.y < Playground.LowerY - 5f )
        {
            ballStops();
        }

        if (meanBallThrown)
        {
            speed = meanSpeed;
            //transform.Translate(Vector2.right * speed * Time.deltaTime);
            //transform.position = Vector2.MoveTowards(transform.position, meanTargetLoc, speed * Time.deltaTime);
            if(lifeCounter >= meanLifetime)
            {
                if (firstBounce)
                {
                    firstBounce = false;
                    speed /= 4;
                }
                stationaryBall();
            }
            else
            {
                lifeCounter += Time.deltaTime;
                SimulateProjectile(meanTargetLoc);
            }
        }
        else
        {
            if (lifeCounter >= lifetime)
            {
                if (firstBounce)
                {
                    firstBounce = false;
                    speed /= 2;
                }
                stationaryBall();
            }
            else
            {
                lifeCounter += Time.deltaTime;
                SimulateProjectile(niceTargetLoc);
            }
        }
    }

    private void TriggerHitAnim()
    {
        GameObject mc = GameObject.Find("MC");
        if (Vector2.Distance(gameObject.transform.position, mc.transform.position) < 4f)
        {
            mc.GetComponent<Animator>().SetTrigger("isHit");


        }
    }

    private void SimulateProjectile(Vector3 targetLoc)
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
        if (meanBallThrown)
        {
            if (numBounces == 0)
            {
                if (Vector3.Distance(transform.position, meanTargetLoc1) < 1f)
                {
                    speed /= 2;
                    arcHeight /= 2;
                    numBounces++;
                }
                else
                {
                    SimulateProjectile(meanTargetLoc1);
                }
            }
            else if (numBounces == 1)
            {
                if (Vector3.Distance(transform.position, meanTargetLoc2) < 1f)
                {
                    //arcHeight /= 2;
                    numBounces++;
                }
                else
                {
                    SimulateProjectile(meanTargetLoc2);
                }
            }
            else
            {
                ballStops();
            }
        }
        else
        {
            if (numBounces == 0)
            {
                if (Vector3.Distance(transform.position, niceTargetLoc1) < 1f)
                {
                    arcHeight /= 2;
                    numBounces++;
                }
                else
                {
                    SimulateProjectile(niceTargetLoc1);
                }
            }
            else if (numBounces == 1)
            {
                if (Vector3.Distance(transform.position, niceTargetLoc2) < 1f)
                {
                    numBounces++;
                }
                else
                {
                    SimulateProjectile(niceTargetLoc2);
                }
            }
            else
            {
                ballStops();
            }
        }
    }

    private void ballStops()
    {
        Destroy(gameObject);
        GameObject newBall = Instantiate(gameObject, transform.position, Quaternion.identity);
        SpriteRenderer ballSprite = newBall.GetComponent<SpriteRenderer>();
        ballSprite.sortingLayerName = "Background Props";
        newBall.name = "newBall";
        newBall.AddComponent<CircleCollider2D>().isTrigger = true;
        numBounces = 0;
        lifeCounter = 0f;
    }

    static Quaternion LookAt2D(Vector2 forward)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }

    private void PlayHitParticles()
    {
        tempSys = Instantiate(ballHitParticle, transform.position, Quaternion.identity) as GameObject;
        Destroy(tempSys, 1);
    }
}
