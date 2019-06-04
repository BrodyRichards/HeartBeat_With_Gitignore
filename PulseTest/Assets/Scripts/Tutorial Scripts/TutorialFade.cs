using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialFade : MonoBehaviour
{
    public Animator fadeAnimator;
    private int levelToLoad;
    private bool isReady;
    private bool isCalledOnce;

    private void Awake()
    {
        isReady = false;
        isCalledOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (McExit.nextScene && !isCalledOnce)
        {

            FadeToLevel();
            StartCoroutine(LoadNextSceneAsync("LoadingScene"));
            isCalledOnce = true;
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
        isReady = true;
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

    IEnumerator LoadNextSceneAsync(string nstl)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(nstl);
        ao.allowSceneActivation = false;
        while (!ao.isDone)
        {
            //Output the current progress
            Debug.Log(ao.progress);

            // Check if the load has finished
            if (ao.progress >= 0.9f)
            {
                //Wait to you press the space key to activate the Scene
                if (isReady)
                    //Activate the Scene
                    ao.allowSceneActivation = true;
            }

            yield return null;
        }
    }

}
