using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrow : MonoBehaviour
{
    public GameObject ball;
    public GameObject newBall;
    public float offset;
    public float pickupDist;
    public Animator anim;
    public bool thrownBall = false;
    public static bool isMeanBall = false;
    

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
        //Space bar for nice action and E for mean action
        if (Input.GetKeyDown(Control.positiveAction) && !thrownBall)
        {
            ThrowBall();
        }
        else if (Input.GetKeyDown(Control.negativeAction) && !thrownBall)
        {
            isMeanBall = true;
            ThrowBall();
        }
    }

    //Helper function to throw ball, reset animation, and stop motion while throwing
    void ThrowBall()
    {
        thrownBall = true;
        if (isMeanBall)
        {
            anim.SetBool("isThrowing", true);
        }
        else
        {
            anim.SetTrigger("kindThrow");
        }
        
        

        // postpone 0.6 seconds to finish the animation
        //Stop movement while throwing
        GameObject.Find("2").GetComponent<Movement>().enabled = false;
        StartCoroutine(PutOutBall());
        StartCoroutine(ResetAnimation());
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
        BallProjectile.meanBallThrown = isMeanBall;

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
        float distance = Vector3.Distance(transform.position, newBall.transform.position);

        if(distance < pickupDist)
        {
            thrownBall = false;
            isMeanBall = false;
            BallProjectile.meanBallThrown = false;
            Destroy(newBall);
            anim.SetBool("hasBall", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "newBall")
        {
            PickupBall();
        }
    }

    IEnumerator ResetAnimation()
    {
        yield return new WaitForSeconds(0.25f);
        anim.SetBool("isThrowing", false);
    }

}
