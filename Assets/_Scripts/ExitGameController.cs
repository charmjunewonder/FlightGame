using UnityEngine;
using System.Collections;

public class ExitGameController : MonoBehaviour {
	public GUISkin mySkin;
	void Start () {
		Screen.showCursor = true;
	}
	void OnGUI () {
		GUI.skin = mySkin;

		GUIStyle myStyle = new GUIStyle();
		myStyle.normal.textColor = Color.white;
		myStyle.fontSize = 100;
		GUI.Label(new Rect(Screen.width/2-190, Screen.height/5-50, 100,20), "Flight Game", myStyle);

		GUI.Label(new Rect(Screen.width/2-100, Screen.height/2-100, 200, 20), "Score: " + Storage.score);

		if(GUI.Button(new Rect(Screen.width/2-100,Screen.height/2-20,200,50), "Restart")) {
			Application.LoadLevel("Game");
		}
		
		if(GUI.Button(new Rect(Screen.width/2-100,Screen.height/2+100,200,50), "Exit")) {
			print ("You clicked me!");
			Application.Quit();
		}
	}
}
