using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgBurstScript : MonoBehaviour
{

    private ParticleSystem ps;
    private GameObject Camera;
    private int flag;

    // Start is called before the first frame update
    void Start()
    {
        ps = gameObject.GetComponent<ParticleSystem>();
        Camera = GameObject.FindGameObjectWithTag("MainCamera");

        flag = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ps.transform.position = Camera.transform.position + new Vector3(-27,10,0);
        if (flag == 1){ Debug.Log("Played");ps.Play(); flag = 0; }//ps.emission.SetBurst(0,new ParticleSystem.Burst(0, 30,1,1)); }
    }

    public void Boom()
    {
        flag = 1;
        Debug.Log(flag);
        return;
    }
}
