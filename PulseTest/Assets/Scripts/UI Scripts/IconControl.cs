﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconControl : MonoBehaviour
{
    public static bool journalActivated = false;

    [SerializeField]
    private Image rabbitIcon;
    [SerializeField]
    private Image ballIcon;
    [SerializeField]
    private Image musicIcon;
    [SerializeField]
    private Image journalIcon;
    private List<Image> icons;
    // Start is called before the first frame update
    void Start()
    {
        icons = new List<Image>{ rabbitIcon, ballIcon, musicIcon};
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var img in icons)
        {
            if ( icons.IndexOf(img) + 1 == characterSwitcher.charChoice)
            {
                ChangeAlpha(img, 1.0f);
                Rescale(img, 80f);
            }
            else
            {
                ChangeAlpha(img, 0.5f);
                Rescale(img, 50f);
            }
        }

        if (journalActivated)
        {
            ChangeAlpha(journalIcon, 1.0f);
        }
        else
        {
            ChangeAlpha(journalIcon, 0.5f);
        }
    }

    void ChangeAlpha(Image im, float alpha)
    {
        var colour = im.color;
        colour.a = alpha;
        im.color = colour;
    }

    void Rescale(Image im, float pixel)
    {
        im.rectTransform.sizeDelta = new Vector2(pixel, pixel);
    }
}