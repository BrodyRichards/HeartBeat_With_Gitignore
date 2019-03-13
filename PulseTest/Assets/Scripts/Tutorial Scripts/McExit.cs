using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class McExit : MonoBehaviour
{
    private float timer;
    public float busArrival;
    public bool printedAlready;
    public GameObject bus;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > busArrival)
        {
            bus.SetActive(true);
            if (!printedAlready)
            {
                printedAlready = true;
                Debug.Log("Bus is here, foo");
            }

            if (Input.GetKeyDown(Control.evacuate))
            {
                Debug.Log("Loading Scene...");
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    //Function to exit on collision with door
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "door")
        {
            Debug.Log("Loading Scene...");
            SceneManager.LoadScene("SampleScene");
        }
    }*/
}
