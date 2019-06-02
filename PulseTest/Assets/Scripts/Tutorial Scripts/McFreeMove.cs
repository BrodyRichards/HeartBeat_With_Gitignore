using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class McFreeMove : MonoBehaviour
{
    public Animator anim;
    private float currSpeed;
    private float lastX;
    private bool isFlipped;
    private Animator animForMC;
    private enum Mood { happy, sad, idle };
    private float maxSpeed;
    public float acceleration;
    public float deceleration;
    public float stayTimer;
    private float stayTime;
    private float step;
    public bool inFinalScene;
    private Vector2 direction;
    public Vector3 scale;
    public Vector3 scaleOpposite;
    private string[] controllersName = { "MC_happy", "MC_sad", "MC_controller" };
    public List<Vector2> mcWaypoints;
    //public List<Vector2> refPoints;
    //public Vector2 waypoint1;
    //public Vector2 waypoint2;
    //public Vector2 waypoint3;

    private void Awake()
    {
        currSpeed = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = 5;
        isFlipped = false;
        animForMC = GetComponent<Animator>();
        if (inFinalScene)
        {
            AnimationMoodCheck();
            isFlipped = true;
        }
        else
        {
            lastX = 10f;
        }
        scale = transform.localScale;
        scaleOpposite = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        step = maxSpeed * Time.deltaTime;
        getInput();
        if (!BedtimeProcedure.journalIsOpened)
        {
            GoToWaypoints(step);
        }
    }

    /*
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
    */  

    protected void getInput()
    {
        /*if (mcWaypoints.Count <= 3)
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                mcWaypoints.Add(waypoint1);
            }

            if (Input.GetKey(KeyCode.Alpha2))
            {
                mcWaypoints.Add(waypoint2);
            }

            if (Input.GetKey(KeyCode.Alpha3))
            {
                mcWaypoints.Add(waypoint3);
            }
        }*/
    }

    private void AnimationMoodCheck()
    {
        var mood = MentalState.OverallResult();
        //var mood = -10;
        if (mood < 5 && mood > -5) // no mood
        {
            SwitchAnimController((int)Mood.idle);
            animForMC.SetFloat("speed", 1f);
        }
        else if (mood > 5) // happy
        {
            animForMC.SetFloat("speed", 0.25f);
            SwitchAnimController((int)Mood.happy);
        }
        else
        {
            animForMC.SetFloat("speed", 0.2f);

            SwitchAnimController((int)Mood.sad);
        }
    }

    private void FlipAssetDirection()
    {
        if (lastX < transform.position.x && !isFlipped)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            isFlipped = true;
        }
        else if (lastX > transform.position.x && isFlipped)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            isFlipped = false;
        }

        lastX = transform.position.x;
    }

    // Go to the assigned waypoints that haven't been reached yet
    private void GoToWaypoints(float step)
    {
        Debug.Log(stayTime >= stayTimer);
        if (mcWaypoints.Count != 0 && stayTime >= stayTimer)
        {
            FlipAssetDirection();
            McGoesTo(mcWaypoints[0], step);
        }
        else
        {
            stayTime += Time.deltaTime;
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                BedtimeProcedure.writingJournal = true;
            }else if (SceneManager.GetActiveScene().buildIndex == 1 && !TutorialJournal.journalOpenOnce)
            {
                TutorialProcControl.writingJournal = true;
            }

            //Debug.Log("stayTime" + stayTime);
            //mcWaypoints = new List<Vector2>(refPoints);
        }
    }

    // Move towards the assigned waypoint, if arrive, will return true 
    private void McGoesTo(Vector2 target, float step)
    {

        transform.position = Vector2.MoveTowards(transform.position, target, step);
        anim.SetBool("isWalking", true);

        if (Vector2.Distance(transform.position, target) < 1.0f)
        {
            //Debug.Log("arrive at" + target);
            stayTime = 0f;
            mcWaypoints.RemoveAt(0);
            anim.SetBool("isWalking", false);
        }
    }

    private void SwitchAnimController(int i)
    {
        animForMC.runtimeAnimatorController = Resources.Load(controllersName[i]) as RuntimeAnimatorController;
    }
}

    
