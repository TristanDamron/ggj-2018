using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleGrab : MonoBehaviour {
    
    public TentacleController _controller;
    bool eaten;

    //TODO: What does the tentacle do if it touches the player?
    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Eatable")
        {
            if(eaten)
            {
                return;
            }

            c.gameObject.transform.position = transform.position;
            _controller.BirdySnatch();

            if(c.GetComponent<JointScript>() != null)
            {
                c.GetComponent<JointScript>().Snatched(transform);
                eaten = true;
            }
        }
    }
}
