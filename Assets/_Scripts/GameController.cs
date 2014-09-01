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
	public Texture DropCubeIcon;

	public static int additionalScore;
	public int score;
	int level;
	public Font myFont;
	public GameObject levelLabel;

	void Awake ()
	{
		instance = this;
	}
	// Use this for initialization
	void Start () {
		additionalScore = 0;
		level = 1;
		poolOfScoreItem = ScoreItemPool.GetComponent<ObjectPool> ();
		poolOfCube = CubePool.GetComponent<ObjectPool> ();
		poolOfDropCube = DropCubePool.GetComponent<ObjectPool> ();
		poolOfSpeedItem = SpeedItemPool.GetComponent<ObjectPool> ();
		poolOfInsanityModeItem = InsanityModeItemPool.GetComponent<ObjectPool> ();

		originPositionOfPlane = plane.transform.position.z;
		StartCoroutine("PutCube");
		StartCoroutine("PutScoreItem");
		StartCoroutine ("CheckLevel");
		StartCoroutine ("PutInsanityModeItem");
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator PutScoreItem () {
		for (;;) {
			GameObject scoreItem = poolOfScoreItem.instance.GetObjectFromPool ();
			scoreItem.transform.position = plane.transform.position + new Vector3 (Random.Range (-300, 300), 0, Random.Range (1500, 500));
			yield return new WaitForSeconds (.5f);
		}
	}

	IEnumerator PutSpeedItem () {
		for (;;) {
			GameObject speedItem = poolOfSpeedItem.instance.GetObjectFromPool ();
			speedItem.transform.position = plane.transform.position + new Vector3 (Random.Range (-300, 300), 0, Random.Range (1500, 1800));
			yield return new WaitForSeconds (2f);
		}
	}

	IEnumerator PutInsanityModeItem () {
		for (;;) {
			GameObject insanityModeItem = poolOfInsanityModeItem.instance.GetObjectFromPool ();
			insanityModeItem.transform.position = plane.transform.position + new Vector3 (Random.Range (-300, 300), 0, Random.Range (1500, 500));
			yield return new WaitForSeconds (2f);
		}
	}

	public void PutMoreCube () {
		GameObject cube = poolOfCube.GetObjectFromPool ();
		cube.transform.position = plane.transform.position + new Vector3 (Random.Range (-300, 300), 0, Random.Range (500, 800));
	}

	IEnumerator PutCube () {
		for (;;) {
			GameObject cube = poolOfCube.GetObjectFromPool ();
			cube.transform.position = plane.transform.position + new Vector3 (Random.Range (-300, 300), 0, Random.Range (1500, 1800));
			Quaternion target = Quaternion.Euler(Random.Range (-5.0f, 5.0f), Random.Range (0, 360), Random.Range (-10.0f, 10.0f));
			cube.transform.rotation = Quaternion.Slerp(cube.transform.rotation, target, 1.0f);
			yield return new WaitForSeconds (.2f);
		}
	}

	IEnumerator PutDropCube () {
		for (;;) {
			StartCoroutine("DropCube");
			yield return new WaitForSeconds (.5f);
		}
	}

	IEnumerator DropCube () {
		GameObject cube = poolOfDropCube.GetObjectFromPool ();
		Vector3 finalPosition = plane.transform.position + new Vector3 (Random.Range (-150, 150), 0, Random.Range (200, 300));

		cube.transform.position = finalPosition + Vector3.up * 50;
//		for (int i = 0; i < 2; i++) {
//			cube.transform.position -= new Vector3(0, 25, 0);
//			yield return null;
//		}
		float i = 0.0f;
		float rate = 5f;
		while (i < 1.0f) {
			i += Time.deltaTime * rate;
			cube.transform.position = Vector3.Lerp(cube.transform.position, finalPosition, i);
			yield return null; 
			if(Mathf.Abs(cube.transform.position.y - finalPosition.y) < 0.1f)
				break;
		}
	}

	IEnumerator CheckLevel () {
		for (;;) {
			if(score > 500){
				levelLabel.guiText.text = "Level 2";
				level = 2;
				levelLabel.SetActive(true);
				yield return new WaitForSeconds (2f);
				levelLabel.SetActive(false);
				StartCoroutine("PutDropCube");
				break;
			}
			if(score > 1000){
				levelLabel.guiText.text = "Level 3";
				level = 3;
				levelLabel.SetActive(true);
				yield return new WaitForSeconds (2f);
				levelLabel.SetActive(false);
				StartCoroutine("PutSpeedItem");
				break;
			}
			yield return new WaitForSeconds (1f);
		}
	}

	void OnGUI () {
		score = (int)(plane.transform.position.z - originPositionOfPlane)/10 + additionalScore;
		GUIStyle myStyle = new GUIStyle();
		myStyle.font = myFont;

		GUI.Label(new Rect(10, 10, 100, 20), "Score: " + score, myStyle);
		if(level > 2)
			GUI.DrawTexture(new Rect(Screen.width-55, 5, 50, 50), DropCubeIcon, ScaleMode.ScaleToFit, true, 0);


	}
}
