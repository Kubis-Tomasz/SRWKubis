using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float accel = 400.0F;
    public float maxSpeed = 2.0F;
    public float rotateSpeed = 2.0F;
    public Animator anim;

    private Vector3 moveDirection = Vector3.zero;
    public float jumpHeight = 100f;
    private bool isGrounded;
    private int state = 0;
    private float speed = 0;

    /*
    STANY
        -2 - backwards run
        -1 - backwards walk
        0  - idle
        1  - walk
        2  - run
        10 - jump up
    */
   
    void FixedUpdate()
    {
        Vector3 vel = rb.velocity;

        moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= accel;

        rb.AddForce(moveDirection * Time.deltaTime);

        //rotate
        transform.Rotate(0, Input.GetAxis("Horizontal")*rotateSpeed, 0);

        if (vel.magnitude > maxSpeed)
        {
            rb.velocity = vel.normalized * maxSpeed;
        }

        //chodzenie/bieganie
        if (vel.magnitude > 0.0f)
        {
            // do przodu
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    state = 2;
                    speed = maxSpeed;
                }
                else
                {
                    state = 1;
                    speed = maxSpeed/2;
                }
            }
            //do tyłu
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    state = -2;
                    speed = maxSpeed;
                }
                else
                {
                    state = -1;
                    speed = maxSpeed/2;
                }
            }
        }
        else
        {
            state = 0;
        }
        anim.SetInteger("state", state);

        // skok
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * jumpHeight);
                anim.SetBool("jump", true);
            }
            else
            {
                anim.SetBool("jump", false);
            }
        }



    }//fixed update

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
            isGrounded = true;
    }
    
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
            isGrounded = false;
    }
}//class
