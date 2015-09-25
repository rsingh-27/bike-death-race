using UnityEngine;

using System;
using System.Collections;

public class GameEventManager:MonoBehaviour {


	/* Singleton instance */
	public static GameEventManager instance;

	/*
	 * Minimum speed needed
	 * for the bike to survive
	 */
	public int minSpeed=50;

	/* 
	 * Current speed of the bike
	 */
	public int currentSpeed=0;

	

	public delegate void GameEvent();
	public static event GameEvent GameStart, GameOver,GamePause,GameResume;


	public void Awake(){


		if (instance==null){
			instance=this;
		}

	}




	public static void TriggerGameStart(){
		
		
		if(GameStart != null){
			GameStart();
		}
	}
	
	public static void TriggerGameOver(){
		if(GameOver != null){
			GameOver();
		}
	}
	
	
	public static void TriggerGamePause(){
		if(GamePause != null){
			GamePause();
		}
	}
	
	public static void TriggerGameResume(){
		if(GameResume != null){
			GameResume();
		}
	}
}
