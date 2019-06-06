using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingController : MonoBehaviour
{
    public GameObject loadingBar;
    public GameObject loadingText;
    public GameObject Charlie;
    public static string nextSceneToLoad = "TutorialScreen";
    private int posX;
    private bool goingToSchool;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadNextSceneAsync(nextSceneToLoad));
        posX = -963;
        goingToSchool = nextSceneToLoad == "SampleScene";

        if (goingToSchool)
        {
            posX = -900;
        }
        else
        {
            posX = 900;
            loadingBar.GetComponent<RectTransform>().localScale = new Vector3(-1f, 1f, 1f);
            Charlie.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (goingToSchool)
        {
            GoToSchool();
        }
        else
        {
            GoBackHome();
        }
        //loadingText.GetComponent<TextMeshProUGUI>().text = percentage + "%";
    }

    void GoToSchool()
    {
        posX += 5;
        loadingBar.GetComponent<RectTransform>().localPosition = new Vector3(posX, 29, 0);
        if (loadingBar.GetComponent<RectTransform>().localPosition.x > -57f)
        {
            Charlie.SetActive(false);
        }
    }

    void GoBackHome()
    {
        posX -= 5;
        loadingBar.GetComponent<RectTransform>().localPosition = new Vector3(posX, 29, 0);
        if (loadingBar.GetComponent<RectTransform>().localPosition.x < -57f)
        {
            Charlie.SetActive(true);
        }
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

                if ((loadingBar.GetComponent<RectTransform>().localPosition.x > 1200 && goingToSchool) ||
                (loadingBar.GetComponent<RectTransform>().localPosition.x < -1000 && !goingToSchool))
                    //Activate the Scene
                    ao.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
