using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 4f;

    Animator anim;
    Rigidbody player;
    Vector3 movement;
    Vector3 playerTurining;

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
        }
        else if(h > 0f)
        {
            playerTurining = transform.localScale;
            playerTurining.x = 1;
            transform.localScale = playerTurining;
        }
    }

    void AnimateRun(float h)
    {
        bool running = h != 0f;
        anim.SetBool("IsRunning", running);
    }
}
