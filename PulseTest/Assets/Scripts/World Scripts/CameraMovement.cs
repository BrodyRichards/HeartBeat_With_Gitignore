using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject obj;      //necessary to access characterSwitcher script
    private float maxFOV = 22f;
    private float minFOV = 15f;
    
    private float targetOrtho;

    public GameObject avatar1;
    public GameObject avatar2;
    public GameObject avatar3;
    private GameObject[] avatars;
    private GameObject avatar;

    public GameObject mainChar;
    private int lookMC;
    private int mcCheck;
    private bool reachMC;

    private float speed = 30f;
    private int choice;

    private Vector3 offset;     //offset for camera 
    private Vector3 mcOffset;
    private Camera cam;
    private Vector3 target;

    public GameObject leftBound;
    float left;
    public GameObject rightBound;
    float right;

    float time;
    float timer;

    public static bool thoughtSystem;

    private void Awake()
    {
        reachMC = false;
        lookMC = 0;
        thoughtSystem = false;
    }

    void Start()
    {
        avatars = new GameObject[3];
        avatars[0] = avatar1; avatars[1] = avatar2; avatars[2] = avatar3;
        cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        offset = new Vector3(0, 8, -10);
        mcOffset = new Vector3(0, 0, -10);
        //transform.position = mainChar.transform.position + offset;                    //camera jumps to character position
        target = mainChar.transform.position + mcOffset;// + offset;
        //target = transform.position;
        transform.position = target;
        time = Time.fixedUnscaledTime;
        timer = time;
        targetOrtho = cam.orthographicSize;
        //cam.fieldOfView = fov;
        //zoomOut();
        
    }

    void LateUpdate()
    {
        time = Time.fixedUnscaledTime;
        if (characterSwitcher.charChoice == -1 && McMovement.leftBoundDist <= 30.0f)
        {
            target = mainChar.transform.position + mcOffset;// + offset;
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        else if (characterSwitcher.charChoice == -1 && McMovement.leftBoundDist >= 30.0f)
        {
            thoughtSystem = true;
            Vector3 pos = new Vector3(-35.2f, 1.0f, -1.3f);
            target = pos;
            targetOrtho += speed / 500;
            if (targetOrtho > maxFOV)//cam.orthographicSize < minFOV)
            {
                //cam.orthographicSize = minFOV;
                targetOrtho = maxFOV;
                //targetOrtho = Mathf.Clamp(targetOrtho, minFOV, maxFOV);
            }
            cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, targetOrtho, speed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        if (characterSwitcher.charChoice != -1 && characterSwitcher.charChoice != 1000)
        {
            checkBounds(target);
            avatar = avatars[characterSwitcher.charChoice - 1];
            mcCheck = lookMC;
            lookMC = EmoControl.hasEmo ? 1 : 0;
            if ((Vector3.Distance(transform.position, avatar.transform.position) > 5f && lookMC == 0)) //when character switches
            {
                
                target = avatar.transform.position + offset;
                targetOrtho -= speed / 500;
                if (targetOrtho < minFOV)//cam.orthographicSize < minFOV)
                {
                    //cam.orthographicSize = minFOV;
                    targetOrtho = minFOV;
                    //targetOrtho = Mathf.Clamp(targetOrtho, minFOV, maxFOV);
                }
                cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, targetOrtho, speed * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                //zoomIn();
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
        else if (characterSwitcher.charChoice == 1000)
        {
            target = mainChar.transform.position + offset;
            checkBounds(target);
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        //checkBounds(target);
        
    }

    void checkBounds(Vector3 pos)
    {
        float leftDist = Vector3.Distance(pos, leftBound.transform.position);
        float rightDist = Vector3.Distance(pos, rightBound.transform.position);
        //Debug.Log("left " + leftDist);
        //Debug.Log("right " + rightDist);
        
        if (leftDist <= 65.0f)
        {
            left = 65.0f - leftDist;
            newPos(pos, left);
        }
        if (rightDist <= 60.0f)
        {
            right = 60.0f - rightDist;
            newPos(pos, -1 * right);
        }
        
    }

    void newPos(Vector3 pos, float f)
    {
        Vector3 displace = new Vector3(f, 0, 0);
        transform.position = Vector3.MoveTowards(transform.position, pos + displace, speed * Time.deltaTime);
    }

    void zoomIn()
    {
        //cam.orthographicSize -= speed / 8;
        targetOrtho -= speed / 20;
        if (targetOrtho < minFOV)//cam.orthographicSize < minFOV)
        {
            //cam.orthographicSize = minFOV;
            targetOrtho = minFOV;
            //targetOrtho = Mathf.Clamp(targetOrtho, minFOV, maxFOV);
        }
        cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, targetOrtho, speed * Time.deltaTime);
    }

    void zoomOut()
    {
        targetOrtho += speed / 20;
        if (targetOrtho < minFOV)//cam.orthographicSize < minFOV)
        {
            //cam.orthographicSize = minFOV;
            targetOrtho = minFOV;
            //targetOrtho = Mathf.Clamp(targetOrtho, minFOV, maxFOV);
        }
        cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, targetOrtho, speed * Time.deltaTime);
    }
}
    

