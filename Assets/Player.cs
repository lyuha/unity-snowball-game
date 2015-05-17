﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, IDamageable {
	public AudioClip walkAudioClip;
	public GameObject snowBall;
	public float speedFactor = 6f;
	public float thrustFactor = 20f;
	public float minThrust = 5f;
	public float maxThrust = 50f;
	public float ballLifetime = 30f;
	public float weightingDistance = 3f;
	public int minWeight = 0;
	public int maxWeight = 200;
	public int weight = 100;
	public int dealAmount = 5;

	protected AudioSource audioSource;
	protected GameObject model;
	protected GameObject shoothole;
	protected Rigidbody rigidbody;
	protected CharacterController characterController;
	protected float movedDistance;

	// Use this for initialization
	protected void Start () {
		model = transform.FindChild("Model").gameObject;
		rigidbody = GetComponent<Rigidbody>();
		characterController = GetComponent<CharacterController>();
		audioSource = GetComponent<AudioSource>();

		shoothole = transform.FindChild("Shoothole").gameObject;
	}

	// Update is called once per frame
	protected void Update () {
	}

	protected void FixedUpdate () {
		Move(new Vector3(0f, -20f * Time.fixedDeltaTime, 0f));
	}

	public void RotateAim(float x, float y) {
		transform.Rotate(0, x, 0);
		shoothole.transform.Rotate(y, 0, 0);
	}

	public void Move(Vector3 movement){
		if(movedDistance >= weightingDistance) {
			movedDistance -= weightingDistance;
			weight += 1;
		}
		
		//rigidbody.MovePosition(transform.position + transform.TransformVector(movement));
		characterController.Move(transform.TransformVector(movement));

		// movement
		movement.y = 0f;
		movedDistance += movement.magnitude;

		if(movement.magnitude != 0f) {
			if(!audioSource.isPlaying) {
				audioSource.clip = walkAudioClip;
				audioSource.Play();
			}
		}
	}

	public void Shoot(float thrust) {
		GameObject ball = Instantiate(snowBall, shoothole.transform.position, Quaternion.identity) as GameObject;
		Snowball ballBehaviour = ball.GetComponent<Snowball>();

		ballBehaviour.damageAmount = dealAmount * 4;
		ballBehaviour.damageSource = this;
		ball.gameObject.GetComponent<Rigidbody>().AddForce(shoothole.transform.forward * thrust, ForceMode.Impulse);

		GameObject.Destroy(ball, ballLifetime);
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
