using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrow : MonoBehaviour
{
    public GameObject ball;
    public GameObject newBall;
    public float offset;
    public Animator anim;
    public bool thrownBall = false;
    

    private bool towardRight;

    // Start is called before the first frame update
    void Start()
    {
        towardRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && !thrownBall)
        {
            thrownBall = true;
            //Vector for Raycast, takes mouse position
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            //Raycast hit register for mouse position
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            //If a hit is registered, find which object was hit
            if (hit.collider == null || hit.collider.gameObject.tag != "Avatars")
            {

                // check the character direction 
                if (mousePos.x < transform.position.x && transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

                }
                else if (mousePos.x > transform.position.x && transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }

                anim.SetBool("isThrowing", true);

                // postpone 0.6 seconds to finish the animation 
                Invoke("PutOutBall", 0.6f);
            }
        }
        else
        {
            anim.SetBool("isThrowing", false);
        }

        //Check to see that a ball was thrown and that it is resting stationary on the ground
        if (Input.GetKey(KeyCode.Space) && thrownBall && GameObject.Find("newBall") != null)
        {
            PickupBall();
        }

    }

    void PutOutBall()
    {
        Vector3 throwAngle = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(throwAngle.y, throwAngle.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.Euler(0f, 0f, rotZ + offset);
        Instantiate(ball, transform.position, q);
    }

    void PickupBall()
    {
        newBall = GameObject.Find("newBall");
        float distance = Vector3.Distance(transform.position, newBall.transform.position);
        //Debug.Log(distance);
        if(distance < 2f)
        {
            thrownBall = false;
            Destroy(newBall);
        }
    }


}
