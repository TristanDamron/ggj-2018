using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class HeadController : MonoBehaviour {
	[SerializeField]
	private int _lastHP;
	[SerializeField]
	private AudioClip _hit;	

	void Start() {
		InvokeRepeating("CheckHP", 0f, 0.1f);
	}

	void Update () {	
		if (Manager.creaturezHP != _lastHP) {
			GameObject.Find("Audio Source").GetComponent<AudioSource>().PlayOneShot(_hit, 0.2f);										
		}		
	}

	private void CheckHP() {
		_lastHP = Manager.creaturezHP;
	} 

	private void InvokeDestroy() {
		ARKitWorldTrackingSessionConfiguration sessionConfig = new ARKitWorldTrackingSessionConfiguration ( UnityARAlignment.UnityARAlignmentGravity, UnityARPlaneDetection.Horizontal);
        UnityARSessionNativeInterface.GetARSessionNativeInterface().RunWithConfigAndOptions(sessionConfig, UnityARSessionRunOption.ARSessionRunOptionRemoveExistingAnchors | UnityARSessionRunOption.ARSessionRunOptionResetTracking);		
		
		Destroy(gameObject);
	}
}
