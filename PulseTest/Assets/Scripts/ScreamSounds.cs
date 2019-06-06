using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamSounds : MonoBehaviour
{
    private AudioSource ass;
    public AudioClip[] screamClips;
    public static AudioClip scream;
    // Start is called before the first frame update
    void Start()
    {
        ass = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (NPCs.playScreamSound)
        {
            playScream();

        }
    }

    private void playScream()
    {
        NPCs.playScreamSound = false;
        int ran = Random.Range(0, screamClips.Length);
        scream = screamClips[ran];
        ass.clip = scream;
        ass.Play();
    }
}
