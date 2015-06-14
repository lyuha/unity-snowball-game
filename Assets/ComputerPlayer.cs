using UnityEngine;
using System.Collections;

public class ComputerPlayer : Player {

	
	void OnDestroy() {
		GameRule gameRule;
		GameObject gameObject = GameObject.Find("/GameRule") as GameObject;
		if (gameObject != null) {
			gameRule = gameObject.GetComponent<GameRule>();
			gameRule.increaseScore();
		}
	}
}