using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SneezeBubble : MonoBehaviour
{
    public Image sneezeBubble;
    Vector3 pos;
    Vector3 offset;

    private float time;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        sneezeBubble.GetComponent<Image>().gameObject.SetActive(false);
        offset = new Vector3(4f, 5f, 0f);
        time = 0f;
        timer = 0f;
    }

    void setTimer()
    {
        time = Time.fixedUnscaledTime;
        timer = time + 2.5f;
        NPCs.sneeze = false;
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.fixedUnscaledTime;
        pos = NpcInstantiator.allergyKidPos;
        sneezeBubble.transform.position = pos + offset;
        if (time >= timer)
        {
            sneezeBubble.GetComponent<Image>().gameObject.SetActive(false);
        }
        if (NPCs.sneeze)
        {
            Debug.Log("Sneezed " + NPCs.sneeze);
            setTimer();
            sneezeBubble.GetComponent<Image>().gameObject.SetActive(true);
        }
    }
}
