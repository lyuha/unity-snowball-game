using UnityEngine;
using System.Collections;

public class ThrowingBall : MonoBehaviour {
	public GameObject snowBall;
	public float thrustFactor = 20f;
	public float maxThrust = 15f;
	public float minThrust = 5f;
	public float timeLimit = 30f;
	float thrust;
	bool isDown = false;
	Ray ray;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		float mouseY = Input.GetAxis("Mouse Y");
		transform.Rotate(-mouseY * 100f * Time.deltaTime, 0, 0);
		Debug.DrawRay (transform.position, transform.forward, Color.green, 1f);

		if(Input.GetMouseButtonDown(0)) {
			isDown = true;
			thrust = minThrust;
		}
		else if(Input.GetMouseButtonUp(0)) {
			isDown = false;
			GameObject clone = Instantiate(snowBall, transform.position, Quaternion.identity) as GameObject;
			clone.GetComponent<Rigidbody>().AddForce(transform.forward * thrust, ForceMode.Impulse);
			GameObject.Destroy(clone, timeLimit);
		}

		if(isDown) {
			thrust += thrustFactor * Time.deltaTime;
			if(thrust > maxThrust) thrust = maxThrust;
		}
	}
}
