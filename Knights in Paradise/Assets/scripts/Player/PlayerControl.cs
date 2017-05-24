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

    public bool grounded = false;
    public float jumpPower = 250f;
    public bool isHurtable = true;
    float fallTimer;

    void Awake()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Rigidbody>();

        fallTimer = 0f;
    }

    void Update()
    {
        fallTimer += Time.deltaTime;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (isHurtable)
        {
            Move(h);
            PlayerChangeSides(h);
        }
        AnimateRun(h);


        if (Input.GetButton("Jump") && grounded)
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
        
        if (isHurtable)
        {
            bool running = h != 0f;
            anim.SetBool("IsRunning", running);
        }
       else
        {
            anim.SetBool("IsRunning", false);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "ground")
        {
            grounded = true;
            isHurtable = true;
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("enemy"), LayerMask.NameToLayer("player"),false);
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
