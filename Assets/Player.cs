using UnityEngine;
using System.Collections;
[RequireComponent (typeof(Rigidbody))]
public class Player : MonoBehaviour, IDamageable {
	public AudioClip walkAudioClip;
	public GameObject snowBall;
	public float speedFactor = 15f;
	public float thrustFactor = 20f;
	public float minThrust = 5f;
	public float maxThrust = 50f;
	public float ballLifetime = 10f;
	public float weightingDistance = 3f;
	public int minWeight = 0;
	public int maxWeight = 200;
	public int weight = 100;
	public int dealAmount = -5;

	protected AudioSource audioSource;
	protected GameObject model;
	protected GameObject shoothole;
	protected CharacterController characterController;
	protected float movedDistance;

	// Use this for initialization
	protected void Start () {
		model = transform.FindChild("Model").gameObject;
		characterController = GetComponent<CharacterController>();
		audioSource = GetComponent<AudioSource>();

		shoothole = transform.FindChild("Shoothole").gameObject;
	}

	// Update is called once per frame
	protected void Update () {
		if(IsDead()) {
			Destroy(this.gameObject);
		}
		ChangeSpeed();
	}

	protected void FixedUpdate () {
		Move(new Vector3(0f, -20f * Time.fixedDeltaTime, 0f));
	}

	public void RotateAim(float x, float y) {
		transform.Rotate(0, x, 0);
		shoothole.transform.Rotate(y, 0, 0);
	}

	public void Move(Vector3 movement, bool isHumanPlayer) {
		if(movedDistance >= weightingDistance) {
			movedDistance -= weightingDistance;

			weight += 1;
		}
		
		//rigidbody.MovePosition(transform.position + transform.TransformVector(movement));
		if(isHumanPlayer)
			characterController.Move(transform.TransformVector(movement));
		
		// movement
		movement.y = 0f;
		movedDistance += movement.magnitude;

		/*if(!isHumanPlayer)
		{
			movedDistance *= 100;
		}*/

		if(movement.magnitude != 0f) {
			if(!audioSource.isPlaying) {
				audioSource.clip = walkAudioClip;
				audioSource.Play();
			}
		}
	}

	public void Move(Vector3 movement){
		Move (movement, true);
	}

	public void Shoot(float thrust) {
		GameObject ball = Instantiate(snowBall, shoothole.transform.position, Quaternion.identity) as GameObject;
		Snowball ballBehaviour = ball.GetComponent<Snowball>();

		ballBehaviour.damageAmount = dealAmount * 4;
		ballBehaviour.damageSource = this;
		ball.gameObject.GetComponent<Rigidbody>().AddForce(shoothole.transform.forward * thrust, ForceMode.Impulse);
		ball.gameObject.GetComponent<Rigidbody>().AddForce(characterController.velocity, ForceMode.VelocityChange);

		GameObject.Destroy(ball, ballLifetime);
		TakeDamage(-dealAmount);
	}

	public void TakeDamage(int damage) {
		weight += damage;
	}

	public bool TakeDamage(IDamage damage) {
		Debug.Log ("Player damaged");
		if(damage.GetDamageSource() != this)
			TakeDamage(damage.GetDamageAmount());

		return true;
	}
	
	bool IsDead() {
		if(this.weight <= minWeight || weight >= maxWeight) {
			return true;
		}
		return false;
	}
	
	void ChangeSpeed() {
		speedFactor = 15 - (weight / 20);  
	}

	public float FindAngle (Vector3 fromVector, Vector3 toVector, Vector3 upVector) {
		// If the vector the angle is being calculated to is 0...
		if(toVector == Vector3.zero)
			// ... the angle between them is 0.
			return 0f;
		
		// Create a float to store the angle between the facing of the enemy and the direction it's travelling.
		float angle = Vector3.Angle(fromVector, toVector);
		
		// Find the cross product of the two vectors (this will point up if the velocity is to the right of forward).
		Vector3 normal = Vector3.Cross(fromVector, toVector);
		
		// The dot product of the normal with the upVector will be positive if they point in the same direction.
		angle *= Mathf.Sign(Vector3.Dot(normal, upVector));
		
		// We need to convert the angle we've found from degrees to radians.
		angle *= Mathf.Deg2Rad;
		
		return angle;
	}

}
