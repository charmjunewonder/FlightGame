using UnityEngine;
using System.Collections;

public class LightBeam : MonoBehaviour {
	public GameObject lightBeam;
	public GameObject outerLight;
	public GameObject innerLight;
	// Use this for initialization
	void Start () {
		StartCoroutine ("GlowUp");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator GlowUp() {
		float i = 0.0f;
		float rate = 0.01f;
		Vector3 finalScale = new Vector3(5, 40, 5);
		while (i < 1.0f) {
			i += Time.deltaTime * rate;
			outerLight.light.range = Mathf.Lerp(outerLight.light.range, 8, i);
			innerLight.light.range = Mathf.Lerp(innerLight.light.range, 4, i);
			lightBeam.transform.localScale = Vector3.Lerp(lightBeam.transform.localScale, finalScale, i);
			lightBeam.renderer.material.SetFloat("_Brightness", lightBeam.renderer.material.GetFloat("_Brightness") + 0.003f);
			yield return null; 
			if(Mathf.Approximately(outerLight.light.range, 8.0f))
				break;
		}
	}

}
