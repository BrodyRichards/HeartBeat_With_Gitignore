using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioControl : MonoBehaviour
{
    // Start is called before the first frame update
    public static int currentMood = 0;
    public static string musicListener = "";
    public static bool mcIsAffected = false;
    public static bool npcIsAffected = false;
    public static bool isMusic = false;

    private bool isBG;

    public ParticleSystem ps;
   
    private SpriteRenderer sr;
    private enum Mood { idle, happy, sad};
    [SerializeField] private AudioClip sadSong;
    [SerializeField] private AudioClip happySong;
    private AudioSource audioSource;
    private AudioSource backgroundMusic;

    public Sprite happy;
    public Sprite sad;
    public Sprite idle;

    public float actionDist;

    Sprite[] sprites;
    AudioClip[] audioClips;
    Color[] particleColors;


    private void Start()
    {
        sprites = new Sprite[] { idle, happy, sad};
        audioClips = new AudioClip[] { happySong, sadSong };
        particleColors = new Color[] { Color.white, Color.cyan };
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

            AllTheStuff();


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
        

        
    }

    private void AllTheStuff()
    {
        if (Input.GetKey(Control.positiveAction))
        {
            if (!isMusic)
            {
                PlaySong(0);
            }
            else
            {
                ResetThisGuy();
            }
        }else if (Input.GetKey(Control.negativeAction))
        {
            if (!isMusic)
            {
                PlaySong(1);
            }
            else
            {
                ResetThisGuy();
            }
        }

        if (isMusic)
        {
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, actionDist, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.tag == "MC" && !mcIsAffected)
            {
                mcIsAffected = true;
                Invoke("McNotAffected", 3f);
            }else if ( hit.collider != null && hit.collider.gameObject.tag == "Person" && !mcIsAffected && !npcIsAffected)
            {
                npcIsAffected = true;
                musicListener = hit.collider.gameObject.name;
                Debug.Log(musicListener);
            }
        }
        else
        {
            mcIsAffected = false;
            npcIsAffected = false;
            musicListener = "";
            
        }

    }

    private void PlaySong(int index)
    {
        currentMood = index==0? (int)Mood.happy: (int)Mood.sad;

        audioSource.clip = audioClips[index];

        sr.sprite = sprites[index];

        audioSource.Play();

        EmitParticles(index);

        isMusic = true;
    }
   

    private void EmitParticles(int index)
    {

        ps.startColor = particleColors[index];
        ps.Play();
    }

    private void ResetThisGuy()
    {
        currentMood = (int)Mood.idle;
        sr.sprite = sprites[currentMood];
        audioSource.clip = null;
        audioSource.Pause();
        EmoControl.CRunning = false;
        isMusic = false;
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

    private void McNotAffected()
    {
        mcIsAffected = false;
    }
}
