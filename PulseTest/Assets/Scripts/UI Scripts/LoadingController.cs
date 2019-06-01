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
    private int posX;
    private int percentage;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadNextSceneAsync(nextSceneToLoad));
        posX = -963;
        percentage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        posX += 5;
        percentage += 1;
        loadingBar.GetComponent<RectTransform>().localPosition = new Vector3(posX, 29, 0);
        //loadingText.GetComponent<TextMeshProUGUI>().text = percentage + "%";
    }

    IEnumerator LoadNextSceneAsync(string nstl)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(nstl);
        ao.allowSceneActivation = false;
        while (!ao.isDone)
        {
            //Output the current progress
            Debug.Log(ao.progress);

            // Check if the load has finished
            if (ao.progress >= 0.9f)
            {
                //Wait to you press the space key to activate the Scene
                if (loadingBar.GetComponent<RectTransform>().localPosition.x > 1200)
                    //Activate the Scene
                    ao.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
