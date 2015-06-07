﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameRule : MonoBehaviour {
	public GameObject enamyAssault;
	public double playingDuration = 60;
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
			
		scoreText.text = score.ToString();	 
	}
	
	bool IsEnd() {
		if (endTime <= Time.time) {
			return true;
		}
		return false;
	}

	Transform PickRandomSpawnPoint() {
		GameObject spawnPoints = GameObject.Find("/SpawnPoints");
		Transform[] spawnPointList = spawnPoints.GetComponentsInChildren<Transform>();

		return spawnPointList[Random.Range(0, spawnPointList.Length)];
	}

	void SpawnEnemy() {
		Transform place = PickRandomSpawnPoint();
		GameObject ememy = Instantiate(enamyAssault, place.position, Quaternion.identity) as GameObject;
		Debug.Log("spawned at: " + place.position);

		if(!IsEnd())
			Invoke("SpawnEnemy", spawnInterval);
	}
	
	void AddComputerPlayer() {
		
	}
	public void increaseScore() {
		this.score++;
	}
}