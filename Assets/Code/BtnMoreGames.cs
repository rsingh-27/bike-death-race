using UnityEngine;
using System.Collections;

public class BtnMoreGames : MonoBehaviour {

	void Update () {

			if(Input.GetMouseButtonDown(0)&&GetComponent<GUITexture>().HitTest(Input.mousePosition)){

				AppFlood.ShowPanel(AppFlood.PANEL_LANDSCAPE);
			}


	}
}
