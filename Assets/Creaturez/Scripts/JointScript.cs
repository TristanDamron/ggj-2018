using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointScript : MonoBehaviour {
    
    [SerializeField]
    private Rigidbody _anchor;
    private SpringJoint _joint;


    public void Grabbed()
    {
        CreateAJoint();
    }

    void DestroyJoint()
    {
        Destroy(GetComponent<SpringJoint>());
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
    }

    public Transform ReturnAnchorTransform()
    {
        return _anchor.GetComponent<Transform>();
    }

    public void AttachNewJoint(Rigidbody newAnchor)
    {
        Destroy(_joint);
        _anchor = newAnchor;
        CreateAJoint();
    }
}
