using UnityEngine;
using System.Collections;

public class Retry : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)&&GetComponent<GUITexture>().HitTest(Input.mousePosition)){
			Debug.Log("Retry");
			Application.LoadLevel(Application.loadedLevelName);
			
		}
	}
}
