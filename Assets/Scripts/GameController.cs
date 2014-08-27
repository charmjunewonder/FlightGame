using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject plane;

	// Use this for initialization
	void Start () {
		StartCoroutine("PutCube");
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator PutCube () {
		for (;;) {
			GameObject cube = ObjectPool.instance.GetObjectFromPool ();
			cube.transform.position = plane.transform.position + new Vector3 (Random.Range (-200, 200), 0, Random.Range (300, 500));
			yield return new WaitForSeconds (.1f);
		}
	}
	
}
