using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class PlacePortalsOnWalls : MonoBehaviour {

    [SerializeField]
    GameObject _portalParent;


	void Start () {
        
        UnityARSessionNativeInterface.ARAnchorAddedEvent += PlaceOnAnchor;
	}
	
    public void PlaceOnAnchor(ARPlaneAnchor anchor)
    {
        
        Debug.Log("Anchor Values: " + "Pos: " + UnityARMatrixOps.GetPosition(anchor.transform));

        Debug.Log("anchor placed");

        var pos = UnityARMatrixOps.GetPosition(anchor.transform);
        var rot = UnityARMatrixOps.GetRotation(anchor.transform);

        var portal = Instantiate(_portalParent);

        portal.transform.position = pos;
        portal.transform.rotation = rot;

        var child = portal.GetComponentInChildren<Transform>();
        child.localPosition = anchor.center;

        //var rot = UnityARMatrixOps.GetRotation(anchor.transform);
        //rot.eulerAngles = new Vector3(rot.x, rot.y + 180f, rot.z);
        //child.rotation = rot;
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            var portal = Instantiate(_portalParent, transform.position, transform.rotation);
            portal.transform.localScale = new Vector3(.1f, .1f, .1f);
            portal.SetActive(true);


        }
    }

}
