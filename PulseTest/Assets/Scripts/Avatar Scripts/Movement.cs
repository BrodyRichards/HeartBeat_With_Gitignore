using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    [SerializeField]
    private float speed = 20f;
    public static bool isRight = true;
    private Vector2 direction;
    

    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update() {
        getInput();
        Move();
    }

    public void Move()
    {
        var v2 = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if ( (transform.position.x > Playground.RightX && v2.x > 0 ) ||
            (transform.position.x < Playground.LeftX && v2.x < 0) ||
            (transform.position.y > Playground.UpperY && v2.y > 0) ||
            (transform.position.y < Playground.LowerY && v2.y < 0))
        {
            Debug.Log("hit the bound");
        }
        else
        {
            if (direction == Vector2.right && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                
            }
            else if (direction == Vector2.left && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                
            }
            
            transform.Translate(speed * v2.normalized * Time.deltaTime);
        }


    }

    protected void getInput()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W)){
            direction = Vector2.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction = Vector2.left;
            
        }

        if (Input.GetKey(KeyCode.S)){
            direction = Vector2.down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction = Vector2.right;
            
        }
    }
}
