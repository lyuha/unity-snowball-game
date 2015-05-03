using UnityEngine;
using System.Collections;

public class BallCollision : MonoBehaviour {
	public AudioClip hitAudio;
	AudioSource audioSource;
	GameObject sphere;

	void Start() {
		audioSource = GetComponent<AudioSource>();
		sphere = transform.FindChild("Sphere").gameObject;

	}

	void OnCollisionEnter (Collision other) {
		switch(other.gameObject.tag) {
		case "Terrain":
			Hit ();
			break;
		case "Hittable":
			GameObject.Destroy(other.gameObject);
			Hit ();
			break;
		}
	}

	void Hit() {
		audioSource.clip = hitAudio;
		audioSource.Play();
		print(sphere.transform.position);
		print(audioSource.minDistance);
		print(audioSource.maxDistance);
		GameObject.Destroy(sphere);
	}
}
