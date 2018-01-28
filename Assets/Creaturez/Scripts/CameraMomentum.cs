using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMomentum : MonoBehaviour {
    public Vector3 last;
    private float timer;

    public float GetMomentumX()
    {
        return transform.localPosition.x - last.x;
    }
    
    public float GetMomentumY()
    {
        return transform.localPosition.y - last.y;
    }

    void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Eatable")
        {
            Debug.Log("Player enter");
            if (GetMomentumX() > 1f || GetMomentumY() > 1f)
            {
                Destroy(c.gameObject);
            }
        }
    }

    void Update()
    {
        this.timer += Time.deltaTime;
        if (this.timer >= 1f)
        {
            this.last = transform.localPosition;
            this.timer = 0f;
        }
    }
}
