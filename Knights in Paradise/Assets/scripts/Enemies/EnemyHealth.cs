using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int enemyHealth = 1;
    public int currentHealth;
    float hitTimer;

    private Transform player;
    Rigidbody enemy;
    BoxCollider boxCollider;
    EnemyMove comMove;
    ChargerAttack chargAttk;
    BoomerangMastAttack boomMastAttk;

    // Use this for initialization
    void Awake () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        currentHealth = enemyHealth;
        comMove = transform.root.GetComponent<EnemyMove>();
        chargAttk = transform.root.GetComponent<ChargerAttack>();
        boomMastAttk = transform.root.GetComponent<BoomerangMastAttack>();
    }
	
	// Update is called once per frame
	void Update () {
        hitTimer += Time.deltaTime;
        enemy = GetComponent<Rigidbody>();
	}

    public void EnemyHit()
    {
        enemy.velocity = Vector3.zero;
        KnockBack();
        currentHealth -= 1;
        if (currentHealth <= 0)
        {
            Death();
        }
    }
    
    void KnockBack()
    {
        var horiDirection = player.position - transform.position;
        if (horiDirection.x > 0)
        {
            enemy.AddForce(-3f, 3f, 0f, ForceMode.VelocityChange);
        }
        else
        {
            enemy.AddForce(3f, 3f, 0f, ForceMode.VelocityChange);
        }
    }

    void Death()
    {        
        boxCollider.isTrigger = true;
        enemy.AddForce(0f, 0f, -40f);

        comMove.enabled = false;
        chargAttk.enabled = false;
        boomMastAttk.enabled = false;
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.gameObject.layer == LayerMask.NameToLayer("PlayerWeapon"))
        {
            EnemyHit();
        }
    }
}
