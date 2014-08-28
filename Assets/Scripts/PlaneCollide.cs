using UnityEngine;
using System.Collections;

public class PlaneCollide : MonoBehaviour {

	Animator anim;
	bool isColliding;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		isColliding = false;
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Cube") {
			Debug.Log ("oops");
//			anim.SetBool ("isCrush", true);
			audio.Play ();
		} else if (other.tag == "ScoreItem") {
			if(isColliding) return;
			isColliding = true;
			other.gameObject.SetActive(false);
			GameController.additionalScore += 100;
		}
	}
}
