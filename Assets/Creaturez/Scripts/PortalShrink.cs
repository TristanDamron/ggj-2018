using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalShrink : MonoBehaviour {
    private bool shrink;

    void Awake()
    {
        this.shrink = false;
    }

	void OnTriggerEnter (Collider c)
    {
        if (c.gameObject.tag == "Eatable")
        {
            this.shrink = true;
            Destroy(c.gameObject);
        }
    }

	void Update () {
        
	    if (this.shrink)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * 4f);
            if (transform.localScale.x <= 0.1f)
            {
                Destroy(gameObject);
            }
        }	
	}
}
