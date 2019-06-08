using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicKidBubble : MonoBehaviour
{
    public Image musicBubble;
    Vector3 pos;
    Vector3 offset;

    private float time;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        musicBubble.GetComponent<Image>().gameObject.SetActive(false);
        offset = new Vector3(-4f, 5f, 0f);
        time = 0f;
        timer = 0f;
        pos = NpcInstantiator.musicKidPos;
        musicBubble.transform.position = pos + offset;
    }

    void setTimer()
    {
        time = Time.fixedUnscaledTime;
        timer = time + 2.5f;
        BallProjectile.musicKidTalk = false;
    }


    // Update is called once per frame
    void Update()
    {
        time = Time.fixedUnscaledTime;
        pos = NpcInstantiator.musicKidPos;
        musicBubble.transform.position = pos + offset;
        if (time >= timer)
        {
            musicBubble.GetComponent<Image>().gameObject.SetActive(false);
        }
        if (BallProjectile.musicKidTalk)
        {
            //Debug.Log("Sneezed " + NPCs.sneeze);
            setTimer();
            musicBubble.GetComponent<Image>().gameObject.SetActive(true);
        }
    }
}
