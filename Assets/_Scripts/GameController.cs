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

	public GameObject FireWallPool;
	ObjectPool poolOfFireWall;

	public Texture DropCubeIcon;
	public Texture FireWallIcon;

	public static int additionalScore;
	public int score;
	int level;
	public Font myFont;
	public GameObject levelLabel;
	public GameObject fire;
	float previousPostionOfPlane;
	public static bool isSpeedUp;
	public static bool isInsane;
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

		Plane.speed = 50.0f;
		originPositionOfPlane = plane.transform.position.z;
		StartCoroutine("PutCube");
		StartCoroutine("PutScoreItem");
		StartCoroutine ("CheckLevel");
	}
	
	// Update is called once per frame
	void Update () {
		if (isSpeedUp) {
			additionalScore += (int)(plane.transform.position.z - previousPostionOfPlane) / 10;
		}
		if (isInsane) {
			additionalScore += (int)(plane.transform.position.z - previousPostionOfPlane) *2 / 10;
		}
		previousPostionOfPlane = plane.transform.position.z;
	}

	IEnumerator PutScoreItem () {
		for (;;) {
			GameObject scoreItem = poolOfScoreItem.instance.GetObjectFromPool ();
			scoreItem.transform.position = plane.transform.position + new Vector3 (Random.Range (-200, 200), 0, Random.Range (1200, 1500));
			yield return new WaitForSeconds (1f);
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
			yield return new WaitForSeconds (5f);
		}
	}

	public void PutMoreCube () {
		StartCoroutine("DropCube");
	}

	IEnumerator PutCube () {
		for (;;) {
			GameObject cube = poolOfCube.GetObjectFromPool ();
			cube.transform.position = plane.transform.position + new Vector3 (Random.Range (-300, 300), -5, Random.Range (1500, 1800));
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
		Vector3 finalPosition = plane.transform.position + new Vector3 (Random.Range (-150, 150), 0, Random.Range (300, 400));

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

	IEnumerator PutFire () {
		for (;;) {
			GameObject fire = poolOfFireWall.GetObjectFromPool ();
			//fire.particleSystem.Clear();
			//fire.transform.localScale = new Vector3(1, 1, 1);
			//fire.particleSystem.Play();
			fire.transform.rotation = Quaternion.Slerp(fire.transform.rotation, Quaternion.Euler(0, 0, 0), 1.0f);
			fire.transform.position = plane.transform.position + new Vector3 (Random.Range (-100, 100), -plane.transform.position.y, 500);

			fire.GetComponent<Fire>().GlowUp();
			yield return new WaitForSeconds (1.5f);
		}
	}

	IEnumerator CheckLevel () {
		bool l2 = false, l3 = false, l4 = false, l5 = false;
		for (;;) {
			Debug.Log(score);
			if(!l2 && score > 500){
				levelLabel.guiText.text = "Level 2";
				level = 2;
				levelLabel.SetActive(true);
				poolOfDropCube = DropCubePool.GetComponent<ObjectPool> ();
				yield return new WaitForSeconds (2f);
				levelLabel.SetActive(false);
				StartCoroutine("PutDropCube");
				l2 = true;
			}
			if(!l3 && score > 800){
				//levelLabel.guiText.text = "Level 3";
				//level = 3;
				//levelLabel.SetActive(true);
				poolOfSpeedItem = SpeedItemPool.GetComponent<ObjectPool> ();
				yield return new WaitForSeconds (2f);
				levelLabel.SetActive(false);
				StartCoroutine("PutSpeedItem");
				l3 = true;
			}
			if(!l4 && score > 1200){
				levelLabel.guiText.text = "Level 3";
				level = 3;
				levelLabel.SetActive(true);
				poolOfFireWall = FireWallPool.GetComponent<ObjectPool> ();

				yield return new WaitForSeconds (2f);
				levelLabel.SetActive(false);
				StartCoroutine("PutFire");
				l4 = true;
			}
			if(!l5 && score > 1500){
				//levelLabel.guiText.text = "Level 5";
				//level = 5;
				//levelLabel.SetActive(true);
				poolOfInsanityModeItem = InsanityModeItemPool.GetComponent<ObjectPool> ();
				yield return new WaitForSeconds (2f);
				levelLabel.SetActive(false);
				StartCoroutine("PutInsanityModeItem");
				l5 = true;
			}

			yield return new WaitForSeconds (1f);
		}
	}

	void OnGUI () {
		score = (int)(plane.transform.position.z - originPositionOfPlane)/10 + additionalScore;

		GUIStyle myStyle = new GUIStyle();
		myStyle.font = myFont;
		myStyle.normal.textColor = Color.white;
		myStyle.fontSize = 30;
		GUI.Label(new Rect(10, 10, 100, 20), "Score: " + score, myStyle);

		if(level > 1)
			GUI.DrawTexture(new Rect(Screen.width-55, 5, 50, 50), DropCubeIcon, ScaleMode.ScaleToFit, true, 0);
		if(level > 2)
			GUI.DrawTexture(new Rect(Screen.width-110, 5, 50, 50), FireWallIcon, ScaleMode.ScaleToFit, true, 0);
		float scoreSpeed = 0;
		if (isSpeedUp)
			scoreSpeed += 1;
		if (isInsane)
			scoreSpeed += 2;
		GUI.Label(new Rect(30, 40, 100, 20), "        X" + scoreSpeed, myStyle);
		scoreSpeed = 0;
	}
}
