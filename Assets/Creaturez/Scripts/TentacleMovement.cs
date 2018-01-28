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
    public Transform origin;
    public bool fed;
    public bool boost;
    public bool patrolling;
    private Vector3 lerpTo;
    private float boostTimer;
    private float defaultSpeed;
    private float _targetTime;
    List<Vector3> _posValues;

    void Awake()
    {
        this.bones = GameObject.FindGameObjectsWithTag("Bone");
        this.fed = false;
        this.boost = false;
        this.patrolling = true;
        this.defaultSpeed = this.speed;
    }

    private void Start()
    {
        _posValues = new List<Vector3>();

        for (int i = 0; i < 20; i++)
        {
            _posValues.Add(new Vector3(target.localPosition.x + Random.Range(-5, 5), target.localPosition.y + Random.Range(-5, 5), 0));
        }

    }

    //TODO: What if they are both in the collider?
    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "MainCamera" || c.tag == "Eatable")
        {
            this.target = c.gameObject.transform;
            this.patrolling = false;
        }
    }


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
        GetComponent<InverseKinematics>().Destination = this.target;
        this.timer += Time.deltaTime;

        if (this.patrolling)
        {
            if (this.timer >= _targetTime)
            {

                lerpTo = _posValues[Random.Range(0, _posValues.Count)];
                _targetTime = Random.Range(.1f, 1.2f);

                //lerpTo = new Vector3(Random.Range(transform.localPosition.x + 2, transform.localPosition.x + 20), Random.Range(transform.localPosition.y + 4, transform.localPosition.y + 10), target.localPosition.z);
                this.timer = 0f;
            }

            target.localPosition = Vector3.Lerp(target.localPosition, lerpTo, Time.deltaTime * 2f);
        }

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
