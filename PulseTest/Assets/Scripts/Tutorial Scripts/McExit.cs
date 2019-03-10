using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class McExit : MonoBehaviour
{
    public Vector3 exitPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*RaycastHit2D exit = Physics2D.Raycast(transform.position, -transform.up, 1f);
        if (exit.collider != null && exit.collider.gameObject.name == "door")
        {
            Debug.Log("Loading Scene...");
            SceneManager.LoadScene("SampleScene");
        }*/
        if (transform.position == exitPos)
        {
            Debug.Log("Loading Scene...");
            SceneManager.LoadScene("SampleScene");
        }
    }
}
