using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTentacle : MonoBehaviour {

    [SerializeField]
    List<Transform> _tentacles;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        foreach (var tentacle in _tentacles)
        {
            
        }
	}
}
