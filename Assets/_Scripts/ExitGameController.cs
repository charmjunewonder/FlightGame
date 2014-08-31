using UnityEngine;
using System.Collections;

public class ExitGameController : MonoBehaviour {

	void OnGUI () {
		
		// Make a background box
		GUI.Box(new Rect(Screen.width/5, Screen.height/5, Screen.width*3/5,Screen.height*3/5), "Flight Game");

		GUI.Label(new Rect(Screen.width/2-100, Screen.height/2-100, 100, 20), "Score: " + Storage.score);

		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(Screen.width/2-100,Screen.height/2-25,200,50), "ReStart")) {
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
