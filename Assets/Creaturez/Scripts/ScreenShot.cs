using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System.IO;

public class ScreenShot : MonoBehaviour {
	[SerializeField]
	private List<Texture2D> _images;
	[SerializeField]
	private List<string> _paths;
	[SerializeField]
	private int _index;
	[SerializeField]
	private Image _display;
	[SerializeField]
	private GameObject _screenShotPanel;
	[SerializeField]
	private GameObject _messagePanel;
	[SerializeField]
	private string _masterPath;

	void Start() {
        if (Application.platform == RuntimePlatform.IPhonePlayer) {
			
			if (!File.Exists(Application.persistentDataPath + "/images")) {
                Directory.CreateDirectory(@Application.persistentDataPath + "/images");
			}

		    _masterPath = Application.persistentDataPath + "/images";
		} else {
			_masterPath = Application.persistentDataPath;
		}
		
		StartCoroutine(PopulateList());		
	}

	public void TakeScreenShot() {
		DisplayMessage();
		ScreenCapture.CaptureScreenshot(_masterPath + "/image" + Random.Range(100000f, 1000000f) + ".png");
		StartCoroutine(PopulateList());
	}

	public void DisplayMessage() {
		_messagePanel.SetActive(true);
		Invoke("DisableMessage", 3f);
	}

	public void DisableMessage() {
		_messagePanel.SetActive(false);
	}

	public IEnumerator PopulateList() {
		//Ensure that the lists are empty
		_images.Clear();
		_paths.Clear();
		string[] files = Directory.GetFiles(@_masterPath, "*.png");			
		foreach (string f in files) {
			WWW www = new WWW(f);
			yield return www;		
			_paths.Add(f);
			_images.Add(www.texture);
		}
	}

	public void OpenScreenShotPanel() {
		_screenShotPanel.SetActive(true);
		DisplayImage();
	}

	public void CloseScreenShotPanel() {
		_screenShotPanel.SetActive(false);		
	}

	public void GetNext() {
		if (_index < _images.Count - 1)
			_index++;
		DisplayImage();
	}

	public void GetPrev() {
		if (_index > 0) 
			_index--;
		DisplayImage();
	}

	public void DisplayImage() {
		Sprite tmp = Sprite.Create(_images[_index], new Rect(0f, 0f, _images[_index].width, _images[_index].height), new Vector2(0.5f, 0.5f), 100f);
		_display.sprite = tmp;	
	}

	public void DeleteImage() {
		File.Delete(_paths[_index]);		
		GetPrev();
		StartCoroutine(PopulateList());
	}
}
