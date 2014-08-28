using UnityEngine;
using System.Collections;

public class PlaneRotate : MonoBehaviour {
	public Camera mainCamera;
	float minimumX= -360;
	float maximumX= 360;
	
	float rotationX  = 0;
	float rotationXOfCamera  = 0;

	float RSpeed = 20f;
	
	Quaternion originalRotation;
	Quaternion qTo;
	Quaternion originalRotationOfCamera;
	Quaternion qToOfCamera;
	// Use this for initialization
	void Start () {
		originalRotation = transform.rotation;
		qTo = transform.rotation;
		originalRotationOfCamera = mainCamera.transform.rotation;
		qToOfCamera = mainCamera.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
//		if (Mathf.Approximately (transform.rotation.z, 0f))
//						return;
		float amount = Input.GetAxis ("Horizontal");

		RSpeed = 20f;
		rotationX += amount  * Time.deltaTime * RSpeed;
		rotationX = ClampAngle (rotationX, minimumX, maximumX);

		rotationXOfCamera += amount /10 * Time.deltaTime * RSpeed;
		rotationXOfCamera = ClampAngle (rotationXOfCamera, minimumX, maximumX);

		Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.forward);
		Quaternion xQuaternionOfCamera = Quaternion.AngleAxis (rotationXOfCamera, Vector3.forward);
		if (Mathf.Approximately (amount, 0f))
		{
			rotationX = 0.0f; 
			RSpeed = 2.5f;
		}
		
		qTo =  originalRotation * xQuaternion;
		qToOfCamera = originalRotationOfCamera * xQuaternionOfCamera;
		transform.rotation = Quaternion.Slerp(transform.rotation, qTo, RSpeed * Time.deltaTime);
		mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, qToOfCamera, RSpeed * Time.deltaTime);
	}
		
	static float ClampAngle (float angle, float min, float max){
		if (angle < -360.0f)
			angle += 360.0f;
		if (angle > 360.0f)
			angle -= 360.0f;
		return Mathf.Clamp (angle, min, max);
	}
}
//if (Input.GetKeyUp (KeyCode.RightArrow) || Input.GetKeyUp (KeyCode.LeftArrow)) {



