using UnityEngine;


public class ComputerAssaultPlayer : ComputerPlayer, IAssaultable {

	public float fieldOfView = 60f;

	private bool shouldFireSnowball = false;
	private NavMeshAgent router;
	private GameObject humanPlayer;

	void Awake() {
		router = GetComponent<NavMeshAgent>();
		//router.updateRotation = false;
		humanPlayer = GameObject.FindGameObjectWithTag("Player");
	}

	public void assault() {
		shouldFireSnowball = false;
		
		// Create a vector from the enemy to the player and store the angle between it and forward.
		Vector3 direction = humanPlayer.transform.position - transform.position;
		float angle = Vector3.Angle(direction, transform.forward);
		
		// If the angle between forward and where the player is, is less than half the angle of view...
		if(angle < fieldOfView / 2) {
			RaycastHit hit;
			
			// ... and if a raycast towards the player hits something...
			Debug.DrawRay(transform.position + transform.forward, direction.normalized, Color.red);
			if(Physics.Raycast(transform.position + transform.forward, direction.normalized, out hit)) {

				// ... and if the raycast hits the player...
				if(hit.collider.tag == "Player") {
					//router.updateRotation = false;
					transform.Rotate(0, direction.sqrMagnitude, 0);
					// ... the player is in sight.
					shouldFireSnowball = true;
					// Set the last global sighting is the players current position.
					//lastPlayerSighting.position = player.transform.position;
				}
			}

		}

	}
	new void Update() {
		base.Update();
		router.destination = humanPlayer.transform.position;
		router.speed = 5f;
		assault();

		if(shouldFireSnowball == true) {
			float angle = FindAngle(transform.forward, humanPlayer.transform.position, transform.up);
			// lookat
			// set shoothole "horizontal".
			Shoot(minThrust);
		}
	}
}