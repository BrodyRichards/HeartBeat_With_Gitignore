using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingTxt : MonoBehaviour
{
    public Text textToFade;
    private bool faded = false;
    private float tweenDuration = 2.0f;
    private float tweenUpperbound = 0.95f;
    private float tweenLowerbound = 0.2f;
    
    void Start()
    {
        Debug.Log(textToFade.canvasRenderer.GetAlpha());

    }

    
    private void TweenTxt()
    {
        // if the text is currently not faded, make it fade by decreasing the alpha gradually 
        if (!faded)
        {
            textToFade.CrossFadeAlpha(0, tweenDuration, false);
            // once it reaches the lower bound alpha value, set toggle faded to true
            if (textToFade.canvasRenderer.GetAlpha() < tweenLowerbound)
            {
                faded = true;
            }
        }
        else
        {
            // start increasing alpha until the upperbound 
            textToFade.CrossFadeAlpha(1, tweenDuration, false);
            if (textToFade.canvasRenderer.GetAlpha() > tweenUpperbound)
            {
                faded = false;
            }
        }
        
    }
    void Update()
    {
        TweenTxt();
    }
}
