using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public AudioClip walkAudioClip;
	public float speed = 6f;
	bool isMoving = false;
	Rigidbody rigidbody;
	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
		Screen.lockCursor = true;
	}

	void Update() {
		float mouseX = Input.GetAxis("Mouse X");

		transform.Rotate(0, mouseX * Time.deltaTime * 100f, 0);
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

		movement = movement.normalized * speed * Time.fixedDeltaTime;
		rigidbody.MovePosition(transform.position + transform.TransformVector(movement));
	}
}
