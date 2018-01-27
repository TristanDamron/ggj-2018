using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InFrontOfCamera : MonoBehaviour {


    [SerializeField]
    GameObject _cube;

    public JointScript _script;
    Transform _targetTransform;

    float _distance = .5f;

	// Use this for initialization
	void Start () 
    {
        Instantiate(_cube, transform.position + transform.forward * _distance, transform.rotation);
        _script.Grabbed();
        _targetTransform = _script.ReturnAnchorTransform();
    }
	
	// Update is called once per frame
	void Update () {
        _targetTransform.transform.position = transform.position + transform.forward * _distance;
        _targetTransform.LookAt(transform);
	}
}
