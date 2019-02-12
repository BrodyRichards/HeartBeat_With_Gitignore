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
        backgroundMusic = GameObject.Find("/GameController").GetComponent<AudioSource>();

        isBG = true;

    }

    // Update is called once per frame
    private void Update()
    {
        
       
        if (characterSwitcher.isMusicGuyInCharge)
        {
            ChangeMusic();

            TurnBgOff();
            
            UIControl();

            
        }
        else
        {

            TurnBgOn();
            
            ResetThisGuy();
           
        }
        
    }

    private void ChangeMusic()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentMood = currentMood % 3 + 1;

            audioSource.clip = audioClips[(currentMood - 1) % 3];

            sr.sprite = sprites[currentMood];

            audioSource.Play();
            isMusic = true;

        }
    }
    private void ResetThisGuy()
    {
        currentMood = (int)Mood.idle;
        sr.sprite = sprites[currentMood];
        audioSource.clip = null;
        audioSource.Pause();
    }


    private void TurnBgOff()
    {
        if (isBG && currentMood != (int)Mood.idle)
        {

            backgroundMusic.Pause();
            isBG = false;
        }
    }

    private void TurnBgOn()
    {
        if (!isBG)
        {
            backgroundMusic.Play();
            isBG = true;
        }
    }

    private void UIControl()
    {
        if (PauseUI.IsPaused)
        {
            audioSource.Pause();
        }
        else if (!PauseUI.IsPaused)
        {
            audioSource.UnPause();
        }
    }
}
