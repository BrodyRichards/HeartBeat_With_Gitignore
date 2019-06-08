using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicCarryOver : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if ("SampleScene" == SceneManager.GetActiveScene().name || "CreditScreen" == SceneManager.GetActiveScene().name)
        {
            Destroy(this.gameObject);
        }
        if (PauseUI.reset)
        {
            Destroy(this.gameObject);
            PauseUI.reset = false;
        }
    }
}
