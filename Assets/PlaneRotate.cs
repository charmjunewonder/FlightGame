using UnityEngine;
using System.Collections;

public class PlaneRotate : MonoBehaviour {
	float minimumX= -360;
	float maximumX= 360;
	
	float rotationX  = 0;
	
	float RSpeed = 20f;
	
	Quaternion originalRotation;
	Quaternion qTo;
	// Use this for initialization
	void Start () {
		originalRotation = transform.rotation;
		qTo = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		float amount = Input.GetAxis ("Horizontal");
		transform.Rotate(0, 0, -amount/2);

		//if (Mathf.Approximately (transform.rotation.z, 0f)) {
//			if (transform.rotation.z > 0) {
//					float angle = 0.0F;
//					Vector3 axis = Vector3.zero;
//					transform.rotation.ToAngleAxis (out angle, out axis);
//					Vector3 rot = new Vector3 (0, 1, 0) - axis;
//					if (Mathf.Approximately (amount, 0f)) {
//							//transform.rotation = new Quaternion(0, 180, 0, 0);
//							transform.Rotate (rot);
//					}
//			} else if (transform.rotation.z < 0) {
//					if (Mathf.Approximately (amount, 0f)) {
//							//transform.rotation = new Quaternion(0, 180, 0, 0);
//							//float abz = transform.rotation.z;
//				float angle = 0.0F;
//				Vector3 axis = Vector3.zero;
//				transform.rotation.ToAngleAxis (out angle, out axis);
//				Vector3 rot = axis - new Vector3 (0, 1, 0) ;
//				transform.Rotate (rot);
//					}
//			}
		//}

		rotationX += Input.GetAxis("Horizontal") * Time.deltaTime * RSpeed;
		rotationX = ClampAngle (rotationX, minimumX, maximumX);

		var xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.forward);

		if (Mathf.Approximately (amount, 0f))
		{
			rotationX = 0.0f; 
		}
		
		qTo =  originalRotation * xQuaternion;
		
		transform.rotation = Quaternion.Slerp(transform.rotation, qTo, RSpeed * Time.deltaTime);
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



