using UnityEngine;
using System.Collections;

public class Resume : MonoBehaviour {

	void Update () {
		
		if (Input.GetMouseButtonDown(0)&&GetComponent<GUITexture>().HitTest(Input.mousePosition)){
			GameEventManager.TriggerGameResume();
			Debug.Log("Resume");
			
		}
		
	}
}
