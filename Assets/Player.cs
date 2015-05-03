using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public AudioClip walkAudioClip;
	public GameObject snowBall;
	public float speedFactor = 6f;
	public float thrustFactor = 20f;
	public float maxThrust = 20f;
	public float minThrust = 5f;
	public float timeLimit = 30f;

	bool isMoving = false;
	bool isMouseDown = false;
	Rigidbody rigidbody;
	AudioSource audioSource;
	GameObject shootHole;
	float thrust;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
		shootHole = transform.FindChild("ShootHole").gameObject;
		Cursor.lockState = CursorLockMode.Locked;
	}

	// Update is called once per frame
	void Update () {
		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = Input.GetAxis("Mouse Y");
		
		transform.Rotate(0, mouseX * Time.deltaTime * 100f, 0);
		shootHole.transform.Rotate(-mouseY * 100f * Time.deltaTime, 0, 0);

		if(Input.GetMouseButtonDown(0)) {
			isMouseDown = true;
			thrust = minThrust;
		}
		else if(Input.GetMouseButtonUp(0)) {
			isMouseDown = false;
			GameObject clone = Instantiate(snowBall, shootHole.transform.position, Quaternion.identity) as GameObject;
			clone.GetComponent<Rigidbody>().AddForce(shootHole.transform.forward * thrust, ForceMode.Impulse);
			GameObject.Destroy(clone, timeLimit);
		}
		
		if(isMouseDown) {
			thrust += thrustFactor * Time.deltaTime;
			if(thrust > maxThrust) thrust = maxThrust;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		if(moveHorizontal != 0f || moveVertical != 0f) {
			if(!isMoving) {
				isMoving = true;
				audioSource.clip = walkAudioClip;
				audioSource.Play();
			}
		}
		else {
			if(isMoving) {
				isMoving = false;
				audioSource.Stop();
			}
		}
		
		Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
		
		movement = movement.normalized * speedFactor * Time.fixedDeltaTime;
		rigidbody.MovePosition(transform.position + transform.TransformVector(movement));
	}
}
