using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    [SerializeField]
    private float speed = 20f;
    private Vector2 direction;

    protected bool walking = false;



    // Update is called once per frame
    void Update () {
        getInput();
        Move();
	}

    public void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
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
