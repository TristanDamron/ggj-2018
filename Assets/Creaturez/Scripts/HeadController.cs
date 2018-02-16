using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour {
	[SerializeField]
	private int _lastHP;

	void Start() {
		InvokeRepeating("CheckHP", 0f, 0.1f);
	}

	void Update () {	
		if (Manager.creaturezHP != _lastHP) {
			//TODO: Trigger animations and SFX on hit
		}		
	}

	private void CheckHP() {
		_lastHP = Manager.creaturezHP;
	} 

	private void InvokeDestroy() {
		Destroy(gameObject);
	}
}
