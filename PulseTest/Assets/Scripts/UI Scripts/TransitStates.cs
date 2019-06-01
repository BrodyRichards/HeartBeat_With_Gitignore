using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TransitStates : MonoBehaviour
{
    public Button[] buttons;
    public GameObject[] starIndicators;
    private int currentIndex;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("In start Menu");
        currentIndex = 0;

        
    }

    void Update()
    {
        ButtonSwitch();
        if (Input.GetKey(KeyCode.Escape))
        {
            ExitGame();
        }

    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ButtonSwitch()
    {
        buttons[currentIndex].OnSelect(null);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentIndex += 1;
            currentIndex %= 2;

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
}
