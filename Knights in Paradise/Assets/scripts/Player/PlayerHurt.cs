using System.Collections;
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
    ThrowBoomerang playerAttk;
    EnemyMove enemyDir;
    Rigidbody player;
    Animator anim;
    BoxCollider boxCollider;
    float hitTimer;
    


    void Awake()
    {  
        player = GetComponent<Rigidbody>();
        enemyDir = GetComponent<EnemyMove>();
        playerCtrl = GetComponent<PlayerControl>();
        playerAttk = GetComponent<ThrowBoomerang>();
        anim = GetComponent<Animator>();
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

        updateHealth();
        if (currentHealth <= 0)
        {
            NotificationText.text = "Ouch, Game Over";
            Death();
        }    
    }

    void KnockBack(float dir)
    {
        player.AddForce(3f * dir, 3f, 0f, ForceMode.VelocityChange);
    }

    void updateHealth()
    {
        int internalHeath;
        internalHeath = currentHealth;
        healthBar.value = internalHeath;
    }

    void Death()
    {
        player.velocity = Vector3.zero;
        player.AddForce(0f,0f, -40f);
        boxCollider.isTrigger = true;

        playerCtrl.enabled = false;
        playerAttk.enabled = false;       
    }
}
