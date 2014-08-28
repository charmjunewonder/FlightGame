using UnityEngine;
using System.Collections;

public class PlaneRotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float amount = Input.GetAxis ("Horizontal");
		transform.Rotate(0, 0, -amount/2);

		if (transform.rotation.z > 0) {
			if (Mathf.Approximately(amount,0f)) {
					//transform.rotation = new Quaternion(0, 180, 0, 0);
					transform.Rotate (0, 0, .5f);
			}
		} else if (transform.rotation.z < 0) {
			if (Mathf.Approximately(amount,0f)) {
				//transform.rotation = new Quaternion(0, 180, 0, 0);
				transform.Rotate (0, 0, -.5f);
			}
		}
	}
}
//if (Input.GetKeyUp (KeyCode.RightArrow) || Input.GetKeyUp (KeyCode.LeftArrow)) {