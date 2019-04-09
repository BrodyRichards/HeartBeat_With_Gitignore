using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeEmo : MonoBehaviour
{
    public ParticleSystem ps;
    private ParticleSystem.EmissionModule em;
    private ParticleSystem.MainModule pm;

    public Material happyEmoParticle;
    public Material sadEmoParticle;
    public Material angryEmoParticle;

    public static bool emitParticleNow;
    // Start is called before the first frame update
    private void Awake()
    {
        emitParticleNow = false;    
    }

    void Start()
    {
        em = ps.emission;
        pm = ps.main;
        em.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (emitParticleNow)
        {
            ChooseParticleColor();
            
            em.enabled = true;
            Invoke("PauseEmoPs", 0.5f);
        }
    }

    private void ChooseParticleColor()
    {
        if (EmoControl.currentEffect == 1)
        {
            ps.GetComponent<ParticleSystemRenderer>().material = happyEmoParticle;
            pm.startColor = Color.yellow;
        }
        else if (EmoControl.currentEffect == 2)
        {
            ps.GetComponent<ParticleSystemRenderer>().material = sadEmoParticle;
            pm.startColor = Color.cyan;
        }
        else
        {
            ps.GetComponent<ParticleSystemRenderer>().material = angryEmoParticle;
            pm.startColor = Color.red;
        }
    }

    private void PauseEmoPs()
    {

        em.enabled = false;
        emitParticleNow = false;
    }
}
