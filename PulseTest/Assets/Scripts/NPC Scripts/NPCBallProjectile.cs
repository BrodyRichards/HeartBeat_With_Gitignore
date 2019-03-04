using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBallProjectile : MonoBehaviour
{
    public Vector3 targetLoc;
    public Vector3 startPos;
    public float arcHeight = 2;

    public float speed = 12f;
    public float lifetime = 1.5f;
    public LayerMask hittableObjects;
    //This is like its hitbox
    public float distance = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("stationaryBall", lifetime);
        startPos = transform.position;
        targetLoc = GameObject.Find("2").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance, hittableObjects);
        if (hit.collider != null && hit.collider.gameObject.tag != "Person")
        {
            if (hit.collider.gameObject.name == "2")
            {
                //Debug.Log("Ouch! You hit " + hit.collider.gameObject.name);
                hit.collider.gameObject.GetComponent<BallThrow>().thrownBall = false;
                destroyBall();
            }
        }

        SimulateProjectile();
    }

    private void SimulateProjectile()
    {
        float x0 = startPos.x;
        float x1 = targetLoc.x;
        float dist = x1 - x0;
        float nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);
        float baseY = Mathf.Lerp(startPos.y, targetLoc.y, (nextX - x0) / dist);
        float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
        Vector3 nextPos = new Vector3(nextX, baseY + arc, transform.position.z);

        // Rotate to face the next position, and then move there
        transform.rotation = LookAt2D(nextPos - transform.position);
        transform.position = nextPos;
    }

    private void destroyBall()
    {
        Destroy(gameObject);
        GameObject.Find("2").GetComponent<Animator>().SetBool("hasBall", true);
        GameObject.Find("2").GetComponent<Animator>().SetBool("isThrowing", false);
    }

    private void stationaryBall()
    {
        //Sound and special FX can go here
        Destroy(gameObject);
        GameObject newBall = Instantiate(gameObject, transform.position, Quaternion.identity);
        newBall.name = "newBall";
    }

    static Quaternion LookAt2D(Vector2 forward)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }
}
