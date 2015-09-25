using UnityEngine;
using System.Collections;

public class BtnCancelHelp : MonoBehaviour {
	
	void Update () {
	
		if (Input.GetMouseButtonDown(0)&&GetComponent<GUITexture>().HitTest(Input.mousePosition)){

			gameObject.SetActive(false);
		}

	}
}
