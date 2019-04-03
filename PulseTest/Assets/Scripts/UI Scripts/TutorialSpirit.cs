using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpirit : MonoBehaviour
{
    private ParticleSystem ps;
    private ParticleSystem.EmissionModule em;

    private GameObject[] avatars;

    private GameObject MC;
    private GameObject possessed;
    private GameObject bed;

    public static bool goToMC = false;
    // Start is called before the first frame update
    void Start()
    {
        ps = gameObject.GetComponent<ParticleSystem>();
        em = ps.emission;

        avatars = GameObject.FindGameObjectsWithTag("Avatars");
        
        bed = GameObject.Find("bed");
        transform.position = bed.transform.position;
        possessed = bed;
        goToMC = false;
    }

    // Update is called once per frame
    void Update()
    {
        var alarm = avatars[0];
        var curtain = avatars[1];

        transform.position = possessed.transform.position;
        if (!goToMC)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {

                possessed = alarm;

            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                possessed = curtain;

            }
        }
        else
        {
            MC = GameObject.FindGameObjectWithTag("MC");
            possessed = MC;
        }
        
        

        
        

        


    }

    private void Emite(){
        em.enabled = true;
    }
    private void StopEmite(){
        em.enabled = false;
    }
}
