using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleMovement : MonoBehaviour {
    
    public GameObject[] bones;
    public float timer;
    public float speed;
    public Transform target;
    public Transform origin;
    public bool fed;
    public bool boost;
    private float boostTimer;
    private float defaultSpeed;

    void Awake()
    {
        this.bones = GameObject.FindGameObjectsWithTag("Bone");
        this.fed = false;
        this.boost = false;
        this.defaultSpeed = this.speed;
        //this.target = transform;
    }

    //TODO: What if they are both in the collider?
    void OnTriggerStay (Collider c)
    {
        if (c.tag == "MainCamera" || c.tag == "Eatable")
        {
            this.target = c.gameObject.transform;
        }
    }

    void OnTriggerExit (Collider c)
    {
        if (c.tag == "MainCamera" || c.tag == "Eatable")
        {
            this.target = transform;  
        }
    }

    private Vector3 GetControlBone()
    {
        return this.bones[0].transform.position;
    }

    private void SetControlBone(Vector3 pos)
    {
        this.bones[0].transform.position = Vector3.Lerp(GetControlBone(), pos, Time.deltaTime * this.speed);
    }

    private void SetLastBone(Vector3 pos)
    {
        this.bones[this.bones.Length - 2].transform.position = Vector3.Lerp(GetLastBone(), pos, Time.deltaTime * this.speed);
    }

    private Vector3 GetLastBone()
    {
        return this.bones[this.bones.Length - 2].transform.position;
    }

    void Update()
    {
        if (!this.fed)
        {
            SetLastBone(GetControlBone());
            SetControlBone(this.target.position + (this.target.forward * 2));
        } else
        {
            SetLastBone(GetControlBone());
            SetControlBone(this.origin.position + (this.target.forward * 2));
        }

        if (this.boost)
        {
            this.speed = this.speed + 0.5f;
            this.boostTimer += Time.deltaTime;
        }

        if (this.boostTimer >= 0.5f)
        {
            this.boost = false;
            this.boostTimer = 0f;
            this.speed = this.defaultSpeed;
            this.fed = true;
        }
    }
}
