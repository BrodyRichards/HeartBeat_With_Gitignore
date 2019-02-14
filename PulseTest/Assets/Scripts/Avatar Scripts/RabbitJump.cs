using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitJump : MonoBehaviour
{
    public static bool beingCarried = false;
    private Rigidbody2D rb;
    private double currentPosX;
    private double lastPosX;

    public Animator anim;
   
    // Start is called before the first frame update
    void Start()
    {
         lastPosX = transform.position.x;

    }

    // Update is called once per frame
    void Update()
    {
        DetectMovement();
        jumpIntoArms();
    }

    private void DetectMovement()
    {
        currentPosX = transform.position.x;
        if (currentPosX != lastPosX)
        {
            //Debug.Log("rabbit is moving");
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        lastPosX = transform.position.x;
    }

    public void jumpIntoArms()
    {
        if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(1))
        {
            if (beingCarried)
            {
                transform.parent = null;
                GetComponent<Movement>().enabled = true;
                beingCarried = false;
                GetComponent<SortRender>().offset = 10;
                EmoControl.rabbitHug = false;
                anim.SetBool("isCarried", false);
            }
            else
            {
                //Vector for Raycast, takes mouse position
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //Decompose to 2D vector
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                //Raycast hit register for mouse position
                RaycastHit2D hit = Physics2D.CircleCast(transform.position, 4, Vector2.zero);

                //Check if an avatar was clicked on
                if (hit.collider != null && (hit.collider.gameObject.tag == "Person" || hit.collider.gameObject.tag == "MC"))
                {
                    //Check distance from object
                    Debug.Log("I want to jump into " + hit.collider.gameObject.name + "'s arms");
                    float distance = Vector2.Distance(transform.position, hit.collider.gameObject.transform.position);
                    //Debug.Log(distance);
                    //if (distance < 2f)
                    {
                        beingCarried = true;
                        anim.SetBool("isCarried", true);
                        transform.position = new Vector3(hit.collider.gameObject.transform.position.x + 0.1f, hit.collider.gameObject.transform.position.y, -1);
                        transform.parent = hit.collider.gameObject.transform;
                        if (hit.collider.gameObject.name == "MC")
                        {
                            EmoControl.rabbitHug = true;
                        }
                        
                        GetComponent<Movement>().enabled = false;
                        GetComponent<SortRender>().offset = 0;
                        Debug.Log("I'm being carried");
                    }
                }
            }
            
        }
    }
}
