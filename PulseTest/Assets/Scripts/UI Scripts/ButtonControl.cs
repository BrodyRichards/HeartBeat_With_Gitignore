using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour
{
    public Button resume;
    public Button reset;
    public Button exit;

    public Button[] buttons;

    private KeyCode kcForButton;
    private int currentIndex;
    private Button selected;

    private Color unselectedColor = Color.red;
    private Color selectedColor = new Color(0.6117f, 0.8705f, 0.8821f);

    // Start is called before the first frame update
    void Start()
    {
        kcForButton = KeyCode.Space;
        buttons = new Button[] { resume, reset, exit }; //0, 1, 2
        currentIndex = 0;
        
       
    }

    // Update is called once per frame
    void Update()
    {

        buttons[currentIndex].OnSelect(null);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentIndex = currentIndex==0 ? 2 : currentIndex -1;
           
            EnableThisDisableRest(currentIndex);
            
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex = (currentIndex+1) % 3;
           
            
            EnableThisDisableRest(currentIndex);

            
        }

        if (Input.GetKey(KeyCode.Return))
        {
            buttons[currentIndex].onClick.Invoke();
        }


    }

    void ButtonHint(Button butt)
    {
        ColorBlock col = butt.colors;
        col.normalColor = new Color(0.6117f, 0.8705f, 0.8821f);
        butt.colors = col;
        //butt.colors = new Color(0.6117f, 0.8705f, 0.8821f);
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
    }
}
