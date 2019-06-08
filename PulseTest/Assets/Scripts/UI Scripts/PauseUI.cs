using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    // thanks to Brackey's tutorial
    // https://www.youtube.com/watch?v=JivuXdrIHK0

    public static bool IsPaused;
    public static bool reset;
    public GameObject pauseUI;
    private int thisSceneIndex;

    private void Awake()
    {
        IsPaused = false;
        reset = false;
    }

    private void Start()
    {

        Time.timeScale = 1f;
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

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void BackToMenu()
    {
        reset = true;
        SceneManager.LoadScene(0);
    }

}
