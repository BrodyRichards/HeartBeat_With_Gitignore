using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TransitStates : MonoBehaviour
{
    public Text loadingText;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("In start Menu");
        loadingText.enabled = false;
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void StartGame()
    {
        loadingText.enabled = true;
        StartCoroutine(LoadAsyncScene(1));
    }


    IEnumerator LoadAsyncScene(int nextSceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneIndex, LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            //slider.value = progress;
            if (progress > 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
