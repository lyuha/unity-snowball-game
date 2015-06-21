using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
	Button restart;
	Button menu;
	
	// Use this for initialization
	void Start () {
		restart = gameObject.transform.FindChild("Restart").GetComponent<Button>();
		menu = gameObject.transform.FindChild("MainMenu").GetComponent<Button>();
		
		restart.onClick.AddListener(() => {
			Application.LoadLevel("Main");
		});
		menu.onClick.AddListener(() => {
			Application.LoadLevel("Menu");
		});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
