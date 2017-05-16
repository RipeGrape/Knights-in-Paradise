using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBoomerang : MonoBehaviour {

    private Animator anim;
    private PlayerControl playerDir;
    public GameObject boomerang;

	void Awake() {
        anim = transform.root.gameObject.GetComponent<Animator>();
        playerDir = transform.root.GetComponent<PlayerControl>();	
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetTrigger("AnimSpin");

        if (Input.GetKeyDown(KeyCode.Z))
        {
           if (playerDir.facingRight)
            {
                Instantiate(boomerang, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            }
            else
            {
                Instantiate(boomerang, transform.position, Quaternion.Euler(new Vector3(0, 180, 0)));
            }         
        }
	}
}
