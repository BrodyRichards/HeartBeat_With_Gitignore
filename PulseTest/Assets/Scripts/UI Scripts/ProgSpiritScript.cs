using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgSpiritScript : MonoBehaviour
{
    private ParticleSystem ps;
    private ParticleSystem.EmissionModule em;
    //private GameObject MC;

    private GameObject[] avatars;
    private GameObject Camera;
    private int flag;
    private float timer;

    private GameObject bunny;
    private GameObject ball;
    private GameObject music;
    private GameObject possessed;
    private Renderer rend;
    public Texture HappyTex, SadTex, MadTex;

    void Awake(){
        ps = gameObject.GetComponent<ParticleSystem>();
        ps.gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        //ps = gameObject.GetComponent<ParticleSystem>();
       // ps.Play();
        em = ps.emission;
        em.rateOverDistance = 0;
        
        //MC = GameObject.FindGameObjectWithTag("MC");
        
        avatars = GameObject.FindGameObjectsWithTag("Avatars");
        bunny = avatars[0];
        ball = avatars[1];
        music = avatars[2];
        possessed = bunny;

        rend = GetComponent<Renderer>();

        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        
        flag = 0;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            
            possessed = bunny;
            
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            possessed = ball;
            
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            possessed = music;
            
        }

        if(flag == 1){
            Debug.Log("Flag up");
            if (timer < 1.0f)
                {
                    timer += Time.deltaTime;
                    ps.transform.position = Camera.transform.position + new Vector3(-27, 14, 0);
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
            ps.transform.position = possessed.transform.position;
        }
    }
    public void Emit(int x){
        flag = 0;
        ps.transform.position = possessed.transform.position;
        em.rateOverDistance = 1;
        //ps.transform.position += new Vector3(25, 14, 0);
        //em.rateOverDistance = 0;
        Debug.Log("Emit()");
        if (x == 1) { rend.material.SetTexture("_MainTex", HappyTex); }
        if (x == 2) { rend.material.SetTexture("_MainTex", SadTex); }
        if (x == 3) { rend.material.SetTexture("_MainTex", MadTex); }
        flag = 1;
        return;
    }
}
