using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, IDamageable {
	public AudioClip walkAudioClip;
	public GameObject snowBall;
	public float speedFactor = 6f;
	public float thrustFactor = 20f;
	public float maxThrust = 20f;
	public float minThrust = 5f;
	public float timeLimit = 30f;
	public int weight = 100;
	public int dealAmount = 10;
	public AudioSource audioSource;

	public GameObject model;
	public GameObject shoothole;
	public Rigidbody rigidbody;

	// Use this for initialization
	protected void Start () {
		model = transform.FindChild("Model").gameObject;
		rigidbody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();

		shoothole = transform.FindChild("Shoothole").gameObject;
	}

	// Update is called once per frame
	protected void Update () {
	}

	protected void FixedUpdate () {

	}

	public void RotateAim(float x, float y) {
		transform.Rotate(0, x, 0);
		shoothole.transform.Rotate(y, 0, 0);
	}

	public void Move(Vector3 movement){
		if(movement.magnitude != 0f) {
			rigidbody.MovePosition(transform.position + transform.TransformVector(movement));
			if(!audioSource.isPlaying) {
				audioSource.clip = walkAudioClip;
				audioSource.Play();
			}
		}
	}

	public void Shoot(float thrust) {
		GameObject clone = Instantiate(snowBall, shoothole.transform.position, Quaternion.identity) as GameObject;
		clone.GetComponent<Rigidbody>().AddForce(shoothole.transform.forward * thrust, ForceMode.Impulse);
		GameObject.Destroy(clone, timeLimit);
	}

	public bool TakeDamage(IDamage damage) {
		Debug.Log ("Player damaged");
		weight -= damage.GetDamageAmount();

		if(weight <= 0 || weight >= 200) {
			Destroy(this.gameObject);
		}

		return true;
	}
}
