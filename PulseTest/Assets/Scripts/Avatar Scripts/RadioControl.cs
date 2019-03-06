using System;
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

    private bool musicNoteCreated = false;
    private bool isBG;

    public LayerMask Carriers;
    public ParticleSystem ps;
   
    private SpriteRenderer sr;
    private enum Mood { happy, sad, idle};
    [SerializeField] private AudioClip sadSong;
    [SerializeField] private AudioClip happySong;
    [SerializeField] private GameObject happyMusicNote;
    [SerializeField] private GameObject sadMusicNote;
    private GameObject musicNoteObj;
    private AudioSource audioSource;
    private AudioSource backgroundMusic;

    public Sprite happy;
    public Sprite sad;
    public Sprite idle;

    public Sprite happyNote1;
    public Sprite happyNote2;
    public Sprite happyNote3;

    public Sprite sadNote1;
    public Sprite sadNote2;
    public Sprite sadNote3;


    public static float actionDist;


    Sprite[] sprites;
    Sprite[] happyNoteSprites;
    Sprite[] sadNoteSprites;
    AudioClip[] audioClips;
    Color[] particleColors;


    private void Start()
    {
        sprites = new Sprite[] { happy, sad, idle};
        happyNoteSprites = new Sprite[] { happyNote1, happyNote2, happyNote3 };
        sadNoteSprites = new Sprite[] { sadNote1, sadNote2, sadNote3 };
        audioClips = new AudioClip[] { happySong, sadSong };
        particleColors = new Color[] { Color.yellow, Color.cyan };
        currentMood = (int) Mood.idle;

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
            DetectAction();
            DetectMusic();
        }
        else
        {
            
            // ps.Stop();
            TurnBgOn();
            ResetThisGuy();
            if (musicNoteCreated && musicNoteObj!= null)
            {
                Destroy(musicNoteObj);
                musicNoteCreated = false;
            }
        }
    }

    private void DetectAction()
    {
        if (Input.GetKeyDown(Control.positiveAction) && currentMood != 0)
        {
            PlaySong(0);
            TurnBgOff();
        }
        else if (Input.GetKeyDown(Control.negativeAction) && currentMood != 1)
        {
            PlaySong(1);
            TurnBgOff();
        }
        else if ((Input.GetKeyDown(Control.negativeAction) && currentMood == 1) || (Input.GetKeyDown(Control.positiveAction) && currentMood == 0))
        {
            ResetThisGuy();
            TurnBgOn();
        }
    }

    private void DetectMusic()
    {
        if (isMusic)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, actionDist, Carriers);

            //Check if array is empty or if there was anything collided with
            // Codes from Justin's Rabbit script 
            if (colliders.Length != 0)
            {
                Array.Reverse(colliders);
                foreach (Collider2D coll in colliders)
                {
                    if (coll.gameObject.tag == "MC" && !mcIsAffected)
                    {
                        if (!musicNoteCreated)
                        {
                            CreateMusicNote();
                        }
                        musicListener = coll.gameObject.name;
                        mcIsAffected = true;
                        // 3 seconds later call this function and reset MC 
                        Invoke("McNotAffected", 1f);
                    }
                    else if (coll.gameObject.tag == "Person" && !mcIsAffected && !npcIsAffected)
                    {
                        if (!musicNoteCreated)
                        {
                            CreateMusicNote();
                        }
                        npcIsAffected = true;
                        musicListener = coll.gameObject.name;
                        //Debug.Log(musicListener);
                        // 3 seconds later reset npc
                        Invoke("NpcNotAffected", 3f);
                        break;
                    }
                }
            }
        }
        else
        {
            // reset 
            mcIsAffected = false;
            npcIsAffected = false;
            musicListener = "";

        }

        if (musicNoteCreated && musicNoteObj!=null)
        {
            SendMusicToTarget(GameObject.Find(musicListener));
        }
        

    }
    // play songs, change sprites and particles according to the mood 0=happy 1=sad 
    private void PlaySong(int index)
    {
        currentMood = (index==0) ? (int)Mood.happy: (int)Mood.sad;

        audioSource.clip = audioClips[index];

        sr.sprite = sprites[index];

        audioSource.Play();

        //EmitParticles(index);

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
        isMusic = false;
        //ps.Stop();
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
        // doesne't send message from emoControl anymore 
        var msg = currentMood == 0 ? "Happy Song" : "Sad Song";
        MentalState.sendMsg(msg);
        mcIsAffected = false;
    }

    private void NpcNotAffected()
    {
        npcIsAffected = false;
    }

    private void SendMusicToTarget(GameObject target)
    {
        musicNoteObj.transform.position = Vector3.MoveTowards(musicNoteObj.transform.position, target.transform.position, 10f * Time.deltaTime);
        if (musicNoteObj.transform.position == target.transform.position)
        {
            Destroy(musicNoteObj);
            musicNoteCreated = false;
        }
        
    }

    //private float RoatateParticles(Vector3 myself, GameObject other)
    //{
    //    var dir = other.transform.position - myself;
    //    dir = other.transform.InverseTransformDirection(dir);
    //    var angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
    //    Debug.Log(" particle is rotating!! " + angle);
    //    return angle;
        
        
    //}

    private void CreateMusicNote()
    {
        if (currentMood == (int) Mood.happy)
        {
            musicNoteObj = InstantiateNoteObj(happyNoteSprites);
        }
        else if (currentMood == (int) Mood.sad)
        {
            musicNoteObj = InstantiateNoteObj(sadNoteSprites);
        }
        
        musicNoteCreated = true;
        
        
    }

    private GameObject InstantiateNoteObj(Sprite[] spriteArray)
    {
        GameObject go = Instantiate(happyMusicNote, transform.position, Quaternion.identity) as GameObject;
        SpriteRenderer srsr = go.GetComponent<SpriteRenderer>();
        var index = UnityEngine.Random.Range(0, 3);
        srsr.sprite = spriteArray[index];
        return go;
    }
}
