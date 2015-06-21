using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HumanPlayer : Player, IDamageable {
	GameObject hud;
	Slider powerSlider;
	Text angleText;
	Text weightText;
	bool isMouseDown = false;
	float thrust;

	// Use this for initialization
	protected new void Start () {
		base.Start();

		hud = GameObject.Find("/HUDCanvas");
		weightText = hud.transform.Find("WeightUI/Weight").gameObject.GetComponent<Text>() as Text;

		angleText = hud.transform.Find("ShootingUI/Angle").gameObject.GetComponent<Text>() as Text;

		powerSlider = hud.transform.Find("ShootingUI/PowerSlider").gameObject.GetComponent<Slider>() as Slider;
		powerSlider.minValue = minThrust;
		powerSlider.maxValue = maxThrust;
		powerSlider.value = minThrust;
	}
	
	// Update is called once per frame
	protected new void Update () {
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
			powerSlider.value = minThrust;
		}
		
		if(isMouseDown) {
			thrust += thrustFactor * Time.deltaTime;
			if(thrust > maxThrust) thrust = maxThrust;
			powerSlider.value = thrust;
		}
		
		weightText.text = weight.ToString();
	}

	protected new void FixedUpdate() {
		base.FixedUpdate();
		
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
		movement = movement.normalized * speedFactor * Time.fixedDeltaTime;

		Move(movement);
	}

	public new void RotateAim(float x, float y) {
		base.RotateAim(x, y);
		angleText.text = shoothole.transform.eulerAngles.x.ToString();
	}
	
	public new bool TakeDamage(IDamage damage) {
		Debug.Log ("HumanPlayer damaged");
		bool ret = base.TakeDamage(damage);

		return ret;
	}
}
