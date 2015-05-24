using UnityEngine;


public class ComputerAssaultPlayer : Player, IAssaultable {

	public float fieldOfView = 60f;

	private bool shouldFireSnowball = false;

	private ComputerPlayerApproachLimit limitChecker;
	private NavMeshAgent router;
	private GameObject humanPlayer;

	void Awake() {
		limitChecker = GetComponentInParent<ComputerPlayerApproachLimit>();
		router = GetComponent<NavMeshAgent>();
		humanPlayer = GameObject.FindGameObjectWithTag("player");
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
			if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit)) {
				// ... and if the raycast hits the player...
				if(hit.collider.tag == "player") {
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

		assault();

		if(limitChecker.shouldStopMoving == false) {
			// move
		}
		if(shouldFireSnowball == true) {
			float angle = FindAngle(transform.forward, humanPlayer.transform, transform.up);
			// lookat
			// set shoothole "horizontal".

			Shoot(.7f);
		}
	}
}