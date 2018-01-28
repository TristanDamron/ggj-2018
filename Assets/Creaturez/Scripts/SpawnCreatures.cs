using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCreatures : MonoBehaviour {

    private Transform _camTrans;

	void Start () 
    {
        _camTrans = Camera.main.GetComponent<Transform>();
	}

}
