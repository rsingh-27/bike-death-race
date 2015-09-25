using UnityEngine;
using System.Collections;

public class BtnHelp : MonoBehaviour {


	public GameObject instruction;

	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)&&GetComponent<GUITexture>().HitTest(Input.mousePosition)){
			
			instruction.SetActive(true);
		}
	}
}
