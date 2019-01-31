using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    [SerializeField]
    private float speed = 20f;
    private Vector2 direction;
    private double worldX;
    private double worldY;

    protected bool walking = false;

    void Start()
    {
        worldX = GameObject.Find("/Quad").transform.localScale.x / 2;
        worldY = GameObject.Find("/Quad").transform.localScale.y / 2;
    }

    // Update is called once per frame
    void Update() {
        getInput();
        Move();
    }

    public void Move()
    {
        
        if ( (transform.position.x > worldX && direction == Vector2.right ) ||
            (transform.position.x < - worldX && direction == Vector2.left) ||
            (transform.position.y > worldY && direction == Vector2.up) ||
            (transform.position.y < - worldY && direction == Vector2.down))
        {
            Debug.Log("hit the bound");
        }
        else
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }


    }

    protected void getInput()
    {
        direction = Vector2.zero;
        walking = false;

        if (Input.GetKey(KeyCode.W)){
            direction = Vector2.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction = Vector2.left;
            walking = true;
        }

        if (Input.GetKey(KeyCode.S)){
            direction = Vector2.down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction = Vector2.right;
            walking = true;
        }
    }
}
