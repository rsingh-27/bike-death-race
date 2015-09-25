using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;



public class GUIManager : MonoBehaviour {

	public GUIText txtCounter,txtMessage;

	public GUITexture shade,retry,resume,menu,pause;

	private bool bannerShown=false;
	private static bool interstitialLoad=false;

	float best;

	private static float startTime;
	private float elapsedTime;

	private BannerView bannerAd;

	// Use this for initialization
	void Start () {
	
		Debug.Log("Loaded");
		MainCamera.isloading=false;


		GameEventManager.GamePause+=GamePause;
		GameEventManager.GameOver+=GameOver;
		GameEventManager.GameResume+=GameResume;
		Time.timeScale=1;
		MainCamera.instance.LoadAd();

		best=PlayerPrefs.GetFloat("Score");






	}
	
	void Update () {


	}





	void OnDestroy(){

		//bannerAd.Destroy ();

		GameEventManager.GamePause-=GamePause;
		GameEventManager.GameOver-=GameOver;
		GameEventManager.GameResume-=GameResume;


	}

	private void GamePause(){

		Time.timeScale=0;
		shade.gameObject.SetActive(true);
		menu.gameObject.SetActive(true);
		resume.gameObject.SetActive(true);
		pause.gameObject.SetActive(false);

		AudioListener.pause=true;


	}

	private void GameOver(){

		shade.gameObject.SetActive(true);
		menu.gameObject.SetActive(true);
		retry.gameObject.SetActive(true);
		pause.gameObject.SetActive(false);

		GetComponent<AudioSource>().volume=0.025f;

		elapsedTime=Time.time-startTime;

		Debug.Log("elapsed: "+elapsedTime+" Start time: "+startTime);
		ShowMessage();
		MainCamera.instance.ShowAd();
		
	}


	private void ShowMessage(){
		txtMessage.gameObject.SetActive(true);

		float score=Mathf.Round((Harley.distance/1000)*100f)/100f;


		if (score>best){
			txtMessage.text="Sad, your harley exploded,\n but you were better than your \n previous incarnations doing "+PlayerPrefs.GetFloat("Score")+" km.";

		}else{
			txtMessage.text="Holy Sh*t your bike exploded, and you made\n "+score+" km before RIP. " +
				"Btw you made your\n best as "+best+"km in your previous incarnation.";
		}



	}

	private void GameResume(){
		shade.gameObject.SetActive(false);
		menu.gameObject.SetActive(false);
		resume.gameObject.SetActive(false);
		pause.gameObject.SetActive(true);
		AudioListener.pause=false;

		Time.timeScale=1;

		//StartCoroutine(CountDown());
	}

	IEnumerator CountDown(){


		txtCounter.gameObject.SetActive(true);
		txtCounter.text="3";
		
		yield return new WaitForSeconds(1);
		txtCounter.text="2";
		
		yield return new WaitForSeconds(1);
		txtCounter.text="1";
		
		yield return new WaitForSeconds(1);
		
		txtCounter.text="Go";
		
		yield return new WaitForSeconds(1);
		
		txtCounter.gameObject.SetActive(false);
		
	}




}
