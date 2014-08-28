using UnityEngine;
using System.Collections;

public class PlaneCollide : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter () {
		Debug.Log ("oops");
		anim.SetBool("isCrush", true);
		audio.Play ();
	}
}
