using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameRule : MonoBehaviour {
	public GameObject[] enamyPrefabs;
	public double playingDuration = 60;
	public int maxEnemy = 20;
	float spawnInterval = 5f;

	double startTime;
	double endTime;
	GameObject hud;
	Text remainedTime;
	Text scoreText;
	int score;
	
	// Use this for initialization
	void Start () {
		this.score = 0;
		hud = GameObject.Find("/HUDCanvas");
		endTime = Time.time + playingDuration;
		
		remainedTime = hud.transform.Find("StatusUI/Time").gameObject.GetComponent<Text>() as Text;
		remainedTime.text = playingDuration.ToString();

		Invoke("SpawnEnemy", spawnInterval);

		scoreText = hud.transform.Find("StatusUI/Score").gameObject.GetComponent<Text>() as Text;
		scoreText.text = score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		int minutes, second, millisecond;
		double currentTime;
		
		if (this.IsEnd()) {
			endTime = Time.time;
			Destroy(GameObject.FindGameObjectWithTag("Player"));
			CancelInvoke("SpawnEnemy");
		}
		
		currentTime = endTime - Time.time; 
		minutes = (int)currentTime / 60;
		second = (int)currentTime % 60;
		
		millisecond = (int)((currentTime - (int)currentTime) * 100);
		remainedTime.text = string.Format("{0:D2}:{1:D2}:{2:D2}", minutes, second, millisecond);
			
		scoreText.text = score.ToString() + " kill";	 
	}
	
	bool IsEnd() {
		if (endTime <= Time.time) {
			return true;
		}
		if(null == GetHumanPlayer())
			return true;

		return false;
	}

	Transform PickRandomSpawnPoint() {
		GameObject spawnPoints = GameObject.Find("/SpawnPoints");
		Transform[] spawnPointList = spawnPoints.GetComponentsInChildren<Transform>();

		return spawnPointList[Random.Range(1, spawnPointList.Length)];
	}

	void SpawnEnemy() {
		Transform place = PickRandomSpawnPoint();

		Debug.Log (GetComputerPlayers().Length);
		if(GetComputerPlayers().Length < maxEnemy)
			AddComputerPlayer(place, Random.Range(0, enamyPrefabs.Length));

		if(!IsEnd())
			Invoke("SpawnEnemy", spawnInterval);
	}

	public GameObject GetHumanPlayer() {
		return GameObject.Find("/HumanPlayer");
	}

	GameObject[] GetComputerPlayers() {
		return GameObject.FindGameObjectsWithTag("Enemy");
	}

	public void AddComputerPlayer(Transform place, int type) {
		GameObject enemy = Instantiate(enamyPrefabs[type], place.position, Quaternion.identity) as GameObject;		
	}

	public void increaseScore() {
		this.score++;
	}
}