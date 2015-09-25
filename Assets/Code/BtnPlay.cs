using UnityEngine;
using System.Collections;

public class BtnPlay : MonoBehaviour {

	
	void Update () {

		if (Input.GetMouseButtonDown(0)&&GetComponent<GUITexture>().HitTest(Input.mousePosition)){


			Application.LoadLevel(1);
			Debug.Log("Loading");
			MainCamera.isloading=true;

		}

	}
}
