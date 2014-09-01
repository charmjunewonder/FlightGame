using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

	// Use this for initialization
	void Start () {

		GlowUp ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
//	IEnumerator FireGlowUp() {
		//yield return new WaitForSeconds (3.0f);
//		Quaternion target = Quaternion.Euler(0, Random.Range (0, 150), 0);
//		transform.rotation = Quaternion.Slerp(transform.rotation, target, 1.0f);
//		float i = 0.0f;
//		float rate = 0.01f;
//		transform.localScale = new Vector3(100, 1, 1);
//		Vector3 finalScale = new Vector3(100, 1, 1);
//		//Vector3 originalScale = transform.localScale;
//		transform.localScale = finalScale;
//		while (i < 1.0f) {
//			i += Time.deltaTime * rate;
//			transform.localScale = Vector3.Lerp(transform.localScale, finalScale, i);
//			yield return null; 
//			if(Vector3.Angle(transform.localScale, finalScale) < 1.0f)
//				break;
//		}

//	}
	public void GlowUp(){
		//StartCoroutine ("FireGlowUp");
		Quaternion target = Quaternion.Euler(0, Random.Range (0, 150), 0);
		transform.rotation = Quaternion.Slerp(transform.rotation, target, 1.0f);
	}
//	IEnumerator FireGlowUp() {
//		float i = 0.0f;
//		float rate = 0.01f;
//		Vector3 finalScale = new Vector3(100, 1, 1);
//		//Vector3 originalScale = transform.localScale;
//		transform.localScale = finalScale;
//		while (i < 1.0f) {
//			i += Time.deltaTime * rate;
//			transform.localScale = Vector3.Lerp(transform.localScale, finalScale, i);
//			yield return null; 
//			if(Vector3.Angle(transform.localScale, finalScale) < 1.0f)
//				break;
//		}
//		yield return new WaitForSeconds(5.0f);
//		while (i < 1.0f) {
//			i += Time.deltaTime * rate;
//			transform.localScale = Vector3.Lerp(transform.localScale, originalScale, i);
//			yield return null; 
//			if(Vector3.Angle(transform.localScale, originalScale) < 1.0f)
//				break;
//		}
//	}

}
