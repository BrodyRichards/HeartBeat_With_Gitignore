using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconControl : MonoBehaviour
{
    public static bool journalActivated = false;
    public static bool journalTweening = false;
    public static bool bringTheIconsIn = false;
    public Animator animator;
    public GameObject journal;
    [SerializeField]
    private Image rabbitIcon;
    [SerializeField]
    private Image ballIcon;
    [SerializeField]
    private Image musicIcon;
    [SerializeField]
    private Image journalIcon;
    private List<Image> icons;
    private Color red = new Color(0f, 0.38f, 0.9f);
    private Color green = new Color(1f, 0.7007f, 0f);

    
    // Start is called before the first frame update
    void Start()
    {
        icons = new List<Image>{ rabbitIcon, ballIcon, musicIcon};
        journal.SetActive(false);
        ToggleIcons(false);
        animator.SetBool("newAccom", false);

    }

    // Update is called once per frame
    void Update()
    {
        if (bringTheIconsIn) { ToggleIcons(true); }
        foreach(var img in icons)
        {
            if ( icons.IndexOf(img) + 1 == characterSwitcher.charChoice)
            {
                
                Rescale(img, 40f);

                if (Input.GetKey(Control.positiveAction))
                {
                    ChangeColor(img, green);
                }
                else if (Input.GetKey(Control.negativeAction))
                {
                    ChangeColor(img, red);
                }
                else
                {
                    ChangeColor(img, Color.white);
                }
            }
            else
            {
                
                Rescale(img, 30f);
            }
        }

        if (journalTweening)
        {
            animator.SetBool("newAccom", true);
            
        }
        else
        {
            animator.SetBool("newAccom", false);
        }


        if (journalActivated)
        {
            
            journal.SetActive(true);
        }
        else
        {
            
            journal.SetActive(false);
        }
    }

    void ChangeColor(Image im, Color col)
    {
        
        im.color = col;
    }

    void Rescale(Image im, float pixel)
    {
        im.rectTransform.sizeDelta = new Vector2(pixel, pixel*2f);
    }

    private void ToggleIcons(bool boo)
    {
        foreach (var i in icons) { i.enabled = boo; }
        journalIcon.enabled = boo;
    }
    
    
}
