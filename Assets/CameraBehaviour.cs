using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {
	Camera rootCam;

	void Start() {
		rootCam = transform.Find("/RootCamera").GetComponent<Camera>() as Camera;
	}

	void OnDestroy() {
		rootCam.CopyFrom(transform.GetComponent<Camera>() as Camera);
		rootCam.enabled = true;
	}
}
