using UnityEngine;
using System;
using System.Collections;
using GoogleMobileAds.Api;


public class MainCamera : MonoBehaviour {


	/* This is on Main Menu Screen */


	public GameObject loadingScreen;
	public GameObject loadingText;

	public static bool isloading=false;

	/* 
	 * This will be true when 
	 * game is in running state
	 */
	public static bool isBreakAdLoaded;

	private InterstitialAd startAd;

	public static MainCamera instance;

	private InterstitialAd breakAd;

	//public GoogleAnalyticsV3 googleAnalytics;


	void Awake(){
		instance = this;
	}

	void Start () {
		GetComponent<AudioSource>().Play();
		startAd = new InterstitialAd ("YOUR_ADD_ID");
		isBreakAdLoaded = false;
		AdRequest adrequest =new AdRequest.Builder().Build();

		startAd.LoadAd(adrequest);
		startAd.AdLoaded += HandleAdLoaded;
		//googleAnalytics.StartSession ();



	}


	public void HandleAdLoaded(object sender, EventArgs args)
	{
		// Handle the ad loaded event.
		startAd.Show ();
		
	}


	void Update(){

		loadingScreen.SetActive(!isloading);
		loadingText.SetActive(isloading);

		if (Input.GetKeyDown(KeyCode.Escape)){
			//googleAnalytics.StopSession ();
			Application.Quit();

		}



	}



	public void LoadAd(){
		
		Debug.Log("Load Ad");
		MainCamera.isBreakAdLoaded = true;
		breakAd = new InterstitialAd ("YOUR_ADD_ID");
		AdRequest adrequest = new AdRequest.Builder ().Build ();
		breakAd.LoadAd (adrequest);
		
	}

	public void ShowAd(){
		Debug.Log ("ShowAd: " + breakAd.IsLoaded ());
		if (breakAd.IsLoaded()){
		breakAd.Show();
		}
	}



}
