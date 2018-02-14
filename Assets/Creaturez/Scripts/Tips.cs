using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tips : MonoBehaviour {
	public enum  Events
	{
		Patrolling,
		PickedUpBird,
		FightingMonster
	}

	public Events current;
	[SerializeField]
	private Image _img;

	[SerializeField]
	private Sprite[] _images;
 
	void Start() {
		_img = GetComponent<Image>();
	}

	private void SetImage() {
		if (current == Events.Patrolling) {
			_img.sprite = _images[0]; 
		} else if (current == Events.PickedUpBird) {
			_img.sprite = _images[1]; 
		} else if (current == Events.FightingMonster) {
			_img.sprite = _images[2];  
		}
	}

	void Update () {
		Invoke("SetImage", 1f);
	}
}
