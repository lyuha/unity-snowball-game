using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameRule : MonoBehaviour {
	double startTime;
	double playingDuration = 30;
	double endTime;
	GameObject hud;
	Text remainedTime;
	
	// Use this for initialization
	void Start () {
		endTime = Time.time + playingDuration;
		hud = GameObject.Find("/HUDCanvas");
		remainedTime = hud.transform.Find("Time").gameObject.GetComponent<Text>() as Text;
		remainedTime.text = playingDuration.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		if (this.IsEnd()) {
			endTime = Time.time;
			Destroy(GameObject.FindGameObjectWithTag("Player"));
		}
		remainedTime.text = ((int)(endTime - Time.time)).ToString();
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