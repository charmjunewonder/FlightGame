using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject plane;
	float originPositionOfPlane;
	// Use this for initialization
	void Start () {
		StartCoroutine("PutCube");
		originPositionOfPlane = plane.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator PutCube () {
		for (;;) {
			//StartCoroutine("DropCube");
			GameObject cube = ObjectPool.instance.GetObjectFromPool ();
			cube.transform.position = plane.transform.position + new Vector3 (Random.Range (-300, 300), 0, Random.Range (300, 500));
			yield return new WaitForSeconds (.1f);
		}
	}

	IEnumerator DropCube () {
		GameObject cube = ObjectPool.instance.GetObjectFromPool ();
		cube.transform.position = plane.transform.position + new Vector3 (Random.Range (-300, 300), 47, Random.Range (200, 300));
		for (int i = 0; i < 2; i++) {
			cube.transform.position -= new Vector3(0, 25, 0);
			yield return null;
		}
	}

	void OnGUI () {
		float distance = plane.transform.position.z - originPositionOfPlane;
		GUI.Label(new Rect(10, 10, 100, 20), "Score: " + distance.ToString("#."));
	}
}
