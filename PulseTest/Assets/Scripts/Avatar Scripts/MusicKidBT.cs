using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicKidBT : MonoBehaviour
{
    private enum Mood { happy, sad, idle };

    public static int currentMood;
    public static string musicListener = "";
    public static bool mcIsAffected = false;
    public static bool npcIsAffected = false;
    public static bool isMusic = false;
    public static float actionDist;

    private bool musicNoteCreated;
    private bool emitParticleNow;
    private bool isBG;
    private bool foundTheMC;

    private readonly float mcAffectedInterval = 3f;

    public GameObject notes;
    public Sprite[] happyNotesSprite;
    public Sprite[] sadNotesSprite;
    public AudioSource[] theAudioSources;

    public LayerMask Carriers;
    public ParticleSystem ps;
    public Material happyNoteMat;
    public Material sadNoteMat;

    private AudioSource happySong;
    private AudioSource sadSong; 
    private AudioSource backgroundMusic;

    private Animator anim;

    private ParticleSystem.EmissionModule em;

    private Node musicKidBT;

    private int mcHappySongCounter;
    private int mcSadSongCounter;

    private int songQueued;
    // Start is called before the first frame update
    void Start()
    {
        mcHappySongCounter = 0;
        mcSadSongCounter = 0;
        actionDist = 2f;
        musicNoteCreated = false;
        currentMood = (int)Mood.idle;
        musicKidBT = CreateBehaviorTree();
        sadSong = GetComponents<AudioSource>()[1];
        happySong = GetComponents<AudioSource>()[0];
        theAudioSources = new AudioSource[]{ sadSong, happySong};
        backgroundMusic = GameObject.Find("/GameController").GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        em = ps.emission;
        songQueued = 0;
        em.enabled = false;
        isBG = true;
    }

    Node CreateBehaviorTree()
    {

        Leaf playCheck = new Leaf(PassedFunc: DetectTurnOn);
        Leaf playMusic = new Leaf(PassedFunc: PlayMusic);
        Leaf stopCheck = new Leaf(PassedFunc: DetectTurnOff);
        Inverter iv = new Inverter(stopCheck);

        Sequence playMusicSQ = createSeqRoot(playCheck, playMusic);

        Leaf mcCheck = new Leaf(FindMC);
        Leaf affectMC = new Leaf(AffectMC);

        Sequence affectMcSQ = createSeqRoot(mcCheck, affectMC);

        Leaf npcCheck = new Leaf(FindNpc);
        Leaf affectNPC = new Leaf(AffectNpc);

        Sequence affectNpcSQ = createSeqRoot(npcCheck, affectNPC);

        Leaf updateCheck = new Leaf(SongTimeCount);
        Leaf updateEvent = new Leaf(UpdateEvent);


        Selector playingSEL = createSelRoot(playMusicSQ, stopCheck);
        Selector affectingSEL = createSelRoot(affectMcSQ, affectNpcSQ);
        Sequence eventHappeningSQ = createSeqRoot(updateCheck, updateEvent);

        Sequence root = createSeqRoot(playingSEL, affectingSEL, eventHappeningSQ);
        return root;
    }

    // Update is called once per frame
    void Update()
    {
        //UIControl();
        if (!IconControl.journalActivated && !PauseUI.IsPaused)
        {
            musicKidBT.Evaluate();
        }

    }


    private NodeStatus DetectTurnOn()
    {
        if (characterSwitcher.isMusicGuyInCharge && !Movement.timeToLeave)
        {
            if (Input.GetKeyDown(Control.positiveAction) && currentMood != (int)Mood.happy)
            {
                songQueued = 0;
                return NodeStatus.SUCCESS;
            }
            else if (Input.GetKeyDown(Control.negativeAction) && currentMood != (int)Mood.sad)
            {
                songQueued = 1;

                return NodeStatus.SUCCESS;

            }
        }
        return NodeStatus.FAILURE;
            
    }

    private NodeStatus PlayMusic()
    {

        PlaySong(songQueued);
        TurnBgOff();
        anim.SetTrigger("click");
        return NodeStatus.SUCCESS;
    }

    private NodeStatus DetectTurnOff()
    {
        if (!characterSwitcher.isMusicGuyInCharge || Movement.timeToLeave)
        {

            ResetThisGurl();
            return NodeStatus.FAILURE;

        }
        if (!isMusic)
        {
            return NodeStatus.FAILURE;
        }

        if ((Input.GetKeyDown(Control.negativeAction) && currentMood == (int)Mood.sad) || (Input.GetKeyDown(Control.positiveAction) && currentMood == (int)Mood.happy))
        {

            anim.SetTrigger("click");
            ResetThisGurl();
            return NodeStatus.FAILURE;
        }

        

        return NodeStatus.SUCCESS;
    }




    private NodeStatus ResetThisGurl()
    {
        TurnBgOn();
        currentMood = (int)Mood.idle;
        foreach(var ass in theAudioSources)
        {
            ass.Pause();
        }
        isMusic = false;
        DestroyRemainingNote();
        mcSadSongCounter *= 0;
        mcHappySongCounter *= 0;
        em.enabled = false;
        mcIsAffected = false;
        npcIsAffected = false;
        musicListener = "";
        return NodeStatus.FAILURE;

    }
    private NodeStatus FindMC()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, actionDist, Carriers);
        //Check if array is empty or if there was anything collided with
        // Codes from Justin's Rabbit script 
        if (colliders.Length != 0 && isMusic)
        {
            Array.Reverse(colliders);
            foreach (Collider2D coll in colliders)
            {

                if (coll.gameObject.tag == "MC")
                {
                    musicListener = coll.gameObject.name;
                    return NodeStatus.SUCCESS;
                }

            }
        }
        MCAffectReset();
        return NodeStatus.FAILURE;

    }

    private NodeStatus FindNpc()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, actionDist, Carriers);
        if (colliders.Length != 0 && isMusic)
        {
            Array.Reverse(colliders);
            foreach (Collider2D coll in colliders)
            {
                //Debug.Log("We detect" + coll.gameObject.name);
                //Debug.Log("ohhh " + coll.gameObject.tag);
                if (coll.gameObject.tag == "Person" && !npcIsAffected)
                {
                    musicListener = coll.gameObject.name;
                    Debug.Log("NPC detected");

                    return NodeStatus.SUCCESS;
                }

            }
        }
        //musicListener = "";
        return NodeStatus.FAILURE;

    }
    private NodeStatus AffectMC()
    {
        mcIsAffected = true;
        SendNotesToMusicListenerOrDestroy();
        if (currentMood == (int)Mood.happy)
        {
            mcHappySongCounter += 1;
            mcSadSongCounter *= 0;
        }
        else
        {
            mcHappySongCounter *= 0;
            mcSadSongCounter += 1;
        }
        if (!musicNoteCreated)
        {
            CreateMusicNote();
            var musicCreatedInterval = (currentMood == 0 ? 0.3f : 0.6f);
            Invoke("ResetMusicNote", musicCreatedInterval);

        }
        return NodeStatus.SUCCESS;
    }

    private NodeStatus AffectNpc()
    {
        npcIsAffected = true;
        Invoke("NpcNotAffected", 0.5f);
        Debug.Log("IN AFFECT NPC");
        return NodeStatus.SUCCESS;
    }







    private NodeStatus SongTimeCount()
    {
        if (mcSadSongCounter > 200 || mcHappySongCounter > 200)
        {
            return NodeStatus.SUCCESS;
        }
        return NodeStatus.FAILURE;
    }

    private NodeStatus UpdateEvent()
    {

        var msg = "";
        if (currentMood == (int)Mood.happy)
        {
            msg = "Happy Song";
        }
        else if (currentMood == (int)Mood.sad)
        {
            msg = "Sad Song";
        }
        else
        {
            Debug.Log("How did this happen?");
            return NodeStatus.FAILURE;
        }
        MentalState.sendMsg(msg);
        mcSadSongCounter *= 0;
        mcHappySongCounter *= 0;
        return NodeStatus.SUCCESS;
    }

    //-----------------Helper function-----------------------
    Sequence createSeqRoot(params Node[] nodeList)
    {
        List<Node> rootOrder = new List<Node>();

        foreach (Node n in nodeList)
        {
            rootOrder.Add(n);
        }

        Sequence newSeq = new Sequence(rootOrder);

        return newSeq;
    }

    Selector createSelRoot(params Node[] nodeList)
    {
        List<Node> rootOrder = new List<Node>();

        foreach (Node n in nodeList)
        {
            rootOrder.Add(n);
        }

        Selector newSel = new Selector(rootOrder);

        return newSel;
    }

    private void UIControl()
    {
        if (PauseUI.IsPaused)
        {
            theAudioSources[songQueued].Pause();

        }
        else if (!PauseUI.IsPaused)
        {
            if ( theAudioSources[songQueued].time != 0f)
            {
                theAudioSources[songQueued].UnPause();
            }

        }
    }



    private void ChooseParticleColor()
    {
        if (currentMood == (int)Mood.happy)
        {

            ps.GetComponent<ParticleSystemRenderer>().material = happyNoteMat;
        }
        else if (currentMood == (int)Mood.sad)
        {

            ps.GetComponent<ParticleSystemRenderer>().material = sadNoteMat;
        }

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

    private void PlaySong(int index)
    {
        currentMood = (index == 0) ? (int)Mood.happy : (int)Mood.sad;


        if (theAudioSources[songQueued].time != 0f)
        {
            theAudioSources[songQueued].UnPause();
        }
        else
        {
            theAudioSources[songQueued].Play();
        }

        int temp = Math.Abs(songQueued - 1);
        theAudioSources[temp].Pause();






        isMusic = true;

        ChooseParticleColor();

        em.enabled = true;
        //emitParticleNow = true;
    }

    private void SendNotesToMusicListenerOrDestroy()
    {
        if (musicListener != "")
        {
            SendMusicToTarget(GameObject.Find(musicListener));
        }
        else
        {
            DestroyRemainingNote();
        }
    }

    private void SendMusicToTarget(GameObject target)
    {
        var tempList = GameObject.FindGameObjectsWithTag("MusicNote");
        foreach (var temp in tempList)
        {
            if (Vector2.Distance(temp.transform.position, target.transform.position) > actionDist + 5f)
            {
                Destroy(temp);
                continue;
            }
            temp.transform.position = Vector3.MoveTowards(temp.transform.position, target.transform.position, 10f * Time.deltaTime);
            if (temp.transform.position == target.transform.position)
            {
                Destroy(temp);

            }
        }


    }

    private void ResetMusicNote()
    {
        musicNoteCreated = false;
    }

    private void MCAffectReset()
    {
        mcIsAffected = false;
        mcSadSongCounter *= 0;
        mcHappySongCounter *= 0;
        DestroyRemainingNote();

    }

    private GameObject InstantiateNoteObj(Sprite[] spriteArray)
    {
        GameObject go = Instantiate(notes, transform.position, Quaternion.identity) as GameObject;
        SpriteRenderer srsr = go.GetComponent<SpriteRenderer>();
        var index = UnityEngine.Random.Range(0, 3);
        srsr.sprite = spriteArray[index];
        var col = srsr.color;
        col.a = 1.0f;
        srsr.color = col;
        return go;
    }

    private void DestroyRemainingNote()
    {
        var temp = GameObject.FindGameObjectsWithTag("MusicNote");
        foreach (var t in temp) { Destroy(t); }
    }

    private void CreateMusicNote()
    {
        if (currentMood == (int)Mood.happy)
        {
            InstantiateNoteObj(happyNotesSprite);
        }
        else if (currentMood == (int)Mood.sad)
        {
            InstantiateNoteObj(sadNotesSprite);
        }

        musicNoteCreated = true;


    }

    private void NpcNotAffected()
    {
        npcIsAffected = false;
    }

    //private Collider[] ColliderSort(Collider[] before_sort )
    //{
    //    Collider[] after_sort = new Collider[before_sort.Length];

    //    foreach(var col in before_sort)
    //    {

    //    }

    //    return after_sort;
    //}
}
