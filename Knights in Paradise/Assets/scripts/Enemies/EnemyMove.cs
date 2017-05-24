using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {
    
    private Rigidbody enemy;
    private Animator anim;
    Transform edgeTrigger;

    public bool facingLeft = true;
    public float speed = 3f;
    bool edge;
    Vector3 movement;
    float dir = -1f;
    bool turnAround = false;
    float turnTimer = 3f;
    float forceTurnTimer = 2f;

    RaycastHit playerSeen;
    Vector3 direction;
    float dirFloat = 1f;
    public Transform eyes;
    float sight = 2f;
    public bool seePlayer = false;

    GameObject player;
    PlayerHurt playerHit;

    void Awake()
    {
        enemy = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        edgeTrigger = GameObject.FindWithTag("boundry").transform;
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("enemy"), LayerMask.NameToLayer("enemy"));

        player = GameObject.FindGameObjectWithTag("Player");
        playerHit = player.GetComponent<PlayerHurt>();

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyWeapon"), LayerMask.NameToLayer("enemy"));
    }
	
	// Update is called once per frame
    /*
     Th update works by finding the bool facingLeft and change the float to set in the vector3 called "direction", this allows the 
     enemy sight to change direction and look ahead to find the transform of the Player.
     */
	void Update () {
        // This part is to send out a raycast and return as AI sees the player as true or false
        if (facingLeft)
        {
            dirFloat *= -dirFloat;
        }
        else
        {
            dirFloat *= dirFloat;
        }
        direction.Set(dirFloat,0f,0f); 
        if (Physics.Raycast(eyes.position, direction, out playerSeen, sight, LayerMask.GetMask("player"))){
            PlayerControl player = playerSeen.collider.GetComponent<PlayerControl>();
            if (playerSeen.transform.tag == "Player")
            {
                Debug.Log("see player");
                Debug.DrawRay(eyes.position, direction * sight, Color.green);
                seePlayer = true;
            }
        }
        else
        {
            Debug.Log("where is he");
            Debug.DrawRay(eyes.position, direction * sight, Color.red);
            seePlayer = false;
        }

        //This part checks if the AI should force to turn around
        turnTimer += Time.deltaTime;
        forceTurnTimer = turnTimer;
        if (turnAround && (forceTurnTimer > 0.5f))
        {
            Debug.Log("True");
            Flip();
        }
        Move(dir);
    }

    //Move will move the enemies
    void Move(float dir)
    {
        movement.Set(dir, 0f, 0f);
        movement = movement.normalized * speed * Time.deltaTime;
        enemy.MovePosition(transform.position + movement);
    }

    //Flip will change the enemies direction
    void Flip()
    {
        turnTimer = 0f;
        turnAround = !turnAround;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        dir *= -1f;
        if (facingLeft)
        {
            facingLeft = false;
        }
        else
        {
            facingLeft = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("boundry"))
        {
            Debug.Log("It worked");
            if (turnAround == false)
            {
                turnAround = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("boundry"))
        {
            Debug.Log("It worked");
            turnAround = false;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            Debug.Log("I was hit");
            if(facingLeft)
            {
                playerHit.PlayerHit(-dir);
            }
            else if(!facingLeft)
            {
                playerHit.PlayerHit(-dir);
            }
            
        }
    }
}
