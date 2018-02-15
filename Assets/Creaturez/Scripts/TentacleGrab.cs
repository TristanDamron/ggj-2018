using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleGrab : MonoBehaviour {
    
    public TentacleController _controller;
    bool eaten;

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
        } else if (c.tag == "Player") {            
            Manager.playerHP -= 1;
            Manager.flash = true;
        }
    } 
}
