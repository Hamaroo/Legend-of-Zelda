using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Moving : MonoBehaviour {
    public float speed = 3.0F;
    public float tileSize = 1f;

    private Vector3 destination;
    private bool isMoving = false;

    private enum direction {SIDE,FORWARD,BACKWARD };
    private direction facing = direction.FORWARD;


    CharacterController controller;
    Animator anim;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        
    }

    void Update()
    {
        float xmove = Input.GetAxisRaw("Horizontal");
        float ymove = Input.GetAxisRaw("Vertical");

        //facing the correct way
        if(xmove != 0)
        {
            transform.localScale = new Vector2(xmove*-4, 4);
        }

        //moving
          
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            isMoving = true;
            destination = new Vector2(transform.position.x + tileSize * xmove, transform.position.y);
        }
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            isMoving = true;
            destination = new Vector2(transform.position.x, transform.position.y + tileSize * ymove);
        }

        if (isMoving)
        {
            if (transform.position != destination)
                transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * speed);
            else
            {
                if(xmove ==0 && ymove == 0)
                    isMoving = false;
            }
        }

        //facing direction
        if(xmove != 0) { facing = direction.SIDE; }
        else if (ymove > 0) { facing = direction.BACKWARD; }
        else if (ymove < 0) { facing = direction.FORWARD; }

        if(facing == direction.SIDE )
        {
            if(isMoving)
                anim.SetBool("walkingSide", true);
            else
                anim.SetBool("walkingSide", false);
        }
        if (facing == direction.FORWARD)
        {
            if (isMoving)
                anim.SetBool("walkingForward", true);
            else
                anim.SetBool("walkingForward", false);
        }
        if (facing == direction.BACKWARD)
        {
            if (isMoving)
                anim.SetBool("walkingBackward", true);
            else
                anim.SetBool("walkingBackward", false);
        }


    }
}
