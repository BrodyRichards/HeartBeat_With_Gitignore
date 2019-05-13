using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSounds : MonoBehaviour
{
    private AudioSource ass;
    public AudioClip[] walkCarpet;
    private AudioClip step;
    // Start is called before the first frame update
    void Start()
    {
        ass = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Walking"))
        {
            Debug.Log("MC is walking");
            int ran = Random.Range(0, walkCarpet.Length);
            step = walkCarpet[ran];
            ass.clip = step;
            ass.Play();
        }
    }
}
