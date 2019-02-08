using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrow : MonoBehaviour
{
    public GameObject ball;
    public float offset;
    public Animator anim;
    

    private bool towardRight;

    // Start is called before the first frame update
    void Start()
    {
        towardRight = true;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 mousePosi = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePosi2D = new Vector2(mousePosi.x, mousePosi.y);
        RaycastHit2D hit2 = Physics2D.Raycast(mousePosi2D, Vector2.zero);
        
        //Use this if hovering over someone
        if (hit2.collider != null)
        {
            

            string name = hit2.collider.gameObject.name;
            Debug.Log(name);
            
        }
            
        if (Input.GetKeyDown("space") )
        {
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

        
       
    }

    void PutOutBall()
    {
        Vector3 throwAngle = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(throwAngle.y, throwAngle.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.Euler(0f, 0f, rotZ + offset);
        Instantiate(ball, transform.position, q);
    }

  
}
