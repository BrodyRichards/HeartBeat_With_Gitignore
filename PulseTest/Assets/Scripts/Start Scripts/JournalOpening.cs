using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalOpening : MonoBehaviour
{
    public GameObject leftPage;
    private Animator leftOpen;
    public GameObject rightPage;
    private Animator rightOpen;

    public static bool cameraMove = false;
    // Start is called before the first frame update
    void Start()
    {
        leftOpen = leftPage.GetComponent<Animator>();
        rightOpen = rightPage.GetComponent<Animator>();

        Invoke("playAnim", 3f);
    }

    void playAnim()
    {
        leftOpen.Play("LeftSideOpen");
        rightOpen.Play("RightSideOpen");
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
