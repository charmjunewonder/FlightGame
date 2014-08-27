using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
	
	public static ObjectPool instance;

	public GameObject objectPrefab;
	/// The pooled objects currently available.
	public List<GameObject> objectPool;
	
	public GameObject plane;
	public int unusedDistance;

	/// The amount of objects of each type to buffer.
	public int amountToBuffer;
	
	/// The container object that we will keep unused pooled objects so we dont clog up the editor with objects.
	protected GameObject containerObject;
	
	void Awake ()
	{
		instance = this;
	}
	
	// Use this for initialization
	void Start ()
	{
		containerObject = new GameObject("ObjectPool");
		
		//Loop through the object prefabs and make a new list for each one.
		//We do this because the pool can only support prefabs set to it in the editor,
		//so we can assume the lists of pooled objects are in the same order as object prefabs in the array

		objectPool = new List<GameObject>(); 

		for ( int n=0; n<amountToBuffer; n++)
		{
			GameObject newObj = Instantiate(objectPrefab) as GameObject;
			newObj.SetActive(false);
			newObj.GetComponent<Cube>().isUsed = false;
			newObj.transform.parent = containerObject.transform;
			objectPool.Add(newObj);
		}

		StartCoroutine("DoCheck");
	}

	/// <summary>
	/// Gets a new object for the name type provided.  If no object type exists or if onlypooled is true and there is no objects of that type in the pool
	/// then null will be returned.
	/// </summary>
	/// <returns>
	/// The object for type.
	/// </returns>
	/// <param name='objectType'>
	/// Object type.
	/// </param>
	/// <param name='onlyPooled'>
	/// If true, it will only return an object if there is one currently pooled.
	/// </param>
	public GameObject GetObjectFromPool ()
	{
		for (int i = 0; i < objectPool.Count; i++) {
			if(objectPool[i].GetComponent<Cube>().isUsed){
				continue;
			}
			objectPool[i].GetComponent<Cube>().isUsed = true;
			objectPool[i].SetActive(true);
			return objectPool[i];
		}
		GameObject newObj = Instantiate(objectPrefab) as GameObject;
		newObj.SetActive(true);
		newObj.transform.parent = containerObject.transform;
		objectPool.Add(newObj);
		return newObj;
	}

	void CheckForUnusedObject() {
		foreach (GameObject cube in objectPool){
			if ((plane.transform.position.z - cube.transform.position.z) > unusedDistance ) {
				cube.GetComponent<Cube>().isUsed = false;
				cube.SetActive(false);
			}
		}	
		return;
	}

	IEnumerator DoCheck() {
		for(;;) {
			CheckForUnusedObject();
			yield return new WaitForSeconds(.5f);
		}
	}

}































