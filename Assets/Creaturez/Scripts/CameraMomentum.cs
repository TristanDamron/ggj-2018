using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//@TODO: Do we really need this functionality??
public class CameraMomentum : MonoBehaviour {
    public Vector3 last;
    private float _timer;

    public float GetMomentumX()
    {
        return transform.position.x - last.x;
    }
    
    public float GetMomentumY()
    {
        return transform.position.y - last.y;
    }

    void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Eatable")
        {
            //@Test: Is this too low of a delta?
            if (GetMomentumX() > 3f || GetMomentumY() > 3f)
            {
                c.GetComponent<JointScript>().Snatched(c.transform);
            }
        }
    }

    void Update()
    {
        this._timer += Time.deltaTime;
        if (this._timer >= 1f)
        {
            this.last = transform.position;
            this._timer = 0f;
        }
    }
}
