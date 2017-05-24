﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBoomerang : MonoBehaviour {
    private Transform enemy;
    GameObject player;
    PlayerHurt playerHit;
    private Animator anim;

    float throwTimer;
    float speed = 4f;
    float vertSpeed = 0.8f;
    bool returning = false;
    public bool boomerangInHand = true;

    float hitDir = -1f;
    // Use this for initialization

    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHit = player.GetComponent<PlayerHurt>();
        enemy = GameObject.FindGameObjectWithTag("BoomerangMaster").transform;
        throwTimer = 0.0f;

    }

    // Update is called once per frame
    void Update()
    {
        throwTimer += Time.deltaTime;
        var vertDirection = enemy.position - transform.position;
        vertDirection.x = 0;
        vertDirection.y *= 2;
        var horiDirection = enemy.position - transform.position;

        if (throwTimer >= 2.5f)
        {
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyWeapon"), LayerMask.NameToLayer("enemy"), false);
            returning = true;
        }

        if (!returning)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            if (horiDirection.x > 0)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
                hitDir *= 1f;
            }
            else
            {
                transform.Translate(Vector3.right * -speed * Time.deltaTime, Space.World);
            }
            transform.Translate(vertDirection * vertSpeed * Time.deltaTime, Space.World);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision detected" + gameObject.name);
        if ((other.transform.tag == "BoomerangMaster" || other.transform.name == "BoomerangMaster") && returning)
        {
            Destroy(gameObject);
            boomerangInHand = true;
        }
       
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            playerHit.PlayerHit(-hitDir);
        }
    }
}
