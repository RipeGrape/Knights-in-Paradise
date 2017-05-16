using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrounded : MonoBehaviour {
    
    //private PlayerMovement playerDir;
    public bool grounded = false;


    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "stage")
        {
            grounded = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "stage")
        {
            grounded = false;
        }
    }
}
