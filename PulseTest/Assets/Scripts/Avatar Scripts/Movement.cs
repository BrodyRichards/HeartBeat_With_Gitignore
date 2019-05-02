using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField]
    private float maxSpeed = 20f;
    public float currSpeed = 0f;
    public float acceleration;
    public float deceleration;
    public static bool isRight;
    public static bool timeToLeave;
    private Vector2 direction;
    public Vector3 target;
    public Vector3 scale;
    public Vector3 scaleOpposite;

    public Animator anim;

    private void Awake()
    {
        timeToLeave = false;
        isRight = true;
    }
    void Start()
    {
        anim.SetBool("isWalking", false);
        target = GameObject.Find("GameController").GetComponent<NpcInstantiator>().rightBound.transform.position;
        scale = transform.localScale;
        scaleOpposite = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (!timeToLeave) //|| this.gameObject.name == "MC"
        {
            getInput();
            Move();
            /*if (this.gameObject.name == "MC")
            {
                ExitCheck();
            }*/
        }
        else
        {
            AvatarsExit();
        }
    }

    public void Move()
    {
        var v2 = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (v2.x != 0 || v2.y!=0 )
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if ((transform.position.x > Playground.RightX && v2.x > 0) ||
            (transform.position.x < Playground.LeftX && v2.x < 0) ||
            (transform.position.y > Playground.UpperY && v2.y > 0) ||
            (transform.position.y < Playground.LowerY && v2.y < 0))
        {
            //Hit the bound
        }
        else
        {
            if (v2.x > 0 && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            else if (v2.x < 0 && transform.localScale.x > 0)
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

    public void ExitCheck()
    {
        RaycastHit2D wallCheck = Physics2D.Raycast(transform.position, transform.right, 0.25f);
        //Check for MC exit scene
        if (wallCheck.collider != null && wallCheck.collider.gameObject.name == "RightBound")
        {
            LevelFade.readyToLeave = true;
            //Debug.Log("Loading ResultScreen...");
            //SceneManager.LoadScene("ResultScreen");
        }
    }

    public void AvatarsExit()
    {
        anim.SetBool("isWalking", true);

        transform.localScale = scale;

        if(this.name == "MC")
        {
            maxSpeed = MCBTCreator.walkSpeed;
        }

        if (currSpeed < maxSpeed)
        {
            currSpeed += acceleration;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, currSpeed * Time.deltaTime);

        if (this.name == "MC")
        {
            ExitCheck();
        }
    }
}
