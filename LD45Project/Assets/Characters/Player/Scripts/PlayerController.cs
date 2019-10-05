using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Fighter
{

    // Start is called before the first frame update
    void Start()
    {
        CharacterSetup();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        //get input
        float h = Input.GetAxis("Horizontal");
        //move
        rb.velocity = new Vector2(speed * h * Time.fixedDeltaTime, rb.velocity.y);
    }
}
