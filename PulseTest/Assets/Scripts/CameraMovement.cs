using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    //public Transform bushes;    //use Transform for the GameObjects since only the positions are needed
    //Transform bush;             //this will be the target position 
    public GameObject obj;      //necessary to access characterSwitcher script

    public GameObject avatar1;
    public GameObject avatar2;
    public GameObject avatar3;
    public GameObject avatar4;
    private GameObject[] avatars;
    private GameObject avatar;

    private float speed = 8f;
    private int choice;

    private Vector3 offset;     //offset for camera 
    private Camera cam;
    void Start()
    {
        avatars = new GameObject[4];
        avatars[0] = avatar1; avatars[1] = avatar2; avatars[2] = avatar3; avatars[3] = avatar4;
        cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        offset = new Vector3(0, 0, -10);
        choice = obj.GetComponent<characterSwitcher>().getChar();       //sync choice from script with this one
        avatar = avatars[choice];                                 
        //transform.position = avatar.position + offset;                    //camera jumps to character position
    }

    void LateUpdate()
    {
        choice = obj.GetComponent<characterSwitcher>().getChar();
        avatar = avatars[choice];
        if (Vector3.Distance(transform.position, avatar.transform.position) > 5f) //when character switches
        {
            Vector3 target = avatar.transform.position + offset;
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        else
        {
            transform.position = avatar.transform.position + offset;            //camera follows character
        }

    }
}
