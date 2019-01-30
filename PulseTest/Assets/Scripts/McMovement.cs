﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McMovement : MonoBehaviour
{
    public Animator anim;

    private Vector2 direction;
    private float speed = 5f;
    private double worldX = 21.9;
    void Start()
    {
        direction = Vector2.right;
        anim.SetBool("isWalking", true);
    }

    void Update()
    {
        transform.Translate(Time.deltaTime * speed * direction);
        if ( transform.position.x > worldX  || transform.position.x + Mathf.Epsilon < - worldX)
        {
            direction *= -1;
            transform.localScale = new Vector3(- transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        
        
    }

    
}
