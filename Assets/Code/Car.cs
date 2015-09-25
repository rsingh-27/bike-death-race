using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {


	void OnEnable(){
		GetComponent<Rigidbody>().velocity = new Vector3 (0f,0f,GameEventManager.instance.minSpeed);

	}

	void OnDisable(){
		GetComponent<Rigidbody>().velocity = Vector3.zero;

	}


	void Update () {

		if (transform.position.y<0){
			RoadManager.objectsPresent--;
			gameObject.SetActive(false);

		}
	}
}
