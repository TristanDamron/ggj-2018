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

    void Awake()
    {
        this.bones = GameObject.FindGameObjectsWithTag("Bone");
        //this.target = GameObject.FindGameObjectWithTag("Eatable").transform;
        this.fed = false;
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
            SetControlBone(this.target.position);
        } else
        {
            SetLastBone(GetControlBone());
            SetControlBone(this.origin.position);
        }
    }
}
