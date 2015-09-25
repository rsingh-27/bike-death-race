using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {


	void Update () {
	
		if (Input.GetMouseButtonDown(0)&&GetComponent<GUITexture>().HitTest(Input.mousePosition)){

			Debug.Log("Menu");
			Application.LoadLevel(0);
		}

	}
}
