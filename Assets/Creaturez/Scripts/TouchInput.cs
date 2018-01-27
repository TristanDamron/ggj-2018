using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour {

    private Camera _cam;

    [SerializeField]
    private LayerMask _layer;


    private void Start()
    {
        _cam = Camera.main;
    }
    // Update is called once per frame
    void Update () {




        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray,out hitInfo,10f,_layer))
            {
                if(hitInfo.collider.name == "eatme")
                {
                    
                }
            }

        }
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            Ray ray = _cam.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit,10,_layer))
            {
                if(hit.collider.tag == "Interactive")
                {
                    
                }
            }

        }
	}
}
