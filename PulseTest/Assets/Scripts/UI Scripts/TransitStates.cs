using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TransitStates : MonoBehaviour
{
    public Text loadingText;
    public Text spaceText;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("In start Menu");
        loadingText.enabled = false;
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        loadingText.enabled = true;
        spaceText.enabled = false;

        SceneManager.LoadScene(1);
    }



}
