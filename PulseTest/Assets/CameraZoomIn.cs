using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomIn : MonoBehaviour
{
    private Camera cam;
    private Vector3 target;

    private float maxFOV = 5f;
    private float minFOV = 1.2f;
    private float speed = 20f;

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
        if (JournalOpening.cameraMove)
        {
            Vector3 pos = new Vector3(4.16f, 0.25f, -10f);
            target = pos;

            targetOrtho -= speed / 500;
            if (targetOrtho < minFOV)//cam.orthographicSize < minFOV)
            {
                //cam.orthographicSize = minFOV;
                targetOrtho = minFOV;
                //targetOrtho = Mathf.Clamp(targetOrtho, minFOV, maxFOV);
            }
            cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, targetOrtho, speed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, target, 5f * Time.deltaTime);
        }
    }
}
