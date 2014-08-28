using UnityEngine;
using System.Collections;

public class Plane : MonoBehaviour {

	float speed = 50.0f; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Vector3.right * Input.GetAxis ("Horizontal")*2;
		//transform.Rotate(0, 0, Input.GetAxis ("Horizontal"));
		transform.position += transform.forward * speed * Time.deltaTime * 2;
	}
}
