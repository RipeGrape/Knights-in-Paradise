using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangMastAttack : MonoBehaviour {

    private Animator anim;
    private EnemyMove comMove;
    public GameObject boomerang;
    float newThrow = 4f;

    void Awake()
    {
        anim = transform.root.gameObject.GetComponent<Animator>();
        comMove = transform.root.GetComponent<EnemyMove>();
    }

    // Update is called once per frame
    void Update()
    {
        newThrow += Time.deltaTime;
        anim.SetTrigger("AnimSpin");
        if (comMove.seePlayer && (newThrow > 4f))
        {
            newThrow = 0f;
            ThrowBoomerang();
        }
    }

    void ThrowBoomerang()
    {
        if (!comMove.facingLeft)
        {
            Instantiate(boomerang, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        }
        else
        {
            Instantiate(boomerang, transform.position, Quaternion.Euler(new Vector3(0, 180, 0)));
        }
    }
}
