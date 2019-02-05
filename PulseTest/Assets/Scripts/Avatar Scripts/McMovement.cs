using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McMovement : MonoBehaviour
{
    public Animator anim;

    private Vector2 direction;
    public float speed = 4f;
    private double worldX;

    private Vector2 musicGuyLo;
    private Vector2 rabbitLo;
    private Vector2 ballKidLo;
    private Vector2 startLo;
    float step;

    private Vector2[] McWaypoints;  
    void Start()
    {
        direction = Vector2.right;
        anim.SetBool("isWalking", true);
        worldX = GameObject.Find("/Quad").transform.localScale.x / 2;
        musicGuyLo = GameObject.Find("3").transform.position;
        ballKidLo = GameObject.Find("2").transform.position;
        rabbitLo = GameObject.Find("1").transform.position;
        startLo = gameObject.transform.position;
        Debug.Log("music guy is at" + musicGuyLo);
        Debug.Log("mc is at" + startLo);

        

        McWaypoints = new Vector2[] { musicGuyLo, rabbitLo, ballKidLo};
    }

    void Update()
    {

       

        step = speed * Time.deltaTime; // calculate distance to move
        if (Vector2.Distance(transform.position, musicGuyLo) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, musicGuyLo, step);
        }
        else
        {
            transform.Translate(Time.deltaTime * speed * direction);
        }



    }

    private void BoundCheck()
    {
        if ((transform.position.x > (worldX - 2) && direction == Vector2.right) ||
            (transform.position.x < (-worldX + 2) && direction == Vector2.left))
        {
            direction *= -1;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}
