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
        //SceneManager.LoadScene(0);
        LoadingController.nextSceneToLoad = "TutorialScreen";
        SceneManager.LoadScene("FinalScene");
        //if (IterationController.dayCount > 2)
        //{
        //    SceneManager.LoadScene("FinalScene");
        //}
        //else
        //{
        //    SceneManager.LoadScene("SampleScene");
        //}

    }

    public void FadeOut()
    {
        animator.Play("Fade_Out", 0, 0f);
        Invoke("OnFadeComplete", 3f);
    }

    public void FadeOutStay()
    {
        Debug.Log("FHDSJK");
        animator.Play("Fade_Out_Stay", 0, 0f);
    }
    public void FadeIn()
    {
        Debug.Log("HFSJK");
        animator.Play("Fade_In", 0, 0f);
    }
}