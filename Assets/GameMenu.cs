using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameMenu : MonoBehaviour {
	Button playButton;
	// Use this for initialization
	void Start () {
		playButton = gameObject.GetComponent<Button>();
		playButton.onClick.AddListener(() => {
			Application.LoadLevel("Main");
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
