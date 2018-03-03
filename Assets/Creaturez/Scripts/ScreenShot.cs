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
	[SerializeField]
	private Sprite _defaultSprite;
	[SerializeField]
	private Canvas _canvas;

	void Start() {
        if (Application.platform == RuntimePlatform.IPhonePlayer) {
			
			if (!File.Exists(Application.persistentDataPath + "/images")) {
                Directory.CreateDirectory(@Application.persistentDataPath + "/images");
			}

		    _masterPath = Application.persistentDataPath + "/images";
		} else {
			_masterPath = Application.persistentDataPath;
		}

		Debug.Log(_masterPath);


		StartCoroutine(PopulateList());		
	}

	public void TakeScreenShot() {
		HideCanvas();	
		var name = "/image" + Random.Range(100000f, 1000000f) + ".png";
		ScreenCapture.CaptureScreenshot(_masterPath + name);
		Invoke("ShowCanvas", 0.01f);
		StartCoroutine(PopulateList());
		StartCoroutine(DisplayMessage(name));		
	}

	public IEnumerator DisplayMessage(string name) {
		yield return new WaitForSeconds(0.5f);		
		_messagePanel.SetActive(true);	
		if (File.Exists(_masterPath + name)) {
			_messagePanel.GetComponent<Image>().color = Color.green;
			_messagePanel.GetComponentInChildren<Text>().text = "Screenshot saved successfully into gallery";
		} else {
			_messagePanel.GetComponent<Image>().color = Color.red;			
			_messagePanel.GetComponentInChildren<Text>().text = "Screenshot failed to save";
		}
		Invoke("DisableMessage", 3f);
	}

	public void DisableMessage() {
		_messagePanel.SetActive(false);
	}

	public void HideCanvas() {
		_canvas.enabled = false;
	}

	public void ShowCanvas() {
		_canvas.enabled = true;
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
		Sprite tmp;
		if (_images.Count == 0) 
			tmp = _defaultSprite;
		else
			tmp = Sprite.Create(_images[_index], new Rect(0f, 0f, _images[_index].width, _images[_index].height), new Vector2(0.5f, 0.5f), 100f);
		
		_display.sprite = tmp;	
	}

	public void DeleteImage() {
		File.Delete(_paths[_index]);		
		StartCoroutine(PopulateList());	
	}
}
