using UnityEngine;
using System.Collections;

public class GameRule : MonoBehaviour {
	double startTime;
	double playingDuration = 30;
	double endTime;
	
	// Use this for initialization
	void Start () {
		endTime = Time.time + playingDuration;		
	}
	
	// Update is called once per frame
	void Update () {
		if (this.IsEnd()) {
			endTime = Time.time;
			Destroy(GameObject.FindGameObjectWithTag("Player"));
		}
	}
	
	bool IsEnd() {
		Debug.Log(string.Format("{0}, {1}", endTime, Time.time));
		if (endTime <= Time.time) {
			return true;
		}
		return false;	
	}
	
	void AddComputerPlayer() {
		
	}
}