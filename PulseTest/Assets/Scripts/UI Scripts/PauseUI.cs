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
        thisSceneIndex = SceneManager.GetActiveScene().buildIndex;
        pauseUI.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        StartCoroutine(LoadAsyncScene(thisSceneIndex));
        IsPaused = false;
        Time.timeScale = 1f;
        IconControl.journalActivated = false;
        characterSwitcher.charChoice = -1;
        RadioControl.isMusic = false;
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
        characterSwitcher.charChoice = 3;
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
