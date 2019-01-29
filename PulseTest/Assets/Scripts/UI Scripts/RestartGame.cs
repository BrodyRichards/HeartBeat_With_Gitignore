using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    private int thisSceneIndex;
    // Start is called before the first frame update
    void Start()
    {
        //Retreive build index of the current scene 
        thisSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //Reload current Scene 
            StartCoroutine(LoadAsyncScene(thisSceneIndex)); 
        }
        else if (Input.GetKey(KeyCode.B))
        {
            //Go back to the previous scene 
            if (thisSceneIndex != 0)
            {
                StartCoroutine(LoadAsyncScene(thisSceneIndex - 1));
            }
           
        }


    }
    // This is good for loading screen 
    IEnumerator LoadAsyncScene(int nextSceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneIndex, LoadSceneMode.Single);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
