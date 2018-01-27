using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleMovement : MonoBehaviour {
    public GameObject[] bones;
    public float timer;
    public float speed;
    public Transform target;

    void Awake()
    {
        this.bones = GameObject.FindGameObjectsWithTag("Bone");
    }

    public Vector3 GetControlBone()
    {
        return this.bones[0].transform.position;
    }

    public void SetControlBone(Vector3 pos)
    {
        this.bones[0].transform.position = Vector3.Lerp(GetControlBone(), pos, Time.deltaTime * this.speed);
    }

    public void SetLastBone(Vector3 pos)
    {
        this.bones[this.bones.Length - 2].transform.position = Vector3.Lerp(GetLastBone(), pos, Time.deltaTime * this.speed);
    }

    public Vector3 GetLastBone()
    {
        return this.bones[this.bones.Length - 2].transform.position;
    }

    void Update()
    {
        this.timer += Time.deltaTime;

        SetLastBone(GetControlBone());
        SetControlBone(new Vector3(this.target.position.x, this.target.position.y, this.target.position.z));

        if (this.timer >= 0.25f)
        {
            
            this.timer = 0f;
        }
    }
}
