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
		hud = GameObject.Find("/HUDCanvas");
		endTime = Time.time + playingDuration;
		remainedTime = hud.transform.Find("StatusUI/Time").gameObject.GetComponent<Text>() as Text;
		remainedTime.text = playingDuration.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		int minutes, second, millisecond;
		double currentTime;
		
		if (this.IsEnd()) {
			endTime = Time.time;
			Destroy(GameObject.FindGameObjectWithTag("Player"));
		}
		
		currentTime = endTime - Time.time; 
		minutes = (int)currentTime / 60;
		second = (int)currentTime % 60;
		millisecond = (int)((currentTime - (int)currentTime) * 100);
		remainedTime.text = string.Format("{0} : {1} : {2}", minutes, second, millisecond); 
	}
	
	bool IsEnd() {
		if (endTime <= Time.time) {
			return true;
		}
		return false;	
	}
	
	void AddComputerPlayer() {
		
	}
}