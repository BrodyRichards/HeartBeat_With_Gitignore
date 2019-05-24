﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgSpirit : MonoBehaviour
{
    private ParticleSystem ps;
    private ParticleSystem.EmissionModule em;
    private GameObject MC;
    private GameObject Camera;
    private int flag;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        ps = gameObject.GetComponent<ParticleSystem>();
        ps.Play();
        em = ps.emission;
        em.rateOverDistance = 0;
        MC = GameObject.FindGameObjectWithTag("MC");
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        flag = 0;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
         
        if (Input.GetKeyDown(KeyCode.H)) { Emit(); }
        if(flag == 1){
            Debug.Log("Flag up");
            if (timer < 1.0f)
                {
                    timer += Time.deltaTime;
                    ps.transform.position = Camera.transform.position + new Vector3(25, 14, 0);
                    Debug.Log("Transforming");
                }
            if(timer > 1.0f){
                flag = 0;
                timer = 0;
                em.rateOverDistance = 0;
                Debug.Log("Resetting");
            }
        }
        if(flag ==0){
            ps.transform.position = MC.transform.position;
        }
    }
    public void Emit(){
        flag = 0;
        ps.transform.position = MC.transform.position;
        em.rateOverDistance = 10;
        //ps.transform.position += new Vector3(25, 14, 0);
        //em.rateOverDistance = 0;
        Debug.Log("Emit()");
        flag = 1;
        return;
    }
}
