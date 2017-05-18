using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBoomerang : MonoBehaviour {
    private Transform enemy;
    private Animator anim;

    float throwTimer;
    float speed = 4f;
    float vertSpeed = 0.8f;
    bool returning = false;
    public bool boomerangInHand = true;
    // Use this for initialization

    void OnEnable()
    {
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
}
