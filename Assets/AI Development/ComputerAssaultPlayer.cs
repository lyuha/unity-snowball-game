using UnityEngine;
using System.Collections;

public class ComputerAssaultPlayer : ComputerPlayer, IAssaultable {

	public float fieldOfView = 60f;
    public float angularSpeed = 5f;
    public float shootingInterval = 1f;
    public float shootingRange = 10f;
	private bool readyToFire = true;
	private NavMeshAgent router;
	private GameObject humanPlayer;
	private ComputerPlayerApproachLimit approachLimit;
	void Awake() {
		router = GetComponent<NavMeshAgent>();
		//router.updateRotation = false;
		humanPlayer = GameObject.FindGameObjectWithTag("Player");
		approachLimit = GetComponentInChildren<ComputerPlayerApproachLimit>();
	}
	public void assault() {
		
		// Create a vector from the enemy to the player and store the angle between it and forward.
		Vector3 direction = humanPlayer.transform.position - transform.position;
        float angle = FindAngle(transform.forward, humanPlayer.transform.position - transform.position, transform.up);
		
		// If the angle between forward and where the player is, is less than half the angle of view...
		if(angle < fieldOfView / 2) 
        {
			RaycastHit hit;
			
			// ... and if a raycast towards the player hits something...
			Debug.DrawRay(transform.position + transform.forward, direction.normalized, Color.red);
			if(Physics.Raycast(transform.position + transform.forward, direction.normalized, out hit, shootingRange)) {

				// ... and if the raycast hits the player...
                if (hit.collider.tag == "Player")
                {

                    // ... the player is in sight.

                    shoothole.transform.LookAt(humanPlayer.transform.position);
                    if (readyToFire)
                        StartCoroutine("launch");
                }
                else
                {
                    StopCoroutine("launch");
                    readyToFire = true;
                }
			}

		}
        transform.Rotate(0, angle * angularSpeed , 0);

	}
    private float FindAngle(Vector3 from, Vector3 to, Vector3 up)
    {
        if (to == Vector3.zero)
        {
            return 0f;
        }

        float angle = Vector3.Angle(from, to);

        Vector3 normal = Vector3.Cross(from, to);
        angle *= Mathf.Sign(Vector3.Dot(normal, up));
        angle *= Mathf.Deg2Rad;
        return angle;
    }
    private IEnumerator launch()
    {
        readyToFire = false;
        Shoot((maxThrust + minThrust) / 2);
        yield return new WaitForSeconds(shootingInterval);
        readyToFire = true;
    }
	new void Update() {
		base.Update();
		router.destination = humanPlayer.transform.position;
		router.speed = 5f;
		assault();
		if(approachLimit.shouldStopMoving)
			router.Stop();
		else
			router.Resume();

		/*if(shouldFireSnowball == true) {
			float angle = FindAngle(transform.forward, humanPlayer.transform.position, transform.up);
			// lookat
			// set shoothole "horizontal".
			Shoot(minThrust);
		}*/
	}
}