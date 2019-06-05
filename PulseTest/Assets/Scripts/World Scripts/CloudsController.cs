using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsController : MonoBehaviour
{
    public GameObject leftTree;
    public GameObject rightTree;

    public GameObject[] clouds;

    private float leftDestination;
    private float rightDestination;

    private List<Vector2> destination;

    private readonly float speed = 0.5f;
    private float middleLine;
    // Start is called before the first frame update
    void Start()
    {
        destination = new List<Vector2>();
        leftDestination = leftTree.transform.position.x;
        rightDestination = rightTree.transform.position.x;
        middleLine = leftDestination + (rightDestination - leftDestination / 2);

        foreach(var cloud in clouds)
        {
            if (cloud.transform.position.x < middleLine)
            {
                destination.Add(new Vector2(rightDestination, cloud.transform.position.y));
            }
            else
            {
                destination.Add(new Vector2(leftDestination, cloud.transform.position.y));
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        var step = speed * Time.deltaTime;
        if (characterSwitcher.charChoice == -1)
            CloudGoesToDestination(step);
    }



    // Move towards the assigned waypoint, if arrive, will return true 
    private void CloudGoesToDestination(float step)
    {



        for (var i = 0; i < clouds.Length; ++i)
        {
            clouds[i].transform.position = Vector2.MoveTowards(clouds[i].transform.position, destination[i], step);
            if (Mathf.Abs(clouds[i].transform.position.x - destination[i].x) < 1.0f)
            {
                if (Mathf.Abs(destination[i].x - leftDestination) < Mathf.Epsilon)
                {
                    destination[i] = new Vector2(rightDestination, clouds[i].transform.position.y);
                }
                else
                {
                    destination[i] = new Vector2(leftDestination, clouds[i].transform.position.y);
                }
            }

        }
    }
}
