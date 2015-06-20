using UnityEngine;
using System.Collections;

public class ComputerGuerillaPlayer : ComputerPlayer, IAssaultable, ISneakable, IRetreatable{


    private GameObject humanPlayer;
    private NavMeshAgent router;
    public int RetreatHpThreshold = 50;
    public float fieldOfView = 60f;
    public float angularSpeed = 5f;
    public int AssultHpThreshold = 150;
    public float shootingRange = 10f;
    private bool isRetreating;
    private GameObject[] hidingSpots;
    public float shootingInterval = 1f;
    private bool readyToFire = true;
	private Vector3 previousPostion;
	private ComputerPlayerApproachLimit approachLimit;
    void Awake()
    {
        router = GetComponent<NavMeshAgent>();
        humanPlayer = GameObject.FindGameObjectWithTag(Tags.Player);
        isRetreating = false;
        hidingSpots = GameObject.FindGameObjectsWithTag(Tags.CoverSpot);
        router.destination = humanPlayer.transform.position;
		previousPostion = transform.position;
		approachLimit = GetComponentInChildren<ComputerPlayerApproachLimit>();
    }
    new void Update()
    {
        base.Update();
        checkHealth();
        if (isRetreating)
            return;
        if (weight <= RetreatHpThreshold)
        {
            retreat();
        }
		else{
			assault();

			if(approachLimit.shouldStopMoving)
				router.Stop();
			else
				router.Resume();
		}
            
			


		Vector3 shortDestination = router.nextPosition - previousPostion;
		Move (shortDestination.normalized, false);
		previousPostion = router.nextPosition;
    }

    void checkHealth()
    {
        if (weight >= AssultHpThreshold)
        {
            isRetreating = false;
        }
    }
	public void assault() {

        // Create a vector from the enemy to the player and store the angle between it and forward.
        Vector3 direction = humanPlayer.transform.position - transform.position;
        float angle = FindAngle(transform.forward, humanPlayer.transform.position - transform.position, transform.up);

        // If the angle between forward and where the player is, is less than half the angle of view...
        if (angle < fieldOfView / 2)
        {
            RaycastHit hit;

            // ... and if a raycast towards the player hits something...
            Debug.DrawRay(transform.position + transform.forward, direction.normalized, Color.red);
            if (Physics.Raycast(transform.position + transform.forward, direction.normalized, out hit, shootingRange))
            {

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
        transform.Rotate(0, angle * angularSpeed, 0);

		router.destination = humanPlayer.transform.position;
	}

	public void sneak(){

	}

    private IEnumerator launch()
    {
        readyToFire = false;
        Shoot(minThrust);
        yield return new WaitForSeconds(shootingInterval);
        readyToFire = true;
    }
	public void retreat(){
        Vector3 defaultHidingPosDistance = hidingSpots[0].transform.position - transform.position;
        float sqrDistance = defaultHidingPosDistance.sqrMagnitude;
        foreach (GameObject hidingSpot in hidingSpots)
        {
            Vector3 hidingDistance = hidingSpot.transform.position - transform.position;
            float sqrHidingDistance = hidingDistance.sqrMagnitude;
            if (sqrDistance <= sqrHidingDistance)
            {
                sqrDistance = sqrHidingDistance;
                defaultHidingPosDistance = hidingSpot.transform.position;
            }
        }
        router.destination = defaultHidingPosDistance;
	}
}
