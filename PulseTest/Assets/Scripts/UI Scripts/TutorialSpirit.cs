﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpirit : MonoBehaviour
{
    private ParticleSystem ps;
    private ParticleSystem.EmissionModule em;

    private GameObject[] avatars;

    private GameObject MC;
    private GameObject possessed;
    // Start is called before the first frame update
    void Start()
    {
        ps = gameObject.GetComponent<ParticleSystem>();
        em = ps.emission;

        avatars = GameObject.FindGameObjectsWithTag("Avatars");
        MC = GameObject.FindGameObjectWithTag("MC");
        transform.position = MC.transform.position;
        possessed = MC;
    }

    // Update is called once per frame
    void Update()
    {
        var alarm = avatars[0];
        var curtain = avatars[1];
        var toy = avatars[2];

        transform.position = possessed.transform.position;

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            
            possessed = alarm;
            
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            possessed = curtain;
            
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            possessed = toy;
            
        }

        
        

        


    }

    private void Emite(){
        em.enabled = true;
    }
    private void StopEmite(){
        em.enabled = false;
    }
}
