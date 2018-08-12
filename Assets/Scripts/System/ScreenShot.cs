using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour {


#if UNITY_EDITOR
    void Update ()
    {
		if (Input.GetKeyDown(KeyCode.S))
        {
            ScreenCapture.CaptureScreenshot("screen.png");
            print("scrrenshot");
        }
	}
#endif
}
