using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartFade : MonoBehaviour
{
    public Animator fadeAnimator;
    private int levelToLoad;
    // Start is called before the first frame update
    void Update()
    {
        /*
        if (McExit.nextScene)
        {

            FadeToLevel();
        }
        */
    }
    public void FadeToNextLevel()
    {
        FadeToLevel();
    }
    public void FadeToLevel()
    {
        LoadingController.nextSceneToLoad = "JumpIntoJournalScene";
        fadeAnimator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene("loadingScene");
    }

    public void FadeOut()
    {
        fadeAnimator.Play("Fade_Out", 0, 0f);

    }

    public void FadeOutStay()
    {
        fadeAnimator.Play("Fade_Out_Stay", 0, 0f);
        FadeToLevel();
    }
    public void FadeIn()
    {
        fadeAnimator.Play("Fade_In", 0, 0f);
    }
    // Update is called once per frame
    
}
