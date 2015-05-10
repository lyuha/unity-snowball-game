using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HumanPlayer : Player, IDamageable {
	bool isMouseDown = false;
	float thrust;

	// Use this for initialization
	protected void Start () {
		base.Start();
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	protected void Update () {
		base.Update();

		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = Input.GetAxis("Mouse Y");
		
		RotateAim(mouseX * Time.deltaTime * 100f, -mouseY * 100f * Time.deltaTime);

		if(Input.GetMouseButtonDown(0)) {
			isMouseDown = true;
			thrust = minThrust;
		}
		else if(Input.GetMouseButtonUp(0)) {
			isMouseDown = false;
			Shoot(thrust);
		}
		
		if(isMouseDown) {
			thrust += thrustFactor * Time.deltaTime;
			if(thrust > maxThrust) thrust = maxThrust;
		}
		
		Text weightUI = GameObject.Find("/HUDCanvas/WeightUI/Weight").GetComponent<Text>();
		weightUI.text = weight.ToString();
	}

	protected void FixedUpdate() {
		base.FixedUpdate();
		
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
		
		movement = movement.normalized * speedFactor * Time.fixedDeltaTime;
		Move(movement);
	}

	public bool TakeDamage(IDamage damage) {
		Debug.Log ("HumanPlayer damaged");
		bool ret = base.TakeDamage(damage);

		return ret;
	}
}
