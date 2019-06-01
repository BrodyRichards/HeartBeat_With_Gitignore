using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JournalText : MonoBehaviour
{
    private TextMeshProUGUI tmpug;
    // Start is called before the first frame update
    void Awake()
    {
        tmpug = GetComponent<TextMeshProUGUI>();
        tmpug.text = TutorialJournal.tutText;
    }
}
