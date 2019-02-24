using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject obj;      //necessary to access characterSwitcher script

    public GameObject avatar1;
    public GameObject avatar2;
    public GameObject avatar3;
    private GameObject[] avatars;
    private GameObject avatar;

    public GameObject mainChar;
    private int lookMC = 0;
    private int mcCheck;
    private bool reachMC = false;

    private float speed = 20f;
    private int choice;

    private Vector3 offset;     //offset for camera 
    private Camera cam;
    private Vector3 target;

    float time;
    float timer;

    void Start()
    {
        avatars = new GameObject[3];
        avatars[0] = avatar1; avatars[1] = avatar2; avatars[2] = avatar3;
        cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        offset = new Vector3(0, 0, -10);
        transform.position = mainChar.transform.position + offset;                    //camera jumps to character position
        time = Time.fixedUnscaledTime;
        timer = time;
    }

    void LateUpdate()
    {
        time = Time.fixedUnscaledTime;
        if (characterSwitcher.charChoice != -1)
        {
            avatar = avatars[characterSwitcher.charChoice - 1];
            mcCheck = lookMC;
            lookMC = EmoControl.hasEmo ? 1 : 0;
            if ((Vector3.Distance(transform.position, avatar.transform.position) > 5f && lookMC == 0)) //when character switches
            {
                target = avatar.transform.position + offset;
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            }
            else
            {
                if (mcCheck != lookMC)
                {
                    timer = time + 3.0f;
                    reachMC = false;
                }
                if (timer >= time)
                {
                    target = mainChar.transform.position + offset;
                }
                else
                {
                    if (transform.position == target)
                    {
                        reachMC = true;
                    }
                    if (reachMC == false)
                    {
                        timer += 3.0f;
                    }
                    else
                    {
                        target = avatar.transform.position + offset;
                    }
                }

                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            }
        }
        
    }
    
}
