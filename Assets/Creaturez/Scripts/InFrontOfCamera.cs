﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InFrontOfCamera : MonoBehaviour {

    Transform _targetTransform;

    float _distance = .5f;

    public void SetAnchorTransformTarget(Transform target)
    {
        target.position = transform.position + transform.forward * _distance;
        target.SetParent(transform);
    }

    public Vector3 ReturnVectorPoint()
    {
        return transform.position + transform.forward * 5f; 
    }
}
