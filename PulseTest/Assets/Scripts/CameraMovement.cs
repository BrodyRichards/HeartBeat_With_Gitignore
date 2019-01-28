using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform bushes;    //use Transform for the GameObjects since only the positions are needed
    Transform bush;             //this will be the target position 
    public GameObject obj;      //necessary to access characterSwitcher script

    private float speed = 8f;
    private int choice;

    private Vector3 offset;     //offset for camera 
    private Camera cam;
    void Start()
    {

        cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        offset = new Vector3(0, 0, -10);
        choice = obj.GetComponent<characterSwitcher>().getChar();       //sync choice from script with this one
        bush = bushes.GetChild(choice);                                 
        transform.position = bush.position + offset;                    //camera jumps to character position
    }

    void LateUpdate()
    {
        choice = obj.GetComponent<characterSwitcher>().getChar();
        bush = bushes.GetChild(choice);
        if (Vector3.Distance(transform.position, bush.position) > 5f) //when character switches
        {
            Vector3 target = bush.position + offset;
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        else
        {
            transform.position = bush.position + offset;            //camera follows character
        }

    }
}
