using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameMenu : MonoBehaviour {
	Button play;
	Button quit;
	// Use this for initialization
	void Start () {
		play = gameObject.transform.Find("Play").GetComponent<Button>();
		play.onClick.AddListener(() => {
			Application.LoadLevel("Main");
		});
		
		quit = gameObject.transform.Find("Quit").GetComponent<Button>();
		quit.onClick.AddListener(() => {
			Application.Quit();
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
