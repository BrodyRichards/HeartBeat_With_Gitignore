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

    // Use this for initialization
    void Start()
    {
        to = GameObject.Find("2").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        //GameObject.Find("3").GetComponent<Animator>().SetBool("hasBall", true);
    }
}
