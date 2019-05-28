using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFade : MonoBehaviour
{
    public Animator animator;
    //public IterationController iterationController;
    private int levelToLoad;
    public static bool readyToLeave = false;
    private bool isIncremented;

    private void Awake()
    {
        readyToLeave = false;
        isIncremented = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(readyToLeave){
            FadeToNextLevel();
        }
    }

    public void FadeToNextLevel()
    {
        //Prepare next day information
        //IterationController.dayCount++;

        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeToLevel (int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
