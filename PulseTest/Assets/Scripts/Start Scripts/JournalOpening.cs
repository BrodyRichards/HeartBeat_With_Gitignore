﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalOpening : MonoBehaviour
{
    public GameObject leftPage;
    private Animator leftOpen;
    private AudioSource turnPage;
    public GameObject rightPage;
    private Animator rightOpen;

    public static bool cameraMove = false;
    // Start is called before the first frame update
    void Start()
    {
        leftOpen = leftPage.GetComponent<Animator>();
        rightOpen = rightPage.GetComponent<Animator>();
        turnPage = leftPage.GetComponent<AudioSource>();

        Invoke("playAnim", 3f);
    }

    void playAnim()
    {
        leftOpen.Play("LeftSideOpen");
        rightOpen.Play("RightSideOpen");
        //leftPage.GetComponent<AudioSource>().Play();
        turnPage.Play();
        Invoke("cameraBool", 1f);
    }

    void cameraBool()
    {
        cameraMove = true;
    }

    void Update()
    {
        
    }
}
