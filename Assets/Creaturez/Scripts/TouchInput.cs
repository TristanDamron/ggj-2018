using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour {

    private InFrontOfCamera _camScript;
    private Camera _cam;
    private JointScript _cacheJointScript;
    bool hitTarget;

    [SerializeField]
    private LayerMask _layer;


    private void Start()
    {
        _cam = Camera.main;
        _camScript = _cam.GetComponent<InFrontOfCamera>();

    }
    // Update is called once per frame
    void Update () {
        
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if(Physics.Raycast(ray,out hitInfo,10f,_layer))
            {
                
                if (hitInfo.collider.GetComponent<JointScript>() != null)
                {
                    _cacheJointScript = hitInfo.collider.GetComponent<JointScript>();
                    hitTarget = true;
                }
            }

            if (hitTarget)
            {
                    _cacheJointScript.Grabbed();
                    _camScript.SetAnchorTransformTarget(_cacheJointScript.ReturnAnchorTransform());
            }
         }

        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            Ray ray = _cam.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit,10,_layer))
            {
                if(hit.collider.GetComponent<JointScript>() != null)
                {
                    _cacheJointScript = hit.collider.GetComponent<JointScript>();
                    hitTarget = true;
                }
            }

            if(hitTarget)
            {
                if (SwipeManager.IsSwipingUp() || SwipeManager.IsSwipingUpLeft() || SwipeManager.IsSwipingRight())
                {
                    //Throw the fucking bird
                }

                if(SwipeManager.IsSwipingDown() || SwipeManager.IsSwipingDownLeft() || SwipeManager.IsSwipingDownRight())
                {
                    _cacheJointScript.Grabbed();
                    _camScript.SetAnchorTransformTarget(_cacheJointScript.ReturnAnchorTransform());
                }
            }

            if(touch.phase == TouchPhase.Ended)
            {
                hitTarget = false;
                _cacheJointScript = null;
            }

        }
	}
}
