using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditController : MonoBehaviour
{
    // Start is called before the first frame update

    private float endTimer;
    void Start()
    {
        endTimer = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        endTimer -= Time.deltaTime;
        if (endTimer < 0f)
        {
            LoadingController.nextSceneToLoad = "TutorialScreen";
            SceneManager.LoadScene("StartScreen");
        }
    }
}
