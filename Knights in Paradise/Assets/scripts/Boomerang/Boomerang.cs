using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour {
    private Transform player;
    private Animator anim;
    GameObject charger;
    GameObject boomMast;
    EnemyHealth chargerHit;
    EnemyHealth boomMastHit;

    float throwTimer;
    float speed = 4f;
    float vertSpeed = 0.8f;
    public bool returning = false;
    // Use this for initialization
    void Start()
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerWeapon"), LayerMask.NameToLayer("EnemyWeapon"));
    }
    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        throwTimer = 0.0f;
        charger = GameObject.FindGameObjectWithTag("Charger");
        boomMast = GameObject.FindGameObjectWithTag("BoomerangMaster");
        chargerHit = charger.GetComponent<EnemyHealth>();
        boomMastHit = boomMast.GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        throwTimer += Time.deltaTime;
        var vertDirection = player.position - transform.position;
        vertDirection.x = 0;
        vertDirection.y *= 2;
        var horiDirection = player.position - transform.position;

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
        if ((other.transform.tag == "Player" || other.transform.name == "Player") && returning)
        {
            Destroy(gameObject);
        }
    }
}

