using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCatch : MonoBehaviour
{
    //The current bullet used
    public GameObject projectile;
    //The position of the target (Player)
    public Transform to;
    //This should always be set to -90 cuz it works. Change it to get weird rotations.
    public float offset;
    //Name of the NPC playing catch
    public static string npcName;
    private GameObject ballKid;
    public float faceTime;
    public static float scaleX;
    public static bool playingCatch;
    public Animator anim;

    // Use this for initialization
    void Start()
    {
        faceTime = 2.5f;
        ballKid = GameObject.Find("2");
        to = GameObject.Find("2").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.name == npcName && playingCatch)
        {
            StartCoroutine(FaceCorrectDirection(scaleX, transform.localScale.x, transform.position, anim.GetBool("IsWalking")));
        }
    }

    Quaternion npcGetAngle()
    {
        Vector3 dirVector = to.position - transform.position;
        float angleZ = Mathf.Atan2(dirVector.y, dirVector.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.Euler(0f, 0f, angleZ + offset);
        //Quaternion q = Quaternion.LookRotation(to.position - transform.position);
        return q;
    }

    public void hitByBall()
    {
        Debug.Log("Detected collision");
        //Make the NPC face the ball kid
        /*if(this.name != "MC")
        {
            playingCatch = true;
            StartCoroutine(FaceCorrectDirection(scaleX, transform.localScale.x, transform.position, anim.GetBool("isWalking")));
        }*/
        //Get quaternion with correct angle information from avatar to player
        Quaternion q = npcGetAngle();
        //Create projectile of type projectile, at current avatar position, with rotation info in quaternion q
        GameObject npcBall = Instantiate(projectile, transform.position, q);
        //Disable avatar's version of ball projectile script
        npcBall.GetComponent<BallProjectile>().enabled = false;
        //Give the NPC a projectile script
        NPCBallProjectile bp = npcBall.AddComponent<NPCBallProjectile>();
        //Set variables for projectile script
        bp.hittableObjects |= (1 << LayerMask.NameToLayer("Avatar"));
    }

    IEnumerator FaceCorrectDirection(float kidScaleX, float NPCScaleX, Vector3 currPos, bool status)
    {
        float currTime = 0;

        //If they both are facing the left, make NPC face the right
        //Else if they are both facing the right make NPC face the left
        if (kidScaleX < 0 && NPCScaleX < 0)
        {
            while(currTime <= faceTime)
            {
                Debug.Log("Face right");
                anim.SetBool("IsWalking", false);
                transform.position = currPos;
                transform.localScale = new Vector3(1, 1, 1);
                currTime += Time.deltaTime;
                yield return null;
            }
        }
        else if(kidScaleX > 0 && NPCScaleX > 0)
        {
            while (currTime <= faceTime)
            {
                Debug.Log("Face left");
                anim.SetBool("IsWalking", false);
                transform.position = currPos;
                transform.localScale = new Vector3(-1, 1, 1);
                currTime += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            while(currTime <= faceTime)
            {
                anim.SetBool("IsWalking", false);
                transform.position = currPos;
                currTime += Time.deltaTime;
                yield return null;
            }
        }

        playingCatch = false;
        //anim.SetBool("IsWalking", status);
    }
}
