using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleGrab : MonoBehaviour {
    public TentacleMovement movementScript;

    //TODO: What does the tentacle do if it touches the player?
    void OnTriggerStay(Collider c)
    {
        if (c.tag == "Eatable")
        {
            this.movementScript.target = c.gameObject.transform;
            this.movementScript.boost = true;
            c.gameObject.transform.position = transform.position;
            c.GetComponent<JointScript>().DestroyJoint();
        }
    }
}
