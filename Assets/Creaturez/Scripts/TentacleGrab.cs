using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleGrab : MonoBehaviour {
    
    public TentacleController _controller;
    bool eaten;

    void OnTriggerEnter(Collider c)
    {
        //@TODO: Probably should fix this semantics issue...
        if (c.tag == "Eatable")
        {
            if(eaten)
            {
                return;
            }

            //c.gameObject.transform.position = transform.position;
            _controller.BirdySnatch();

            if(c.GetComponent<JointScript>() != null)
            {
                c.GetComponent<JointScript>().Snatched(transform);
                eaten = true;
            }
        } else if (c.tag == "Player") {            
            _controller.targetRotationTransform.localEulerAngles = new Vector3(-8.48f, -78.14f, -1.65f);
            Manager.playerHP -= 1;
            Manager.flash = true;
        }
    } 
}
