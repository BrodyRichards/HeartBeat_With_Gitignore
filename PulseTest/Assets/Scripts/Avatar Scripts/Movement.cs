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
    public float leaveTime;
    private float leaveTimer;
    public static bool isRight;
    public static bool timeToLeave;
    private Vector2 direction;
    public Vector3 target;
    public Vector3 scale;
    public Vector3 scaleOpposite;

    public Animator anim;

    public float offset;

    private void Awake()
    {
        timeToLeave = false;
        isRight = true;
        offset = 0;
    }
    void Start()
    {
        anim.SetBool("isWalking", false);
        target = GameObject.Find("GameController").GetComponent<NpcInstantiator>().rightBound.transform.position;
        scale = transform.localScale;
        scaleOpposite = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        if (gameObject.name == "1")
        {
            offset = 2f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!timeToLeave)
        {
            UpdatePlayTimes();
            getInput();
            Move();
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
            (transform.position.y > Playground.UpperY - offset && v2.y > 0) ||
            (transform.position.y < Playground.LowerY - offset && v2.y < 0))
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

    private void ExitCheck()
    {
        RaycastHit2D wallCheck = Physics2D.Raycast(transform.position, transform.right, 0.25f);
        //Check for MC exit scene
        if (wallCheck.collider != null && wallCheck.collider.gameObject.name == "RightBound")
        {
            LevelFade.readyToLeave = true;
            //Debug.Log("Loading ResultScreen...");
            //SceneManager.LoadScene("ResultScreen");
        }else if(leaveTimer >= leaveTime)
        {
            LevelFade.readyToLeave = true;
        }
        else
        {
            leaveTimer += Time.deltaTime;
        }
    }

    private void AvatarsExit()
    {
        anim.SetBool("isWalking", true);

        if(this.name == "MC")
        {
            maxSpeed = MCBTCreator.walkSpeed;
            scale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        if (currSpeed < maxSpeed)
        {
            currSpeed += acceleration;
        }

        transform.localScale = scale;

        transform.position = Vector3.MoveTowards(transform.position, target, currSpeed * Time.deltaTime);

        if (this.name == "MC")
        {
            ExitCheck();
        }
    }

    //Helper function to update play times for each avatar
    //Times are stored in IterationController static floats
    private void UpdatePlayTimes()
    {
        if(this.name == "1")
        {
            IterationController.bunnyTimer += Time.deltaTime;
            //Debug.Log("Bunny Time: " + IterationController.bunnyTimer);
        }else if (this.name == "2")
        {
            IterationController.ballKidTimer += Time.deltaTime;
            //Debug.Log("BallKid Time: " + IterationController.ballKidTimer);
        }
        else if (this.name == "3")
        {
            IterationController.musicKidTimer += Time.deltaTime;
            //Debug.Log("MusicKid Time: " + IterationController.musicKidTimer);
        }
    }
}
