using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgSpirit : MonoBehaviour
{
    private ParticleSystem ps;
    private ParticleSystem.EmissionModule em;
    private GameObject ProgressBar;
    private GameObject MC;
    private GameObject Camera;

    private float time;
    private int flag;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        em = ps.emission;
        em.enabled = true;
        ProgressBar = GameObject.FindGameObjectWithTag("ProgressBar");
        MC = GameObject.FindGameObjectWithTag("MC");
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        flag = 1;
        ps.Play();
        ps.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        shift();

        if(flag != 1)
        {
            ps.Play();
            transform.position = MC.transform.position;
            flag = 1;

        }
        

    }
    public void MSpiritC(){
        flag = 0;

        //transform.ProgressBar.transform.position;
    }
    private void shift(){
        transform.position = Camera.transform.position + new Vector3(33,16,0);
    }
}
