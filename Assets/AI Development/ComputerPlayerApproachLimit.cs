using UnityEngine;
using System.Collections;

public class ComputerPlayerApproachLimit : MonoBehaviour {

	public bool shouldStopMoving = false;

	//private GameObject humanPlayer;

	//void Awake() {
	//	humanPlayer = GameObject.FindGameObjectWithTag("player");
	//}

	void OnTriggerEnter(Collider sphereCollider) {
		if(sphereCollider.tag == "Player")
			shouldStopMoving = true;
	}

	void OnTriggerExit(Collider sphereCollider) {
		if(sphereCollider.tag == "Player")
			shouldStopMoving = false;
	}
}
