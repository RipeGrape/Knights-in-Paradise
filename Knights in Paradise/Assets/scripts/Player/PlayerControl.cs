using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public float speed = 4f;
    public bool facingRight = true;

    Animator anim;
    Rigidbody player;
    Vector3 movement;
    Vector3 playerTurining;
    RaycastHit hit;

    public Transform groundCheck;
    public bool grounded = false;
    public float jumpPower = 250f;

    void Awake()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Rigidbody>();
        
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        Move(h);
        PlayerChangeSides(h);
        AnimateRun(h);

        if (Input.GetKeyDown(KeyCode.X) && grounded)
        {
           player.AddRelativeForce(new Vector3(0f, jumpPower, 0f));
        }
    }

    void Move(float h)
    {
        movement.Set(h, 0f, 0f);
        movement = movement.normalized * speed * Time.deltaTime;
        player.MovePosition(transform.position + movement);
    }

    void PlayerChangeSides(float h)
    {
        if (h < 0f)
        {
            playerTurining = transform.localScale;
            playerTurining.x = -1;
            transform.localScale = playerTurining;
            facingRight = false;
        }
        else if(h > 0f)
        {
            playerTurining = transform.localScale;
            playerTurining.x = 1;
            transform.localScale = playerTurining;
            facingRight = true;
        }
    }

    void AnimateRun(float h)
    {
        bool running = h != 0f;
        anim.SetBool("IsRunning", running);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "ground")
        {
            grounded = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "ground")
        {
            grounded = false;
        }
    }
}
