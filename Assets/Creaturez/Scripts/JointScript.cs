using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointScript : MonoBehaviour {
    
    [SerializeField]
    private Rigidbody _anchor;
    private SpringJoint _joint;
    private Transform _anchorTransform;
    private Rigidbody _rb;
    private float increment;
    private IEnumerator _routine;

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
        _anchorTransform.SetParent(null);

        if(_routine != null)
        {
            StopCoroutine(_routine);
        }

        _routine = UpdateY(targetPoint);
        StartCoroutine(_routine);
    }

    IEnumerator UpdateY(Vector3 target)
    {
        this.increment = 0f;

        while (this.increment < 2.5f)
        {
            Vector3 moveLerp = Vector3.Lerp(transform.position, target, Time.deltaTime * 2f);
            this.increment += Time.deltaTime;
            moveLerp.y = 0.5f * Mathf.Sin(Mathf.Clamp01(this.increment) * 5f);
            _anchorTransform.position = moveLerp;

            yield return new WaitForSeconds(0);
        }
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
        _anchor.transform.SetParent(null);

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
