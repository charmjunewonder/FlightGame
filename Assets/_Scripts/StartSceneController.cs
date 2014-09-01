using UnityEngine;
using System.Collections;

public class StartSceneController : MonoBehaviour {
	public GUISkin mySkin;

	void Start () {
		Screen.showCursor = true;
	}

	void OnGUI () {
		GUI.skin = mySkin;

		GUIStyle myStyle = new GUIStyle();
		myStyle.normal.textColor = Color.white;
		myStyle.fontSize = 100;
		GUI.Label(new Rect(Screen.width/2-200, Screen.height/5, 100,20), "Flight Game", myStyle);
		if(GUI.Button(new Rect(Screen.width/2-100,Screen.height/2-50,200,50), "Start")) {
			Application.LoadLevel("Game");
		}
		
		if(GUI.Button(new Rect(Screen.width/2-100,Screen.height/2+50,200,50), "Exit")) {
			print ("You clicked me!");
			Application.Quit();
			print ("You clicked me!");
		}

	}
}
