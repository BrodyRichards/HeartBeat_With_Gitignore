using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioControl : MonoBehaviour
{
    // Start is called before the first frame update

    
    public static int currentMood = 0;
    private SpriteRenderer sr;
    private enum Mood { sad, intense, happy};
    [SerializeField] private AudioClip sadSong;
    [SerializeField] private AudioClip intenseSong;
    [SerializeField] private AudioClip happySong;

    private AudioSource audioSource;

    public Sprite happy;
    public Sprite sad;
    public Sprite intense;

    Sprite[] sprites;
    AudioClip[] audioClips;
    private void Start()
    {
        sprites = new Sprite[] { sad, intense, happy };
        audioClips = new AudioClip[] { sadSong, intenseSong, happySong };
        currentMood = (int)Mood.sad;

        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();

    }

    private void OnMouseDown()
    {
       
        if (enabled)
        {
            currentMood = (currentMood + 1) % 3;

            audioSource.clip = audioClips[currentMood];
            sr.sprite = sprites[currentMood];

            audioSource.Play();
        }
        else
        {
            Debug.Log("disabled");
        }
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
