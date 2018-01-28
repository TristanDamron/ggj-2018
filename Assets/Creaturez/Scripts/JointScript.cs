using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointScript : MonoBehaviour {
    
    [SerializeField]
    private Rigidbody _anchor;
    private SpringJoint _joint;
    private Transform _anchorTransform;
    private Rigidbody _rb;

    private void Start()
    {
        _anchorTransform = _anchor.GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
        CreateAJoint();
    }

    public void Grabbed()
    {
        if(GetComponent<SpringJoint>())
        {
            return;
        }

        CreateAJoint();
    }

    public void Release(Vector3 targetPoint)
    {
        //_rb.constraints = RigidbodyConstraints.None;
        _anchorTransform.SetParent(null);
        _anchorTransform.position = targetPoint;
    }

    public void DestroyJoint()
    {
        Destroy(GetComponent<SpringJoint>());
    }

    public Transform ReturnAnchorTransform()
    {
        return _anchorTransform;
    }

    void CreateAJoint()
    {
        
        gameObject.AddComponent<SpringJoint>();
        _joint = GetComponent<SpringJoint>();
        _joint.connectedBody = _anchor;
        _joint.spring = 75f;
        _joint.damper = 0.3f;
        _joint.tolerance = 0f;
        _joint.enablePreprocessing = true;
        _rb.constraints = RigidbodyConstraints.FreezeRotation;

    }

    public void AttachNewJoint(Rigidbody newAnchor)
    {
        Destroy(_joint);
        _anchor = newAnchor;
        CreateAJoint();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Tentacles")
        {
            //Do Some random shit
        }
    }
}
