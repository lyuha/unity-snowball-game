using UnityEngine;

public class ComputerCamperPlayerProximityLimit: MonoBehaviour
{
	public bool shouldFlee = false;

	void OnTriggerEnter(Collider sphereCollider) {
		if(sphereCollider.tag == "Player")
			shouldFlee = true;
	}
	
	void OnTriggerExit(Collider sphereCollider) {
		if(sphereCollider.tag == "Player")
			shouldFlee = false;
	}
}