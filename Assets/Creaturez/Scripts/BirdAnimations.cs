using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAnimations : MonoBehaviour 
{

    public JointScript _jointScript;

    Transform _camTrans;
    bool _neveDoThis, _andThis;

    Animator _anims;

    void Start()
    {
        _anims = GetComponent<Animator>();
        _camTrans = Camera.main.transform;
    }

    public void DisableThisGameObject()
    {
        gameObject.SetActive(false);
    }

    public void SetAnimation(string animNam)
    {
        
        _anims.Play(animNam);
    }

    public void LookAtCam()
    {
        _neveDoThis = true;
    }

    public void LookAway()
    {
        _neveDoThis = false;
        transform.rotation = _camTrans.rotation;
    }

    public void SetPos()
    {
        transform.position = _jointScript.GetComponent<Transform>().position;
    }

    public JointScript PassJoint()
    {
        return _jointScript;
    }

    public void LookAt(Transform transformPoint)
    {
        transform.LookAt(transformPoint);
    }


    private void Update()
    {
         //var axis = new Vector3(0, -1f, 0);
         //transform.RotateAround(_rotateAround.position, axis, 100 * Time.deltaTime);

        if (_neveDoThis)
        {
            transform.LookAt(_camTrans);
        }
    }
}
