using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class McExit : MonoBehaviour
{
    private float timer;
    public float busArrival;
    public float momSpeech;
    public float momSpeechEnd;
    public bool printedAlready;
    public GameObject bus;
    public Image speechBubble;

    public static bool nextScene = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > busArrival && TutorialJournal.journalOpenOnce)
        {
            bus.SetActive(true); 
            if (!printedAlready)
            {
                printedAlready = true;
                Debug.Log("Bus is here, foo");
            }

            if (Input.GetKeyDown(Control.evacuate))
            {
                nextScene = true;
                //Debug.Log("next scene: " + nextScene);
                Debug.Log("Loading Scene...");
                //SceneManager.LoadScene("SampleScene");
            }
        }
        if (timer > momSpeech)
        {
            speechBubble.GetComponent<Image>().gameObject.SetActive(true);
        }
        if (timer > momSpeechEnd)
        {
            speechBubble.GetComponent<Image>().gameObject.SetActive(false);
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
