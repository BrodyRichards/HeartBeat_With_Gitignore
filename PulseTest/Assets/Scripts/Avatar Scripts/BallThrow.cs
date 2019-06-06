using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrow : MonoBehaviour
{
    public GameObject ball;
    public GameObject newBall;
    public LayerMask hittableObjects;
    public float McCheckDist;
    public float radius;
    public float offset;
    public float pickupDist;
    public Animator anim;
    public bool thrownBall = false;
    public static bool isMeanBall = false;
    private RaycastHit2D[] McCheck;
    

    private bool towardRight;

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("hasBall", true);
        anim.SetBool("isThrowing", false);

        towardRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.x >= 0)
        {
            McCheck = Physics2D.CircleCastAll(transform.position, radius, transform.right, McCheckDist, hittableObjects);

        }
        else
        {
            McCheck = Physics2D.CircleCastAll(transform.position, radius, -transform.right, McCheckDist, hittableObjects);

        }

        if (CheckForMC(McCheck))
        {
            BallProjectile.mcInView = true;
        }
        else
        {
            BallProjectile.mcInView = false;
        }

        //Space bar for nice action and E for mean action
        if (Input.GetKeyDown(Control.positiveAction) && !thrownBall)
        {
            ThrowBall();
        }
        else if (Input.GetKeyDown(Control.negativeAction) && !thrownBall)
        {
            BallProjectile.meanBallThrown = isMeanBall = true;
            ThrowBall();
        }
    }

    private bool CheckForMC(RaycastHit2D[] results)
    {
        foreach (RaycastHit2D result in results)
        {
            if (result.collider.gameObject.name == "MC")
            {
                return true;
            }
        }

        return false;
    }

    //Helper function to throw ball, reset animation, and stop motion while throwing
    void ThrowBall()
    {
        thrownBall = true;
        if (isMeanBall)
        {
            anim.SetTrigger("meanThrow");
        }
        else
        {
            anim.SetTrigger("kindThrow");
        }

        PlayCatch.scaleX = transform.localScale.x;
        // postpone 0.6 seconds to finish the animation
        //Stop movement while throwing
        GameObject.Find("2").GetComponent<Movement>().enabled = false;
        StartCoroutine(PutOutBall());

    }

    //Function to instantiate ball when thrown
    IEnumerator PutOutBall()
    {
        yield return new WaitForSeconds(0.6f);
        //A temporary gameobject to store ball instantiation info
        GameObject tempBall;

        //Check if ball kid is facing left
        if (transform.localScale.x < 0)
        {
            //If so, decompose quaternion into vector3 to modify angles
            Vector3 tempRot = transform.rotation.eulerAngles;
            //Create new vector with modified rotation
            tempRot = new Vector3(tempRot.x, tempRot.y + 180, tempRot.z);
            //Turn it back into a quaternion for instantiate() to use
            Quaternion q = Quaternion.Euler(tempRot);
            //Instantiate ball facing other direction
            tempBall = Instantiate(ball, transform.position, q);
        }
        else
        {
            tempBall = Instantiate(ball, transform.position, transform.rotation);
        }

        //Update ballProjectile script with mean ball info
        //BallProjectile.meanBallThrown = isMeanBall;

        anim.SetBool("hasBall", false);
        //Re-enable movement once animation has finished
        if (characterSwitcher.charChoice == 2)
        {
            GameObject.Find("2").GetComponent<Movement>().enabled = true;
        }
    }

    void PickupBall()
    {
        newBall = GameObject.Find("newBall");
        
        thrownBall = false;
        isMeanBall = false;
        BallProjectile.meanBallThrown = false;
        Destroy(newBall);
        anim.SetBool("hasBall", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "newBall")
        {
            PickupBall();
        }
    }



}
