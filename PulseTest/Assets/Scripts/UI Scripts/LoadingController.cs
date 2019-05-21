using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingController : MonoBehaviour
{
    public GameObject loadingBar;
    public GameObject loadingText;
    public static string nextSceneToLoad = "TutorialScreen";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadNextSceneAsync(nextSceneToLoad));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator LoadNextSceneAsync(string nstl)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(nstl);
        ao.allowSceneActivation = false;
        while (!ao.isDone)
        {
            //Output the current progress
            loadingText.GetComponent<TextMeshProUGUI>().text = (ao.progress * 100) + "%";
            Debug.Log(ao.progress);

            // Check if the load has finished
            if (ao.progress >= 0.9f)
            {
                //Wait to you press the space key to activate the Scene
                if (Input.GetKeyDown(KeyCode.Space))
                    //Activate the Scene
                    ao.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
