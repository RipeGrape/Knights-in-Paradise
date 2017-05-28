using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerAttack : MonoBehaviour {
    GameObject player;
    PlayerHurt playerHit;
    EnemyMove comMove;

    private float dir = -1f;
    Vector3 movement;
    Rigidbody charger;
    float speed = 5f;
	// Use this for initialization
	void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHit = player.GetComponent<PlayerHurt>();
        comMove = transform.root.GetComponent<EnemyMove>();
        charger = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (comMove.seePlayer)
        {
            dir = comMove.dir;

            comMove.enabled = false;
            Move(dir);
        }
        		
	}

    public void Move(float dir)
    {
        movement.Set(dir, 0f, 0f);
        movement = movement.normalized * speed * Time.deltaTime;
        charger.MovePosition(transform.position + movement);
    }
}
