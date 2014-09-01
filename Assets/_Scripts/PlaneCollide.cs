using UnityEngine;
using System.Collections;

public class PlaneCollide : MonoBehaviour {

	Animator anim;
	bool isColliding;
	public Camera mainCamera;
	bool isSpeedUp = false;
	bool isDebuging = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		isColliding = false;
		if(Input.GetKeyDown(KeyCode.T)){
			Debug.Log ("aaoops");
			isDebuging = !isDebuging;
		}
	}

	void OnTriggerEnter (Collider other) {
		if(isColliding) return;
		isColliding = true;
		if (other.tag == "Cube") {
			Debug.Log ("oops");
			if(isDebuging)
				return;
			Plane.speed = 0;
			Plane.rotation = 0;
			anim.SetBool ("isCrush", true);
			audio.Play ();
			Storage.score = GameController.instance.score;
			StartCoroutine("ExitGame");
		} else if (other.tag == "ScoreItem") {
			other.gameObject.SetActive(false);
			GameController.additionalScore += 100;
		} else if (other.tag == "SpeedItem"){
			if(isSpeedUp)
				return;
			other.gameObject.SetActive(false);
			isSpeedUp = true;
			StartCoroutine("SpeedUp");
			StartCoroutine("DragCameraBack");

		} else if (other.tag == "InsanityModeItem"){
			other.gameObject.SetActive(false);
			StartCoroutine("StartInsanityMode");
		}
	}

	IEnumerator ExitGame () {
		yield return new WaitForSeconds (1f);
		Application.LoadLevel("ExitGame");
	}

	IEnumerator DragCameraBack () {
		StartCoroutine("DoDragCameraBack");
		yield return new WaitForSeconds (5f);
		StartCoroutine("DoDragCameraUp");
		Debug.Log ("hi");
	}

	IEnumerator DoDragCameraBack () {
		float i = 0.0f;
		float rate = 0.2f;
		Vector3 finalPosition = mainCamera.transform.localPosition + Vector3.back * 20;
		while (i < 1.0f) {
			i += Time.deltaTime * rate;
			mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, finalPosition, i);
			yield return null; 
			if(Mathf.Abs(mainCamera.transform.localPosition.z - finalPosition.z) < 0.1f)
				break;
		}
	}

	IEnumerator DoDragCameraUp () {
		float i = 0.0f;
		float rate = 0.2f;
		Vector3 finalPosition = mainCamera.transform.localPosition - Vector3.back * 20;
		while (i < 1.0f) {
			i += Time.deltaTime * rate;
			mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, finalPosition, i);
			yield return null; 
			if(Mathf.Abs(mainCamera.transform.localPosition.z - finalPosition.z) < 0.1f){
				isSpeedUp = false;
				break;
			}
		}
	}

	IEnumerator SpeedUp () {
		float speed = Plane.speed;
		Plane.speed += 50;
		//float originPositionOfPlane = transform.position.z;
		GameController.isSpeedUp = true;
		yield return new WaitForSeconds (5f);
		StartCoroutine(SpeedSlowDown(speed));
		GameController.isSpeedUp = false;
		//GameController.additionalScore += (int)(transform.position.z - originPositionOfPlane)/10;
	}

	IEnumerator SpeedSlowDown (float finalSpeed) {
		float i = 0.0f;
		float rate = 0.2f;
		while (i < 1.0f) {
			i += Time.deltaTime * rate;
			Plane.speed = Mathf.Lerp(Plane.speed, 50, i);
			yield return null; 
			if(Mathf.Approximately(Plane.speed, 50.0f))
				break;
		}
	}
	
	IEnumerator StartInsanityMode () {
		StartCoroutine("PutMoreCube");
		Debug.Log ("start");
		GameController.isInsane = true;
		yield return new WaitForSeconds(10f);
		StopCoroutine("PutMoreCube");
		GameController.isInsane = false;
		Debug.Log ("stop");
	}

	IEnumerator PutMoreCube () {
		for (;;) {
			GameController.instance.PutMoreCube();
			yield return new WaitForSeconds (.1f);
		}
	}
}
