using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InFrontOfCamera : MonoBehaviour {

    Transform _targetTransform;

    float _distance = .5f;

	// Use this for initialization
	void Start () 
    {
    }

    public void SetAnchorTransformTarget(Transform target)
    {
        target.position = transform.forward * _distance;
        target.SetParent(transform);
    }
}
