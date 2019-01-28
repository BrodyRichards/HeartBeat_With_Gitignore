using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallProjectile : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public LayerMask hittableObjects;
    //This is like its hitbox
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroyBall", lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance, hittableObjects);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Avatars"))
            {
                Debug.Log("Ouch! You hit " + hit.collider.gameObject.name);
            }
            destroyBall();
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void destroyBall()
    {
        //Sound and special FX can go here
        Destroy(gameObject);
    }
}
