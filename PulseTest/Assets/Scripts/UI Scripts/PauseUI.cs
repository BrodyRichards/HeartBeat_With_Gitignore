using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    // thanks to Brackey's tutorial
    // https://www.youtube.com/watch?v=JivuXdrIHK0

    public static bool IsPaused = false;
    public GameObject pauseUI;
    private int thisSceneIndex;

    private void Start()
    {
        
        pauseUI.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Control.pullPauseMenu))
        {
          
            
            
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {

        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void Pause()
    {

        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void Restart()
    {
        thisSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("restart the scene");
        StartCoroutine(LoadAsyncScene(thisSceneIndex));
        IsPaused = false;
        Time.timeScale = 1f;
        if (thisSceneIndex == 1) // the tutorial scene 
        {
            
        }
        else if (thisSceneIndex == 2)
        {
            
            characterSwitcher.charChoice = -1;
            MentalState.journalInProgress = true;
            RadioControl.isMusic = false;
        }
        else if (thisSceneIndex == 3)
        {
            
        }
        
    }

    public void Quit()
    {
        Application.Quit();
        //if (thisSceneIndex != 0)
        //{
        //    StartCoroutine(LoadAsyncScene(thisSceneIndex - 1));
        //}
        Time.timeScale = 1f;
        IsPaused = false;
    }
    public void BackToMenu()
    {
        StartCoroutine(LoadAsyncScene(0));
        IsPaused = false;
        Time.timeScale = 1f;
        characterSwitcher.charChoice = -1;
        RadioControl.isMusic = false;
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
