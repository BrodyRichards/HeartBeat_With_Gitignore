using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioControl : MonoBehaviour
{
    // Start is called before the first frame update
    public static int currentMood = 0;
    public static bool isMusic = false;
    public ParticleSystem ps;
    private bool isBG;
    private SpriteRenderer sr;
    private enum Mood { idle, happy, sad, startled};
    [SerializeField] private AudioClip sadSong;
    [SerializeField] private AudioClip startleSong;
    [SerializeField] private AudioClip happySong;

    private AudioSource audioSource;
    private AudioSource backgroundMusic;

    public Sprite happy;
    public Sprite sad;
    public Sprite startled;
    public Sprite idle;

    Sprite[] sprites;
    AudioClip[] audioClips;
    Color[] particleColors;
    private void Start()
    {
        sprites = new Sprite[] { idle, happy, sad, startled };
        audioClips = new AudioClip[] { happySong, sadSong, startleSong };
        particleColors = new Color[] { Color.cyan, Color.magenta, Color.white };
        currentMood = (int)Mood.idle;

        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        backgroundMusic = GameObject.Find("/GameController").GetComponent<AudioSource>();

        isBG = true;
        ps.Stop();

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
            ps.Stop();

            TurnBgOn();
            
            ResetThisGuy();
           
        }
        
    }

    private void ChangeMusic()
    {
        if (Input.GetKeyDown(KeyCode.Space)|| Input.GetMouseButtonDown(1))
        {
            

            currentMood = currentMood % 3 + 1;

            audioSource.clip = audioClips[(currentMood - 1) % 3];

            sr.sprite = sprites[currentMood];

            audioSource.Play();

            EmitParticles();
                
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

    private void EmitParticles()
    {

        ps.startColor = particleColors[currentMood - 1];
        ps.Play();
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
