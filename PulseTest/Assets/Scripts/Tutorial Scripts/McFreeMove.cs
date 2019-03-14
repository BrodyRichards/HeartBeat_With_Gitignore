using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McFreeMove : MonoBehaviour
{

    private float currSpeed = 0f;
    private Animator animForMC;
    private enum Mood { happy, sad, idle };
    public float maxSpeed;
    public float acceleration;
    public float deceleration;
    public bool inFinalScene;
    private Vector2 direction;
    public Vector3 scale;
    public Vector3 scaleOpposite;



    // Start is called before the first frame update
    void Start()
    {

        animForMC = GetComponent<Animator>();
        //if (inFinalScene)
        //{
        //    AnimationMoodCheck();
        //}
        scale = transform.localScale;
        scaleOpposite = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

    }

    // Update is called once per frame
    void Update()
    {
        getInput();
        if (!EndJournal.journalIsOpened)
        {
            Move();
        }
       

    }

    public void Move()
    {
        var v2 = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (v2.x != 0 || v2.y != 0)
        {
            animForMC.SetBool("isWalking", true);
            if (GameObject.Find("Dir") != null) { GameObject.Find("Dir").SetActive(false); }
            //Debug.LogError("is walking");

        }
        else
        {
            //Debug.LogError("not walking");
            //Debug.Log("dfdfdfdfdfdddf walking");
            animForMC.SetBool("isWalking", false);

        }

        if ((transform.position.x > 5.0f && v2.x > 0) ||
            (transform.position.x < -7.0f && v2.x < 0) ||
            (transform.position.y > 0.5f && v2.y > 0) ||
            (transform.position.y < -3.0f && v2.y < 0))
        {
            //Hit the bound
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

            if (direction != Vector2.zero && currSpeed < maxSpeed)
            {
                currSpeed += acceleration;
            }

            if (direction == Vector2.zero && currSpeed > 0)
            {
                currSpeed -= deceleration;
            }


            transform.Translate(currSpeed * v2.normalized * Time.deltaTime);
        }
    }

    protected void getInput()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction = Vector2.up;

        }

        if (Input.GetKey(KeyCode.A))
        {
            direction = Vector2.left;

        }

        if (Input.GetKey(KeyCode.S))
        {
            direction = Vector2.down;

        }

        if (Input.GetKey(KeyCode.D))
        {
            direction = Vector2.right;

        }

    }

    private void AnimationMoodCheck()
    {

        var mood = MentalState.OverallResult();
        if (mood < 5 && mood > -5) // no mood
        {
            animForMC.SetInteger("mood", (int)Mood.idle);
        }
        else if (mood > 5) // happy
        {
            animForMC.SetInteger("mood", (int)Mood.happy);
        }
        else
        {

            animForMC.SetInteger("mood", (int)Mood.sad);
        }
    }
}

    
