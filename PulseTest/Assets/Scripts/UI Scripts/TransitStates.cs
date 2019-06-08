using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TransitStates : MonoBehaviour
{
    public Button[] buttons;
    public GameObject[] starIndicators;
    public Image bg;
    public Image logo;
    public GameObject text;
    public GameObject cloud1;
    public GameObject cloud2;
    public GameObject cloud3;
    public GameObject fadeObj;
    private int currentIndex;
    private bool startLoading;
    private bool readyToLoad;
    private float cloudStep;
    private readonly float screenBoundLeftX = -686f;
    private readonly float screenBoundRightX = 686f;
    private Vector3 targetPos;
    private Vector3 targetPos2;
    private Vector3 targetPos3;
    



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("In start Menu");
        currentIndex = 0;
        readyToLoad = false;
        startLoading = false;
        targetPos = new Vector3(screenBoundLeftX, cloud1.GetComponent<RectTransform>().position.y, 0f);
        targetPos2 = new Vector3(screenBoundRightX, cloud2.GetComponent<RectTransform>().position.y, 0f);
        targetPos3 = new Vector3(100f, cloud3.GetComponent<RectTransform>().position.y, 0f);
    }

    void Update()
    {
        cloudStep = Time.deltaTime * 0.1f;
        MoveTheClouds(cloud1, targetPos);
        MoveTheClouds(cloud2, targetPos2);
        MoveTheClouds(cloud3, targetPos3);

        ButtonSwitch();
        if (Input.GetKey(KeyCode.Escape))
        {
            ExitGame();
        }

        if (startLoading)
        {
            text.SetActive(false);
            Color bgcol = bg.color;
            bgcol.a += 0.001f;
            bg.color = bgcol;



            if ( bgcol.a > 0.99f)
            {
                readyToLoad = true;
                startLoading = false;
            }
        }


    }

    public void StartGame()
    {
        //StartCoroutine(LoadNextSceneAsync("JumpIntoJournalScene"));
        Invoke("fadeOut", 1f);
        //Invoke("nextScene", 2f);
        startLoading = true;
        Debug.Log("hello~~");
    }

    public void nextScene()
    {
        SceneManager.LoadScene("JumpIntoJournalScene");
    }

    public void fadeOut()
    {
        var meh = fadeObj.GetComponent<StartFade>();
        meh.FadeOutStay();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ButtonSwitch()
    {
        buttons[currentIndex].OnSelect(null);
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentIndex += 1;
            currentIndex %= 2;

            EnableThisDisableRest(currentIndex);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentIndex == 1) currentIndex = 0;
            else { currentIndex = 1; }

            EnableThisDisableRest(currentIndex);
        }

        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Space))
        {
            buttons[currentIndex].onClick.Invoke();
        }
    }

    void EnableThisDisableRest(int index)
    {
        var sel = buttons[index];

        sel.OnSelect(null);
        foreach (var but in buttons)
        {
            if (!but.Equals(sel))
            {
                but.OnDeselect(null);
            }
        }

        for (var i = 0; i < 2; i++)
        {
            if (i != index)
            {
                starIndicators[i].SetActive(false);
            }
            else
            {
                starIndicators[i].SetActive(true);
            }
        }
    }

    private void MoveTheClouds(GameObject cloud, Vector3 target)
    {
        if (cloud.GetComponent<RectTransform>().position != target)
        {
            cloud.GetComponent<RectTransform>().position = 
            Vector2.MoveTowards(cloud.GetComponent<RectTransform>().position, target, cloudStep);
        }
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
                //Wait to you press the space key to activate the Scen
                if (readyToLoad) ao.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
