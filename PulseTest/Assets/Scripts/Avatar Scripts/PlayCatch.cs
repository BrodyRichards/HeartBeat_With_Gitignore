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

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Quaternion getAngle()
    { 
        Vector3 dirVector = to.position - transform.position;
        float angleZ = Mathf.Atan2(dirVector.y, dirVector.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.Euler(0f, 0f, angleZ + offset);

        return q;
    }

    public void hitByBall()
    {
        Debug.Log("Detected collision");
        //Get quaternion with correct angle information from monster to player
        Quaternion q = getAngle();
        //Create projectile of type projectile, at current lychee position, with rotation info in quaternion q
        GameObject npcBall = Instantiate(projectile, transform.position, q);
        npcBall.GetComponent<BallProjectile>().enabled = false;
        NPCBallProjectile bp = npcBall.AddComponent<NPCBallProjectile>();
        bp.speed = 9f;
        bp.lifetime = 1.5f;
        bp.distance = 0.25f;
        bp.hittableObjects |= (1 << LayerMask.NameToLayer("Avatar"));
    }
}
