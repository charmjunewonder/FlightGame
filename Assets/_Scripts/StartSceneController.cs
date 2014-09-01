using UnityEngine;
using System.Collections;

public class StartSceneController : MonoBehaviour {
	public Font myFont;
	void OnGUI () {
		GUIStyle myStyle = new GUIStyle();
		myStyle.font = myFont;
		myStyle.normal.textColor = Color.white;
		myStyle.fontSize = 50;
		// Make a background box
		GUI.Label(new Rect(Screen.width/2-50, Screen.height/5, 100,20), "Flight Game", myStyle);
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(Screen.width/2-100,Screen.height/2-25,200,50), "Start")) {
			Application.LoadLevel("Game");
		}
		
		// Make the second button.
		if(GUI.Button(new Rect(Screen.width/2-100,Screen.height/2+50,200,50), "Exit")) {
			print ("You clicked me!");
			Application.Quit();
			print ("You clicked me!");
		}
	}
}
