using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McMovement : Movement
{
    public Animator anim;

    
    void Update()
    {
        anim.SetBool("isWalking", walking);
        getInput();
        Move();
    }
}
