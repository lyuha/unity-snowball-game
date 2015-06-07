using UnityEngine;
using System.Collections;

public class ComputerPlayer : Player {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnDestroy() {
		GameRule gameRule;
		GameObject gameObject = GameObject.Find("/GameRule") as GameObject;
		if (gameObject != null) {
			gameRule = gameObject.GetComponent<GameRule>();
			gameRule.increaseScore();
		}
	}
}