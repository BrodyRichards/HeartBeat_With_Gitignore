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

    

    void Start()
    {

        currentMood = (int)Mood.sad;

        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();

    }

    private void OnMouseDown()
    {
        Sprite[] sprites = { sad, intense, happy };
        AudioClip[] audioClips = { sadSong, intenseSong, happySong };

        currentMood = (currentMood + 1) % 3;

        audioSource.clip = audioClips[currentMood];
        sr.sprite = sprites[currentMood];

       


        audioSource.Play();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
