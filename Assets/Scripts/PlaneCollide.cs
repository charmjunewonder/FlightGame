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
		if(isColliding) return;
		isColliding = true;
		if (other.tag == "Cube") {
			Debug.Log ("oops");
//			anim.SetBool ("isCrush", true);
			audio.Play ();
		} else if (other.tag == "ScoreItem") {
			other.gameObject.SetActive(false);
			GameController.additionalScore += 100;
		} else if (other.tag == "SpeedItem"){
			other.gameObject.SetActive(false);
			StartCoroutine("SpeedUp");
		} else if (other.tag == "InsanityModeItem"){
			other.gameObject.SetActive(false);
			StartCoroutine("StartInsanityMode");
		}
	}

	IEnumerator SpeedUp () {
		Plane.speed += 50;
		yield return new WaitForSeconds (5f);
		StartCoroutine("SpeedSlowDown");
	}

	IEnumerator SpeedSlowDown () {
		for (int i = 0; i < 5; i++) {
			Plane.speed -= 10;
			yield return new WaitForSeconds(.1f);
		}
	}

	IEnumerator StartInsanityMode () {
		StartCoroutine("PutMoreCube");
		Debug.Log ("start");
		yield return new WaitForSeconds(10f);
		StopCoroutine("PutMoreCube");
		Debug.Log ("stop");
	}

	IEnumerator PutMoreCube () {
		for (;;) {
			GameController.instance.PutMoreCube();
			yield return new WaitForSeconds (.05f);
		}
	}
}
