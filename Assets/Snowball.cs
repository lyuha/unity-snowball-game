using UnityEngine;
using System.Collections;

public class Snowball : MonoBehaviour, IDamage {
	public AudioClip hitAudio;
	public int damageAmount = 10;
	public MonoBehaviour damageSource;

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
	/*
	void OnTriggerEnter (Collider other) {
		Debug.Log (other);
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
*/
	void Hit() {
		audioSource.clip = hitAudio;
		audioSource.Play();
		GameObject.Destroy(sphere);
	}

	public MonoBehaviour GetDamageSource() {
		return damageSource;
	}

	public int GetDamageAmount() {
		return damageAmount;
	}
}
