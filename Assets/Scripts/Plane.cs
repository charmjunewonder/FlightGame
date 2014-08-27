using UnityEngine;
using System.Collections;

public class Plane : MonoBehaviour {

	float speed = 50.0f;
	float facing = 0.0f; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		facing += Input.GetAxis("Horizontal") * Time.deltaTime;
//		transform.rotation = Quaternion.Euler(0, facing, 0);
		transform.position += Vector3.right * Input.GetAxis ("Horizontal");
		//transform.rotation.z += Input.GetAxis ("Horizontal");
		transform.position += transform.forward * speed * Time.deltaTime * 2;
	}
}
