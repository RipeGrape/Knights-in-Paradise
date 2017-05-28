using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int enemyHealth = 1;
    public int currentHealth;
    float hitTimer;

    Rigidbody enemy;
    BoxCollider boxCollider;

    // Use this for initialization
    void Awake () {
        enemy = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        currentHealth = enemyHealth;
    }
	
	// Update is called once per frame
	void Update () {
        hitTimer += Time.deltaTime;
        enemy = GetComponent<Rigidbody>();
	}

    public void EnemyHit()
    {
        KnockBack();
        currentHealth -= 1;
        if (currentHealth <= 0)
        {
            Death();
        }
    }
    
    void KnockBack()
    {
        enemy.AddForce(110f, 110f, 0f);
    }

    void Death()
    {
        boxCollider.isTrigger = true;
        enemy.AddForce(110f, 110f, 0f);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.gameObject.layer == LayerMask.NameToLayer("PlayerWeapon"))
        {
            EnemyHit();
        }
    }
}
