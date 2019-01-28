using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TransitStates : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("In start Menu");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(LoadAsyncScene(1));
        }

    }

    IEnumerator LoadAsyncScene(int nextSceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneIndex, LoadSceneMode.Single);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
