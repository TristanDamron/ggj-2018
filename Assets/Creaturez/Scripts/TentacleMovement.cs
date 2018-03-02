using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlanZucconi.IK;

public class TentacleMovement : MonoBehaviour
{


    public GameObject[] bones;
    public float timer;
    public float speed;
    public Transform target;
    public bool fed;
    public bool boost;
    public bool patrolling;
    public bool spawning;
    private Vector3 lerpTo;
    private Quaternion rot;
    public float boostTimer;
    public Transform[] joints;
    private float defaultSpeed;
    public float _targetTime;
    List<Vector3> _posValues;

    void Awake()
    {
        //this.target = transform;
        this.fed = false;
        this.boost = false;
        this.patrolling = false;
        this.spawning = true;
        //this.joints = GetComponent<InverseKinematics>().Joints;
        _targetTime = Random.Range(.1f, 1.2f);
    }

    private void Start()
    {
        //GetComponent<InverseKinematics>().Destination = this.target;

        _posValues = new List<Vector3>();

        for (int i = 0; i < 20; i++)
        {
            _posValues.Add(new Vector3(target.localPosition.x, target.localPosition.y + Random.Range(-5, 30), target.localPosition.z));
        }

    }

    // void OnTriggerEnter(Collider c)
    // {    
    //     //@TODO: Tentacle does not go to the target
    //     if (c.tag == "Eatable" || c.tag == "Player")
    //     {
    //         this.target = c.gameObject.transform;
    //         this.patrolling = false;
    //     }
    // }


    //void OnTriggerExit (Collider c)
    //{
    //    if (c.tag == "MainCamera" || c.tag == "Eatable")
    //    {
    //        this.target = GameObject.Find("Default Transform").transform;
    //        this.patrolling = true;
    //    }
    //}

    void Update()
    {
        this.timer += Time.deltaTime;

        if (this.spawning)
        {

            //Quaternion rot = Quaternion.Euler(this.joints[0].transform.localPosition.x + Random.Range(-90f, 90f), 0f, this.joints[0].transform.localPosition.z);
            Quaternion rot = Quaternion.Euler(target.localEulerAngles.x, target.localEulerAngles.y, target.localEulerAngles.z);
            for (int i = 0; i < this.joints.Length; i++)
            {
                Quaternion slerp = Quaternion.Slerp(this.joints[i].transform.localRotation, rot, Time.deltaTime / this.speed);
                this.joints[i].transform.localRotation = slerp;
            }

        }
    }
}
