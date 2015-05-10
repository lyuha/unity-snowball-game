using UnityEngine;
using System.Collections;

public class Snowball : MonoBehaviour, IDamage {
	public AudioClip hitAudio;
	public int damageAmount = 10;

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
		default:
			IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
			if(damageable != null && damageable.TakeDamage(this)) {
				Hit();
			}
			break;
		}
	}

	void Hit() {
		audioSource.clip = hitAudio;
		audioSource.Play();
		//print(sphere.transform.position);
		//print(audioSource.minDistance);
		//print(audioSource.maxDistance);
		GameObject.Destroy(sphere);
	}

	public GameObject GetDamageSource() {
		return this.gameObject;
	}

	public int GetDamageAmount() {
		return damageAmount;
	}
}
