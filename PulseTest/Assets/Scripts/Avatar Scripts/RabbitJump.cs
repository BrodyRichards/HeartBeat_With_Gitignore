using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitJump : MonoBehaviour
{
    private bool beingCarried = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        jumpIntoArms();
    }

    public void jumpIntoArms()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (beingCarried)
            {
                transform.parent = null;
                GetComponent<Movement>().enabled = true;
                beingCarried = false;
            }
            else
            {
                //Vector for Raycast, takes mouse position
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //Decompose to 2D vector
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                //Raycast hit register for mouse position
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

                //Check if an avatar was clicked on
                if (hit.collider != null && hit.collider.gameObject.tag == "Person")
                {
                    //Check distance from object
                    Debug.Log("I want to jump into " + hit.collider.gameObject.name + "'s arms");
                    float distance = Vector3.Distance(transform.position, hit.collider.gameObject.transform.position);
                    //Debug.Log(distance);
                    if (distance < 2f)
                    {
                        beingCarried = true;
                        transform.parent = hit.collider.gameObject.transform;
                        GetComponent<Movement>().enabled = false;
                        Debug.Log("I'm being carried");
                    }
                }
            }
            
        }
    }
}
