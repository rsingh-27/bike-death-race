using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {


	
	void Update () {
	
		if (Input.GetMouseButtonDown(0)&&GetComponent<GUITexture>().HitTest(Input.mousePosition)){
			GameEventManager.TriggerGamePause();
			Debug.Log("Pause");

		}

	}
}
