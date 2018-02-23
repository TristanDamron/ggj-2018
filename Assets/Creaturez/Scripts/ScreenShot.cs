using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenShot : MonoBehaviour {
	public void TakeScreenShot() {
		ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/image" + Random.Range(100000f, 1000000f) + ".png");
	}
}
