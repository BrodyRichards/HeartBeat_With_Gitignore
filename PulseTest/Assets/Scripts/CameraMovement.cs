using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject obj;      //necessary to access characterSwitcher script

    public GameObject avatar1;
    public GameObject avatar2;
    public GameObject avatar3;
    public GameObject avatar4;
    private GameObject[] avatars;
    private GameObject avatar;

    public GameObject mainChar;
    private int lookMC = 0;
    private int mcCheck;
    private bool reachMC = false;
    private Vector3 mcLo;

    private float speed = 20f;
    private int choice;

    private Vector3 offset;     //offset for camera 
    private Camera cam;
    private Vector3 target;
    Vector3 compare;

    void Start()
    {
        avatars = new GameObject[4];
        avatars[0] = avatar1; avatars[1] = avatar2; avatars[2] = avatar3; avatars[3] = avatar4;
        cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        offset = new Vector3(0, 0, -10);
        transform.position = GameObject.Find("MC").transform.position + offset;                    //camera jumps to character position
    }

    void LateUpdate()
    {
        mcLo = mainChar.transform.position + offset;
        avatar = avatars[characterSwitcher.charChoice];
        mcCheck = lookMC;
        lookMC = EmoControl.hasEmo ? 1 : 0;
        if (Vector3.Distance(transform.position, avatar.transform.position) > 5f && lookMC == 0) //when character switches
        {
            target = avatar.transform.position + offset;
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        else
        {
            if (mcCheck != lookMC && reachMC == false)
            //if (mcCheck != lookMC || (reachMC == false && EmoControl.hasEmo))
            {
                
                target = mcLo;
                //Debug.Log(mcLo);
                //lookAtMC();
            }
            else
            {
       
                compare = transform.position;
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);           //camera follows character  
                if (compare == transform.position)
                {
                    target = avatar.transform.position + offset;
                }
                reachMC = false;
            }  
        }
    }

    /*
    void lookAtMC()
    {
        target = mainChar.transform.position + offset;
    }
    */
}
