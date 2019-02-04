using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioControl : MonoBehaviour
{
    // Start is called before the first frame update

    
    public static int currentMood = 0;
    public static bool isMusic = false;
    public bool isBG;
    private SpriteRenderer sr;
    private enum Mood { idle, sad, intense, happy};
    [SerializeField] private AudioClip sadSong;
    [SerializeField] private AudioClip intenseSong;
    [SerializeField] private AudioClip happySong;

    private AudioSource audioSource;
    private AudioSource backgroundMusic;

    public Sprite happy;
    public Sprite sad;
    public Sprite intense;
    public Sprite idle;

    Sprite[] sprites;
    AudioClip[] audioClips;
    private void Start()
    {
        sprites = new Sprite[] { idle, sad, intense, happy };
        audioClips = new AudioClip[] { sadSong, intenseSong, happySong };
        currentMood = (int)Mood.idle;

        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        backgroundMusic = GameObject.Find("/GameObject").GetComponent<AudioSource>();

        isBG = true;

    }

    private void OnMouseDown()
    {
        if (characterSwitcher.isMusicGuyInCharge)
        {
           

            currentMood = currentMood % 3 + 1;
            
            audioSource.clip = audioClips[(currentMood -1) % 3];
            
            sr.sprite = sprites[currentMood];

            Debug.Log("CURRENT CURRY" + currentMood);

            audioSource.Play();
            isMusic = true;
        }
        else
        {
            
            Debug.Log("disabled");
        }
        


    }

    // Update is called once per frame
    void Update()
    {
        if (characterSwitcher.isMusicGuyInCharge)
        {

            if (isBG && currentMood!=(int)Mood.idle)
            {
               
                backgroundMusic.Pause();
                isBG = false;
            }

            if (PauseUI.IsPaused)
            {
                audioSource.Pause();
            }
            else if (!PauseUI.IsPaused)
            {
                audioSource.UnPause();
            }
        }
        else
        {
            if (!isBG)
            {
                backgroundMusic.Play();
                isBG = true;
            }
            
            currentMood = (int)Mood.idle;
           

            sr.sprite = sprites[currentMood];
            audioSource.clip = null;
            audioSource.Pause();
        }
        
    }
}
