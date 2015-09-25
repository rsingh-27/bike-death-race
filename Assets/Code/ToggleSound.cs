using UnityEngine;
using System.Collections;

public class ToggleSound : MonoBehaviour {

	public Texture2D speakerOn;
	public Texture2D speakerOff;
	
	private bool isSpeakerOn;

	void Awake(){
		isSpeakerOn = PlayerPrefs.GetInt("isSpeakerOn")==0?true:false;

		
		if (isSpeakerOn){
			GetComponent<GUITexture>().texture=speakerOn;
			AudioListener.volume=1;
		}else{
			GetComponent<GUITexture>().texture=speakerOff;
			AudioListener.volume=0;

		}
	}

	void Start () {


		
		
	}
	
	void Update () {
		
		if (Input.GetMouseButtonDown(0) && GetComponent<GUITexture>().HitTest(Input.mousePosition)) {
			
			isSpeakerOn=!isSpeakerOn;
			PlayerPrefs.SetInt("isSpeakerOn", isSpeakerOn?0:1);

			if (isSpeakerOn){
				GetComponent<GUITexture>().texture=speakerOn;
				AudioListener.volume=1;

			}else{
				GetComponent<GUITexture>().texture=speakerOff;
				AudioListener.volume=0;

			}
			
		}
	}
}
