using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour {

    private InFrontOfCamera _camScript;
    private Camera _cam;
    private JointScript _cacheJointScript;
    private bool hitTarget, holding;
    private RaycastController _raycastController;

    [SerializeField]
    private LayerMask _layer;

    [SerializeField]
    private LayerMask _doomLayer;

    private void Start()
    {
        _cam = Camera.main;
        _camScript = _cam.GetComponent<InFrontOfCamera>();
        _raycastController = new RaycastController (_cam, _doomLayer);

    }
    // Update is called once per frame
    void Update () {

#if UNITY_EDITOR

        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if(Physics.Raycast(ray,out hitInfo,3f,_layer))
            {
                
                if (hitInfo.collider.GetComponent<JointScript>() != null)
                {
                    _cacheJointScript = hitInfo.collider.GetComponent<JointScript>();
                    hitTarget = true;
                    holding = true;
                }
            }

            if (hitTarget)
            {
                    _cacheJointScript.Grabbed();
                    _camScript.SetAnchorTransformTarget(_cacheJointScript.ReturnAnchorTransform());
            }
         }

        if(Input.GetMouseButtonUp(0))
        {
            hitTarget = false;
        }

        if (holding)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Swiped UP");

                if (_raycastController.IsHit())
                {
                    _cacheJointScript.Release(_raycastController.ReturnHitVectorPoint());
                }
                else
                {
                    _cacheJointScript.Release(_camScript.ReturnVectorPoint());
                }

                holding = false;
                _cacheJointScript = null;
            }
        }

#endif

        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            Ray ray = _cam.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit,3,_layer))
            {
                if(hit.collider.GetComponent<JointScript>() != null)
                {
                    _cacheJointScript = hit.collider.GetComponent<JointScript>();
                    hitTarget = true;
                    holding = true;
                }
            }

            if(hitTarget)
            {
                if(SwipeManager.IsSwipingDown() || SwipeManager.IsSwipingDownLeft() || SwipeManager.IsSwipingDownRight())
                {
                    _cacheJointScript.Grabbed();
                    _camScript.SetAnchorTransformTarget(_cacheJointScript.ReturnAnchorTransform());
                }
            }

            if(touch.phase == TouchPhase.Ended)
            {
                hitTarget = false;
            }

            if (holding)
            {
                if (SwipeManager.IsSwipingUp() || SwipeManager.IsSwipingUpLeft() || SwipeManager.IsSwipingUpRight())
                {
                    Debug.Log("Swiped UP");

                    if (_raycastController.IsHit())
                    {
                        _cacheJointScript.Release(_raycastController.ReturnHitVectorPoint());
                    }
                    else
                    {
                        _cacheJointScript.Release(_camScript.ReturnVectorPoint());
                    }

                    holding = false;
                    _cacheJointScript = null;
                }

            }


        }
	}
}
