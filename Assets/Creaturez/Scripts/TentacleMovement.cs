using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlanZucconi.IK;

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
        this.target = transform;
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

    void Update()
    {
        GetComponent<InverseKinematics>().Destination = this.target;

        if (this.fed)
        {
            this.target = this.origin.transform;
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
