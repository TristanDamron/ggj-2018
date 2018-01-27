using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabable : MonoBehaviour {
    void OnTriggerEnter (Collider c)
    {
        if (c.tag == "Bone")
        {
            GetComponent<JointScript>().AttachNewJoint(c.gameObject.GetComponent<Rigidbody>());
        }
    }
}
