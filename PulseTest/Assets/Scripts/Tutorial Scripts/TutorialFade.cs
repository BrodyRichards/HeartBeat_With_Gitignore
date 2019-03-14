using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialFade : MonoBehaviour
{
    public Animator fadeAnimator;
    private int levelToLoad;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("4")){
            FadeToLevel(2);
        }
    }

    public void FadeToNextLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void FadeToLevel (int levelIndex)
    {
        levelToLoad = levelIndex;
        fadeAnimator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
