using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InFrontOfCamera : MonoBehaviour {

    Transform _targetTransform;

    float _distance = .5f;

    public void SetAnchorTransformTarget(Transform target)
    {
        var offset = new Vector3(transform.position.x, transform.position.y - .15f, transform.position.z);
        target.position = offset + transform.forward * _distance;
        target.SetParent(transform);
    }

    public Vector3 ReturnVectorPointInFront(float distance)
    {
        return transform.position + transform.forward * distance; 
    }

    public Vector3 ReturnVectorPoint()
    {
        return transform.position + transform.forward * 5f; 
    }
}
