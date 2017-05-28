﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHurt : MonoBehaviour {
    public int playerHealth = 3;
    public int currentHealth;
    public bool isHurt = false;
    public Slider healthBar;
    public Text NotificationText;
    Vector3 knockback;

    PlayerControl playerCtrl;
    EnemyMove enemyDir;
    Rigidbody player;
    Animator anim;
    GameObject enemyWeapon;
    BoxCollider boxCollider;
    float hitTimer;
    


    void Awake()
    {  
        player = GetComponent<Rigidbody>();
        enemyDir = GetComponent<EnemyMove>();
        playerCtrl = GetComponent<PlayerControl>();
        anim = GetComponent<Animator>();
        enemyWeapon = GameObject.FindGameObjectWithTag("EnemyWeapon");
        boxCollider = GetComponent<BoxCollider>();
    
        hitTimer = 0f;
        currentHealth = playerHealth;
    }
	
	// Update is called once per frame
	void Update () {

        //enemyWeapons = GameObject.FindGameObjectsWithTag("EnemyWeapon");

        hitTimer += Time.deltaTime;
        if (!playerCtrl.isHurtable)
        {
            isHurt = true;
            hitTimer = 0f;
        }
        else
        {
            isHurt = false;
            
        }
	}

    //This is a public function for the enemy collision dectection on player to apply damage
    public void PlayerHit(float direction)
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("enemy"), LayerMask.NameToLayer("player"));
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyWeapon"), LayerMask.NameToLayer("player"));
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("player"), LayerMask.NameToLayer("charger"));

        anim.SetTrigger("IsHurt");
        player.velocity = Vector3.zero;
        KnockBack(direction);
        playerCtrl.isHurtable = false;
        currentHealth -= 1;
        if(currentHealth <= 0)
        {
            NotificationText.text = "Ouch, Game Over";
            Death();
        }
        updateHealth();
    }

    void KnockBack(float dir)
    {
        player.AddForce(0f, 110f, 0f);
        player.AddForce(110f * dir, 0f, 0f);

    }

    void updateHealth()
    {
        int internalHeath;
        internalHeath = currentHealth;
        healthBar.value = internalHeath;
    }

    void Death()
    {
        boxCollider.isTrigger = true;
        player.AddRelativeForce(110f, 110f, 40f);
    }
}
