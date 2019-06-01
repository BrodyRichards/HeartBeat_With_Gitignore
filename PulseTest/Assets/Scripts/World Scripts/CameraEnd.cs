using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEnd : MonoBehaviour
{
    private Camera cam;
    private Vector3 target;

    private float maxFOV = 30f;
    private float minFOV = 4.84f;
    private float speed = 40f;


    private float targetOrtho;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        targetOrtho = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.k//(KeyCode.P))
        //{
            Vector3 pos = new Vector3(-13.7f, -1.5f, -10f);
            target = pos;
            
            targetOrtho += speed / 500;
            if (targetOrtho > maxFOV)//cam.orthographicSize < minFOV)
            {
                //cam.orthographicSize = minFOV;
                targetOrtho = maxFOV;
                //targetOrtho = Mathf.Clamp(targetOrtho, minFOV, maxFOV);
            }
            
            /*
            targetOrtho -= speed / 500;
            if (targetOrtho < minFOV)//cam.orthographicSize < minFOV)
            {
                //cam.orthographicSize = minFOV;
                targetOrtho = minFOV;
                //targetOrtho = Mathf.Clamp(targetOrtho, minFOV, maxFOV);
            }
            */
            cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, targetOrtho, speed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, target,  5f * Time.deltaTime);
        //}
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
