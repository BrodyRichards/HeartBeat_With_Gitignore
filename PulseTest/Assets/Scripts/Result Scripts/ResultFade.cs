using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultFade : MonoBehaviour
{
    public Animator animator;
    private int levelToLoad;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("4")){
            FadeToNextLevel();
        }
    }

    public void FadeToNextLevel()
    {
        //FadeToLevel();
    }
    public void FadeToLevel (int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        //SceneManager.LoadScene(levelToLoad);
    }
}