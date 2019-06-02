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
        if (McExit.nextScene)
        {

            FadeToLevel();
        }


        
    }

    public void FadeToNextLevel()
    {
        FadeToLevel();
    }
    public void FadeToLevel()
    {
        LoadingController.nextSceneToLoad = "SampleScene";
        fadeAnimator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene("loadingScene");
    }

    public void FadeOut(){
        fadeAnimator.Play("Fade_Out",0,0f);
        
    }

    public void FadeOutStay()
    {
        fadeAnimator.Play("Fade_Out_Stay", 0, 0f);
    }
    public void FadeIn(){
        fadeAnimator.Play("Fade_In",0,0f);
    }

}
