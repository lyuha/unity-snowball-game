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
	public int minWeight = 0;
	public int maxWeight = 200;
	public int weight = 100;
	public int dealAmount = 10;
	public AudioSource audioSource;

	public GameObject model;
	public GameObject shoothole;
	public Rigidbody rigidbody;
	protected float movedDistance;

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
			movedDistance += movement.magnitude;
			if(movedDistance >= 5f) {
				movedDistance = 0f;
				weight += 1;
			}

			rigidbody.MovePosition(transform.position + transform.TransformVector(movement));
			if(!audioSource.isPlaying) {
				audioSource.clip = walkAudioClip;
				audioSource.Play();
			}
		}
	}

	public void Shoot(float thrust) {
		GameObject ball = Instantiate(snowBall, shoothole.transform.position, Quaternion.identity) as GameObject;
		Snowball ballBehaviour = ball.GetComponent<Snowball>();

		ballBehaviour.damageAmount = dealAmount * 2;
		ballBehaviour.damageSource = this;
		ball.gameObject.GetComponent<Rigidbody>().AddForce(shoothole.transform.forward * thrust, ForceMode.Impulse);

		GameObject.Destroy(ball, timeLimit);
		TakeDamage(dealAmount);
	}

	public void TakeDamage(int damage) {
		weight -= damage;
		
		if(weight <= minWeight || weight >= maxWeight) {
			Destroy(this.gameObject);
		}
	}

	public bool TakeDamage(IDamage damage) {
		Debug.Log ("Player damaged");
		if(damage.GetDamageSource() != this)
			TakeDamage(damage.GetDamageAmount());

		return true;
	}
}
