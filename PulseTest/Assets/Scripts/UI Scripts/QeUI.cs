using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QeUI : MonoBehaviour
{
    public Sprite rQ;
    public Sprite rE;
    public Sprite bQ;
    public Sprite bE;
    public Sprite mQ;
    public Sprite mE;

    public GameObject Q;
    public GameObject E;

    public GameObject QEIcons;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (characterSwitcher.charChoice)
        {
            case 1:
                Q.GetComponent<Image>().sprite = rQ;
                E.GetComponent<Image>().sprite = rE;
                break;
            case 2:
                Q.GetComponent<Image>().sprite = bQ;
                E.GetComponent<Image>().sprite = bE;
                break;
            case 3:
                Q.GetComponent<Image>().sprite = mQ;
                E.GetComponent<Image>().sprite = mE;
                break;
            default:
                Q.GetComponent<Image>().sprite = null;
                E.GetComponent<Image>().sprite = null;
                break;

        }

        if (characterSwitcher.charChoice > 0 && characterSwitcher.charChoice < 1000)
        {
            QEIcons.SetActive(true);
        }
        else
        {
            QEIcons.SetActive(false);
        }
    }
}