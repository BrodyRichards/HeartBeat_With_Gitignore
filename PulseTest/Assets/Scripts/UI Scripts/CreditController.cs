using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditController : MonoBehaviour
{
    // Start is called before the first frame update

    private bool end;
    private bool callOnce;
    void Start()
    {
        end = false;
        callOnce = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (end && !callOnce)
        {
            callOnce = true;
            LoadingController.nextSceneToLoad = "TutorialScreen";
            SceneManager.LoadScene("StartScreen");
        }
    }

    public void TimeToEnd()
    {
        end = true;
    }
}
