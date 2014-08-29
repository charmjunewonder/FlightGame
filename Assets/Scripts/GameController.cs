using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static GameController instance;

	public GameObject plane;
	float originPositionOfPlane;

	public GameObject ScoreItemPool;
	ObjectPool poolOfScoreItem;

	public GameObject CubePool;
	ObjectPool poolOfCube;

	public GameObject DropCubePool;
	ObjectPool poolOfDropCube;

	public GameObject SpeedItemPool;
	ObjectPool poolOfSpeedItem;

	public GameObject InsanityModeItemPool;
	ObjectPool poolOfInsanityModeItem;

	public static int additionalScore;
	int score;
	void Awake ()
	{
		instance = this;
	}
	// Use this for initialization
	void Start () {
		additionalScore = 0;

		poolOfScoreItem = ScoreItemPool.GetComponent<ObjectPool> ();
		poolOfCube = CubePool.GetComponent<ObjectPool> ();
		poolOfDropCube = DropCubePool.GetComponent<ObjectPool> ();
		poolOfSpeedItem = SpeedItemPool.GetComponent<ObjectPool> ();
		poolOfInsanityModeItem = InsanityModeItemPool.GetComponent<ObjectPool> ();

		originPositionOfPlane = plane.transform.position.z;
		StartCoroutine("PutCube");
		StartCoroutine("PutScoreItem");
		StartCoroutine ("CheckLevel");
		StartCoroutine ("PutSpeedItem");
		StartCoroutine ("PutInsanityModeItem");
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator PutScoreItem () {
		for (;;) {
			GameObject scoreItem = poolOfScoreItem.instance.GetObjectFromPool ();
			scoreItem.transform.position = plane.transform.position + new Vector3 (Random.Range (-300, 300), 0, Random.Range (300, 500));
			yield return new WaitForSeconds (.5f);
		}
	}

	IEnumerator PutSpeedItem () {
		for (;;) {
			GameObject speedItem = poolOfSpeedItem.instance.GetObjectFromPool ();
			speedItem.transform.position = plane.transform.position + new Vector3 (Random.Range (-300, 300), 0, Random.Range (300, 500));
			yield return new WaitForSeconds (.5f);
		}
	}

	IEnumerator PutInsanityModeItem () {
		for (;;) {
			GameObject insanityModeItem = poolOfInsanityModeItem.instance.GetObjectFromPool ();
			insanityModeItem.transform.position = plane.transform.position + new Vector3 (Random.Range (-300, 300), 0, Random.Range (300, 500));
			yield return new WaitForSeconds (1f);
		}
	}

	public void PutMoreCube () {
		GameObject cube = poolOfCube.GetObjectFromPool ();
		cube.transform.position = plane.transform.position + new Vector3 (Random.Range (-300, 300), 0, Random.Range (300, 500));
	}

	IEnumerator PutCube () {
		for (;;) {
			GameObject cube = poolOfCube.GetObjectFromPool ();
			cube.transform.position = plane.transform.position + new Vector3 (Random.Range (-300, 300), 0, Random.Range (300, 500));
			yield return new WaitForSeconds (.1f);
		}
	}

	IEnumerator PutDropCube () {
		for (;;) {
			StartCoroutine("DropCube");

			GameObject cube = poolOfCube.GetObjectFromPool ();
			cube.transform.position = plane.transform.position + new Vector3 (Random.Range (-300, 300), 0, Random.Range (300, 500));
			yield return new WaitForSeconds (.3f);
		}
	}

	IEnumerator DropCube () {
		GameObject cube = poolOfDropCube.GetObjectFromPool ();
		cube.transform.position = plane.transform.position + new Vector3 (Random.Range (-300, 300), 47, Random.Range (200, 300));
		for (int i = 0; i < 2; i++) {
			cube.transform.position -= new Vector3(0, 25, 0);
			yield return null;
		}
	}

	IEnumerator CheckLevel () {
		for (;;) {
			if(score > 1000){

				StartCoroutine("PutDropCube");
				break;
			}
			yield return new WaitForSeconds (5f);
		}
	}

	void OnGUI () {
		score = (int)(plane.transform.position.z - originPositionOfPlane) + additionalScore;
		GUI.Label(new Rect(10, 10, 100, 20), "Score: " + score);
	}
}
