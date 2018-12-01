using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Moving : MonoBehaviour {
    public float speed = 3.0F;
    public float tileSize = 1f;

    private Vector3 destination;
    private bool isMoving = false;

    private enum direction {RIGHT,LEFT,FORWARD,BACKWARD };
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
        if(xmove > 0) { facing = direction.RIGHT; }
        else if (xmove < 0) { facing = direction.LEFT; }
        else if (ymove > 0) { facing = direction.BACKWARD; }
        else if (ymove < 0) { facing = direction.FORWARD; }

        if(facing == direction.RIGHT)
        {
            if(isMoving)
                anim.SetBool("walkingRight", true);
            else
                anim.SetBool("walkingRight", false);
        }
        


    }
}
