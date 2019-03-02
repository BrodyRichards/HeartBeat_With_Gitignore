using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioControl : MonoBehaviour
{
    // Start is called before the first frame update
    public static int currentMood = 0;
    public static bool isMusic = false;
    public static string musicListener = "";

    public ParticleSystem ps;
    private bool isBG;
    private SpriteRenderer sr;
    private enum Mood { idle, happy, sad};
    [SerializeField] private AudioClip sadSong;
    [SerializeField] private AudioClip happySong;
   

    private AudioSource audioSource;
    private AudioSource backgroundMusic;

    public Sprite happy;
    public Sprite sad;
    public Sprite startled;
    public Sprite idle;

    public float actionDist;

    Sprite[] sprites;
    AudioClip[] audioClips;
    Color[] particleColors;


    private void Start()
    {
        sprites = new Sprite[] { idle, happy, sad};
        audioClips = new AudioClip[] { happySong, sadSong };
        particleColors = new Color[] { Color.cyan, Color.white };
        currentMood = (int)Mood.idle;

        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        backgroundMusic = GameObject.Find("/GameController").GetComponent<AudioSource>();

        isBG = true;
        ps.Stop();
        actionDist = 4f;

    }

    // Update is called once per frame
    private void Update()
    {

        
        UIControl();
        if (characterSwitcher.isMusicGuyInCharge)
        {

            RaycastHit2D hit = Physics2D.CircleCast(transform.position, actionDist, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.tag == "MC")
            {
                ChangeMusic();
            }
            else if ( hit.collider != null && hit.collider.gameObject.tag == "Person")
            {
                musicListener = hit.collider.transform.name;
            }
            else
            {
                ResetThisGuy();
                hit.collider.transform.name = "";
            }



            TurnBgOff();



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
        if (Input.GetKey(Control.positiveAction))
        {
           
            currentMood = (int)Mood.happy;

            audioSource.clip = audioClips[0];

            sr.sprite = sprites[0];

            audioSource.Play();

            EmitParticles();

            isMusic = true;


            

        }
        else if (Input.GetKey(Control.negativeAction))
        {
            currentMood = (int)Mood.sad;

            audioSource.clip = audioClips[1];

            sr.sprite = sprites[1];

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
        EmoControl.CRunning = false;
    }

    private void EmitParticles()
    {

        ps.startColor = currentMood == 1? particleColors[0] : particleColors[1];
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
